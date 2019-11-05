using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Common;
using GDD.Models;
using log4net;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDD.Admin.Web.Controllers
{
    [RoutePrefix("Import")]
    public class ImportController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(QuestionnaireController));
        IDepartmentService departmentService;
        IFunctionalgroupService functionalgroupService;
        IImportService importService;

        public ImportController()
        {
            departmentService = new DepartmentServer();
            functionalgroupService = new FunctionalgroupServer();
            importService = new ImportServer();
        }

        // GET: Import
        public ActionResult Index(string name)
        {
            ViewBag.Name = name;
            return View();
        }


        [HttpPost]
        [Route("Upload")]
        /// <summary>
        /// 展示上传的excel文件中的内容
        /// </summary>
        public JsonResult Upload(string name)
        {
            int code = 0;
            string msg = "";
            JsonResult result = new JsonResult();
            try
            {
                HttpPostedFileBase file = Request.Files["file"];
                if (file == null || file.ContentLength <= 0)
                {
                    code = (int)ImportResultState.NoFile;
                }
                Stream streamfile = file.InputStream;
                DataTable dt = new DataTable();
                //后缀名
                string FinName = Path.GetExtension(file.FileName);
                if (FinName != ".xls" && FinName != ".xlsx")
                {
                    code = (int)ImportResultState.UnExcel;
                    msg = "文件格式不正确，请导入正确的文件格式";
                    return Json(new { code = code, msg = msg, data = new { } }, JsonRequestBehavior.AllowGet); ;
                }
                ISheet sheet;
                if (FinName == ".xls")
                {
                    //创建一个webbook，对应一个Excel文件(用于xls文件导入类)
                    HSSFWorkbook hssfworkbook = new HSSFWorkbook(streamfile);
                    sheet = hssfworkbook.GetSheetAt(0);
                }
                else
                {
                    //创建一个webbook，对应一个Excel文件(用于xlsx文件导入类)
                    XSSFWorkbook xssfworkbook = new XSSFWorkbook(streamfile);
                    sheet = xssfworkbook.GetSheetAt(0);
                }
                //判断表格的最后一行是否为0
                if (sheet.LastRowNum == 0)
                {
                    code = (int)ImportResultState.NullValue;
                    msg = "文件内容为空，导入失败";
                    return Json(new { code = code, msg = msg, data = new { } }, JsonRequestBehavior.AllowGet); ;
                }
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                //循环添加第一行的列头信息
                for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
                {
                    dt.Columns.Add(sheet.GetRow(0).Cells[j].ToString());
                }
                while (rows.MoveNext())
                {
                    IRow row;
                    //根据文件后缀生成Row的类型
                    if (FinName == ".xls")
                    {
                        row = (HSSFRow)rows.Current;
                    }
                    else
                    {
                        row = (XSSFRow)rows.Current;
                    }
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                }
                dt.Rows.RemoveAt(0);
                if (name == "Employee")
                {
                    code = ImportEmployee(dt);
                }
                else if (name == "Option")
                {
                    code = ImportOption(dt);
                }
                else if (name == "QuestionType")
                {
                    code = ImportQuestionType(dt);
                }
                if (code == (int)ImportResultState.Auccess)
                {
                    msg = "导入成功";
                }
                else if (code == (int)ImportResultState.NullValue)
                {
                    msg = "数据为空";
                }
                
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                code = (int)ImportResultState.Exception;
                msg = "数据异常";
            }
            finally
            {
                result = Json(new { code = code, msg = msg, data = new { } }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        public int ImportEmployee (DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return (int)ImportResultState.NullValue;
            }
            List<Department> departments = departmentService.GetDepartmentList();
            List<FunctionalGroup> functiongroups = functionalgroupService.GetFunctionalgroupList();
            List<Employee> employees = new List<Employee>();
            DateTime date = DateTime.Now;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //去除空格
                Employee tempEmployee = new Employee();
                tempEmployee.EmployeeName = dt.Rows[i]["姓名"].ToString().Replace(" ", "");
                tempEmployee.EmployeeNumber = dt.Rows[i]["工号"].ToString().Replace(" ", "");
                tempEmployee.Sex = dt.Rows[i]["性别"].ToString().Replace(" ", "") == Sex.男.ToString() ? 1 : 0;
                tempEmployee.Phone = dt.Rows[i]["手机号"].ToString().Replace(" ", "");
                tempEmployee.DepartmentID = departments.SingleOrDefault(p => p.DepartmentName == dt.Rows[i]["部门"].ToString().Replace(" ", ""))?.DepartmentID;
                tempEmployee.FunctionalgroupID = functiongroups.SingleOrDefault(p => p.FunctionalGroupName == dt.Rows[i]["功能组"].ToString().Replace(" ", "")).FunctionalGroupID;
                tempEmployee.HireTime = Convert.ToDateTime(dt.Rows[i]["入职日期"].ToString().Replace(" ", ""));
                tempEmployee.Password = MD5.GetMd5Hash("123456");
                tempEmployee.EmployeeID = Guid.NewGuid();
                tempEmployee.CreateTime = date;
                tempEmployee.Status = Convert.ToInt32(EmployeeStatus.在职);
                employees.Add(tempEmployee);
            }
            importService.ImportEmployee(employees);
            return (int)ImportResultState.Auccess;
        }

        public int ImportOption(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return (int)ImportResultState.NullValue;
                
            }
            List<Option> options = new List<Option>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //去除空格
                Option option = new Option();
                option.OptionID = Guid.NewGuid();
                option.OptionName = dt.Rows[i]["选项"].ToString().Replace(" ", "");
                option.OptionNumber = Convert.ToInt32(dt.Rows[i]["序号"].ToString().Replace(" ", ""));
                option.OptionScore = Convert.ToInt32(dt.Rows[i]["分值"].ToString().Replace(" ", ""));
                option.CreateTime = DateTime.Now;
                option.Creator = (Session["user"] as SYS_User)?.UserName;
                options.Add(option);
            }
            importService.ImportOption(options);
            return (int)ImportResultState.Auccess;
        }

        public int ImportQuestionType(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return (int)ImportResultState.NullValue;

            }
            List<QuestionType> questionTypes = new List<QuestionType>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //去除空格
                QuestionType questionType = new QuestionType();
                questionType.QuestionTypeID = Guid.NewGuid();
                questionType.QuestionnaireTypeID = new Guid(dt.Rows[i]["问卷类型ID"].ToString().Replace(" ", ""));
                questionType.QuestionTypeName =dt.Rows[i]["题目类型名称"].ToString().Replace(" ", "");
                questionType.CreateTime = DateTime.Now;
                questionType.Creator = (Session["user"] as SYS_User)?.UserName;
                questionTypes.Add(questionType);
            }
            importService.ImportQuestionType(questionTypes);
            return (int)ImportResultState.Auccess;
        }
    }
}