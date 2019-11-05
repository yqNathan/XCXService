using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDD.Admin.Web.Filter
{
    public class CheckFilterAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// 检测是否登录全局过滤器 原理：Action过滤器
        /// </summary>
        public bool IsCheck { get; set; }//IsCheck用于不需要检测的界面 的字段

        /// <summary>
        /// action方法执行前调用
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsCheck)
            {
                //检测用户是否登录
                if (filterContext.HttpContext.Session["user"] == null)
                {
                    var controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["Controller"];
                    var actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["Action"];
                    
                    //this.ClientScript.RegisterStartupScript(this.GetType(), "<script language='javascript'>this.parent.location='/Account/Login" + "?returnUrl=" + controllerName + "/" + actionName + "&state=0';self.close();</script>");
                    filterContext.HttpContext.Response.Write("<script language='javascript'>this.parent.location='/Account/Login" + "?returnUrl=" + controllerName + "/" + actionName + "&state=0';self.close();</script>");
                    filterContext.Result = new ContentResult();
                    //filterContext.HttpContext.Response.Redirect("/Account/Login"+ "?returnUrl=" + controllerName+ "/"+actionName + "&state=0");
                }
            }
            base.OnActionExecuting(filterContext);
        }

        
        /// <summary>
        /// action方法执行前调用
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}