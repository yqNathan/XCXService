using AutoMapper;
using GDD.Admin.Business;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.VO;
using GDD.Admin.Web.Extensions;
using GDD.Admin.Web.Filter;
using GDD.Common;
using GDD.Core;
using GDD.Models;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace GDD.Admin.Web.Controllers
{
    [RoutePrefix("Employee")]
    public class EmployeeController : BaseController
    {
        IEmployeeService employeeService;
        private static readonly ILog log = LogManager.GetLogger(typeof(EmployeeController));
        public EmployeeController()
        {
            employeeService = new EmployeeServer();
        }

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmployeeDetail(string obj)
        {
            EmployeeVO vo = new EmployeeVO();
            if (!string.IsNullOrEmpty(obj))
            {
                vo = JsonConvert.DeserializeObject<EmployeeVO>(obj);
                ViewData["EmployeeData"] = vo;
            }
            else
            {
                vo.EmployeeID = "";
                ViewData["EmployeeData"] = vo;
            }
            return View();
        }

        [CheckFilter(IsCheck = false)]
        [HttpGet]
        [Route("Get")]
        public JsonResult GetEmployeeByEmployeeNumber(string employeeNumber, string password)
        {
            Employee obj = null;
            JsonResult result = new JsonResult();
            string msg = "";
            int code = 0;
            try
            {
                obj = employeeService.GetEmployeeByEmployeeNumber(employeeNumber);
                if (obj == null || string.IsNullOrEmpty(obj.EmployeeNumber))
                {
                    msg = "用户编号不存在";
                    code = 1;
                }
                else if (!MD5.VerifyMd5Hash(password, obj.Password))
                {
                    msg = "密码不正确";
                    code = 2;
                }
                else
                {
                    Session.Add("user", obj);
                    JsonCache.SetCache("employee", obj, System.DateTime.Now.AddMinutes(10), TimeSpan.Zero);
                    msg = "登录成功";
                }
                log.Info(msg);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                //result = Json(new { code = code, msg = msg,  data = obj }, JsonRequestBehavior.AllowGet);
                result = Json(new Result {  Code = code,  Msg = msg, Data = obj }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpGet]
        [Route("List")]
        public JsonResult GetEmployeeList(string name, int pageIndex, int pageSize)
        {
            List<Employee> list = null;
            List<EmployeeVO> listvo = null;
            JsonResult result = new JsonResult();
            int count = 0;
            try
            {
                list = employeeService.GetEmployeeList(name, pageIndex, pageSize);
                count = employeeService.GetEmployeeCount(name,Guid.Empty);
                listvo = Mapper.Map<List<EmployeeVO>>(list);
                log.Info("查询成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { code = 0, msg = "查询成功", count = count, data = listvo }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpGet]
        [Route("SubmittedList")]
        public JsonResult GetSubmittedEmployeeList(string name, Guid? departmentId, Guid? questionnaireId, int pageIndex, int pageSize , int isSubmit)
        {
            List<SubmittedEmployee> list = null;
            List<SubmittedEmployeeVO> listvo = null;
            JsonResult result = new JsonResult();
            int count = 0;
            try
            {
                list = employeeService.GetSubmittedEmployeeList(name, departmentId, questionnaireId, pageIndex, pageSize, isSubmit);
                count = employeeService.GetSubmittedEmployeeCount(name, departmentId, questionnaireId, isSubmit);
                listvo = Mapper.Map<List<SubmittedEmployeeVO>>(list);
                log.Info("查询成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { code = 0, msg = "查询成功", count = count, data = listvo }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [Route("Add")]
        public JsonResult InsertEmployee(Employee employee)
        {
            JsonResult result = new JsonResult();
            try
            {
                bool isSuccess = employeeService.InsertEmployee(employee);
                log.Info("添加成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { msg = "添加成功" }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [Route("Edit")]
        public JsonResult UpdateEmployee(Employee employee)
        {
            JsonResult result = new JsonResult();
            string msg = "";
            try
            {
                bool isSuccess = employeeService.UpdateEmployee(employee);
                if (isSuccess)
                {
                    msg = "修改成功";
                }
                else
                {
                    msg = "修改失败";
                }
                log.Info(msg);
            }
            catch (DbEntityValidationException e)
            {
                log.Error(e.Message);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { msg = msg }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [Route("Del")]
        public JsonResult DeleteEmployee(Guid id)
        {
            JsonResult result = new JsonResult();
            try
            {
                bool isSuccess = employeeService.DeleteEmployee(id);
                log.Info("删除成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { msg = "删除成功" }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
    }
}