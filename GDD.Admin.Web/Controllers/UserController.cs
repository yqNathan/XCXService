using AutoMapper;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.VO;
using GDD.Admin.Web.Extensions;
using GDD.Admin.Web.Filter;
using GDD.Common;
using GDD.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDD.Admin.Web.Controllers
{
    [RoutePrefix("User")]
    public class UserController : BaseController
    {
        IUserService userService;
        private static readonly ILog log = LogManager.GetLogger(typeof(UserController));

        public UserController()
        {
            userService = new UserServer();
        }
        // GET: User
        public ActionResult UserList()
        {
            return View();
        }

        [HttpGet]
        [Route("List")]
        public JsonResult GetUserList(string name, int pageIndex, int pageSize)
        {
            List<SYS_User> list = null;
            List<UserVO> listvo = null;
            JsonResult result = new JsonResult();
            int count = 0;
            string msg = "";
            try
            {
                list = userService.GetUserList(name, pageIndex, pageSize);
                count = userService.GetUserCount(name);
                listvo = Mapper.Map<List<UserVO>>(list);
                msg = "success";
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            finally
            {
                result = Json(new { code = 0, msg = msg, count = count, data = listvo }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }


        [HttpPost]
        [Route("Add")]
        public JsonResult InsertUser(SYS_User user)
        {
            JsonResult result = new JsonResult();
            try
            {
                user.UserID = Guid.NewGuid();
                user.UserPassword = MD5.GetMd5Hash(user.UserPassword);
                user.CreateTime = DateTime.Now;
                user.Creator = (Session["user"] as SYS_User)?.UserName;
                bool isSuccess = userService.InsertUser(user);
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
        public JsonResult UpdateUser(SYS_User user)
        {
            JsonResult result = new JsonResult();
            string msg = "";
            try
            {
                user.UserPassword = MD5.GetMd5Hash(user.UserPassword);
                user.ModifiedTime = DateTime.Now;
                user.Modifier = (Session["user"] as SYS_User)?.UserName;
                bool isSuccess = userService.UpdateUser(user);
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
        public JsonResult DeleteUser(Guid id)
        {
            JsonResult result = new JsonResult();
            try
            {
                bool isSuccess = userService.DeleteUser(id);
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

        [CheckFilter(IsCheck = false)]
        [HttpGet]
        [Route("Get")]
        public JsonResult GetEmployeeByEmployeeNumber(string account, string password)
        {
            SYS_User obj = null;
            JsonResult result = new JsonResult();
            string msg = "";
            int code = 0;
            try
            {
                obj = userService.GetUserByAccount(account);
                if (obj == null || string.IsNullOrEmpty(obj.UserAccount))
                {
                    msg = "用户编号不存在";
                    code = 1;
                }
                else if (!MD5.VerifyMd5Hash(password, obj.UserPassword))
                {                   
                    msg = "密码不正确";
                    code = 2;
                }
                else
                {
                    Session.Add("user", obj);
                    JsonCache.SetCache("user", obj, System.DateTime.Now.AddMinutes(10), TimeSpan.Zero);
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
                result = Json(new { Code = code, Msg = msg, Data = obj, SessionID = Session.SessionID }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

    }
}