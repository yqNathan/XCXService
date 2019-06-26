using GDD.Admin.Business;
using GDD.Common;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace GDD.Admin.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/log4net.config")));
            AutoMapperConfiguration.Configure();//AutoMapper配置
            LogHelper.Default.WriteInfo("AppStart");
            LogHelper.Default.WriteWarning("Warning");
            LogHelper.Default.WriteError("Error");
            LogHelper.Default.WriteFatal("Fatal");
        }
    }
}
