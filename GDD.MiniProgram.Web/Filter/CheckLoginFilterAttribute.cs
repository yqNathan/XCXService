using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GDD.MiniProgram.Web.Filter
{
    public class CheckLoginFilterAttribute : ActionFilterAttribute
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
                var authorization = HttpContext.Current.Request.Headers[HttpRequestHeader.Authorization.ToString()]?.ToString();
                if (string.IsNullOrEmpty(authorization))
                {
                    var controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["Controller"];
                    var actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["Action"];
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Client Certificate Required");//401无权限访问
                }
                //检测用户是否登录
                if (filterContext.HttpContext.Session[authorization] == null)
                {
                    var controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["Controller"];
                    var actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["Action"];
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Client Certificate Required");//401无权限访问
                }
            }

        }
    }
}