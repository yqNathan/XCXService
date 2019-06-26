using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDD.Admin.Web.Extensions
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext ctx)
        {
            base.OnActionExecuted(ctx);
            if (Request == null)
            {
            }
        }
    }
}