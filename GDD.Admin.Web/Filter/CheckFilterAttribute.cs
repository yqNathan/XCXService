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
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (IsCheck)
            {
                //检测用户是否登录
                if (filterContext.HttpContext.Session["user"] == null)
                {
                    var controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["Controller"];
                    var actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["Action"];
                    filterContext.HttpContext.Response.Write("<script>this.parent.location='/Account/Login" + "?returnUrl=" + controllerName + "/" + actionName + "&state=0';self.close();</script>");
                    //filterContext.HttpContext.Response.Redirect("/Account/Login"+ "?returnUrl=" + controllerName+ "/"+actionName + "&state=0");
                }
            }

        }
    }
}