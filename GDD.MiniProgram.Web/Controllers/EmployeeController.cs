using GDD.Common;
using GDD.MiniProgram.Business.BLL;
using GDD.MiniProgram.Business.IBLL;
using GDD.MiniProgram.VO;
using GDD.MiniProgram.Web.Filter;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDD.MiniProgram.Web.Controllers
{
    [RoutePrefix("Employee")]
    public class EmployeeController : Controller
    {
        IEmployeeService employeeService;
        public EmployeeController()
        {
            employeeService = new EmployeeServer();
        }
        
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("List")]
        public JsonResult GetEmployeeList(Guid questionnaireID, Guid departmentID, Guid functionalGroupID, int isSubmit ,int pageIndex, int pageSize)
        {
            PageVO obj = null;
            JsonResult result = new JsonResult();
            int pageCount = 0;
            int code = 0;
            string msg = "";
            try
            {
                obj = employeeService.GetEmployeeList(questionnaireID, departmentID, functionalGroupID, isSubmit, pageIndex, pageSize);
                pageCount = obj.PageCount;
                code = Convert.ToInt32(ResultStatus.Success);
                msg = "查询成功";
            }
            catch (Exception e)
            {
                code = Convert.ToInt32(ResultStatus.Error);
                msg = "查询失败";
            }
            finally
            {
                result = Json(new { code = code, msg = msg, data = obj.EmployeeVOs, pageCount = pageCount }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpGet]
        [Route("AddressBook")]
        public JsonResult GetAddressBook(Guid departmentID, Guid functionalGroupID, string searchStr)
        {
            List<EmployeeVO> obj = null;
            JsonResult result = new JsonResult();
            int code = 0;
            string msg = "";
            List<AddressBookVO> letterList = new List<AddressBookVO>();
            try
            {
                obj = employeeService.GetAddressBook(departmentID, functionalGroupID, searchStr);
                for (char i = 'A'; i <= 'Z'; i++)
                {
                    AddressBookVO item = new AddressBookVO();
                    item.Letter = i.ToString();
                    item.EmployeeList = obj.Where(p => StringHelper.GetSpellCode(p.EmployeeName).Substring(0, 1) == i.ToString()).OrderBy(p=>p.EmployeeName).ToList();
                    letterList.Add(item);
                }
                code = Convert.ToInt32(ResultStatus.Success);
                msg = "查询成功";
            }
            catch (Exception e)
            {
                code = Convert.ToInt32(ResultStatus.Error);
                msg = "查询失败";
            }
            finally
            {
                result = Json(new { code = code, msg = msg, data = letterList }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [Route("EditPassword")]
        public JsonResult UpdatePassword(string employeeNumber,  string openid ,string oldPwd, string newPwd)
        {
            JsonResult result = new JsonResult();
            Employee obj = null;
            int code = 0;
            string msg = "";
            try
            {
                obj = employeeService.GetEmployeeByEmployeeNumber(employeeNumber, openid);
                if (!MD5.VerifyMd5Hash(oldPwd, obj.Password))
                {
                    msg = "密码不正确";
                    code = Convert.ToInt32(ResultStatus.Failure);
                }
                else
                {
                    employeeService.UpdatePassword(obj.EmployeeID, newPwd);
                    code = Convert.ToInt32(ResultStatus.Success);
                    msg = "修改成功";
                }
            }
            catch (Exception e)
            {
                code = Convert.ToInt32(ResultStatus.Error);
                msg = "修改失败";
            }
            finally
            {
                result = Json(new { code = code, msg = msg }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
    }
}