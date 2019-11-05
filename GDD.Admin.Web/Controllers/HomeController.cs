using GDD.Admin.Business;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Common;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace GDD.Admin.Web.Controllers
{
    public class HomeController : Controller
    {
        IRoleService roleService = new RoleServer();
        public ActionResult Index()
        {
            SYS_User vo = new SYS_User();
            var user = (Session["user"] as SYS_User);
            //var user = (SYS_User)JsonCache.GetCache("user");
            if (user != null)
            {
                vo = user;
            }
            ViewData["User"] = vo;
            return View();
        }

        [HttpGet]
        public JsonResult SetThemeColor(string color)
        {
            ConfigHelper.SetAppSettingValue("theme_color", color);
            return Json(new { code = 0,  message = "success" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpGet]
        public string GetString()
        {
            string str = "";
            //取得当前方法命名空间
            str += "命名空间名:" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + "\n";
            //取得当前方法类全名 包括命名空间
            str += "命名空间+类名:" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "\n";
            //获得当前类名
            str += "类名:" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "\n";
            //取得当前方法名
            str += "方法名:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n";
            str += "\n";
            StackTrace ss = new StackTrace(true);
            MethodBase mb = ss.GetFrame(1).GetMethod();
            //取得父方法命名空间
            str += mb.DeclaringType.Namespace + "\n";
            //取得父方法类名
            str += mb.DeclaringType.Name + "\n";
            //取得父方法类全名
            str += mb.DeclaringType.FullName + "\n";
            //取得父方法名
            str += mb.Name + "\n";
            //取得当前方法的命名空间+类名+方法名
            str = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
            return str;
        }

        [HttpGet]
        public JsonResult GetRoles()
        {
            return Json(new { code = 0,  data = roleService.GetRoles(), message = "success"} , JsonRequestBehavior.AllowGet);
        }
    }
}