using GDD.Common;
using GDD.MiniProgram.Business.BLL;
using GDD.MiniProgram.Business.IBLL;
using GDD.MiniProgram.Web.Filter;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDD.MiniProgram.Web.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : Controller
    {
        IEmployeeService employeeService;
        IDepartmentService departmentService;
        IFunctionalGroupService functionalgroupService;
        public AccountController()
        {
            employeeService = new EmployeeServer();
            departmentService = new DepartmentServer();
            functionalgroupService = new FunctionalGroupServer();
        }

        // GET: Account
        [CheckLoginFilterAttribute(IsCheck = false)]
        [HttpGet]
        [Route("Login")]
        public JsonResult Login(string employeeNumber, string password , string openid , bool isLogin)
        {
            Employee obj = null;
            var dName = "";
            var fName = "";
            JsonResult result = new JsonResult();
            string msg = "";
            int code = 0;
            try
            {
                obj = employeeService.GetEmployeeByEmployeeNumber(employeeNumber, openid);
                if (!isLogin && obj == null)
                {
                    code = -1;
                    msg = "请重新登录";
                }
                else if (!isLogin && obj != null)
                {
                    Session.Add(obj.EmployeeID.ToString(), obj);
                    msg = "登录成功";
                }
                else if (isLogin)
                {
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
                        employeeService.UpdateEmployeeOpenIdById(obj.EmployeeID, openid);
                        Session.Add(obj.EmployeeID.ToString(), obj);
                        //Session.Timeout = 30;
                        //JsonCache.SetCache("employee", obj, System.DateTime.Now.AddMinutes(10), TimeSpan.Zero);
                        msg = "登录成功";
                    }
                }
                if (obj != null)
                {
                    dName = departmentService.GetDepartmentNameById(obj.DepartmentID);
                    fName = functionalgroupService.GetFunctionalgroupNameById(obj.FunctionalgroupID);
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                //result = Json(new { code = code, msg = msg,  data = obj }, JsonRequestBehavior.AllowGet);
                result = Json(new { Code = code, Msg = msg, Data = obj, SessionID = Session.SessionID , DepartmentName = dName , FunctionalGroupName = fName}, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
    }
}