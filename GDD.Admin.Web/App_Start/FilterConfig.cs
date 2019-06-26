using GDD.Admin.Web.Filter;
using System.Web;
using System.Web.Mvc;

namespace GDD.Admin.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CheckFilterAttribute() { IsCheck = true });
        }
    }
}
