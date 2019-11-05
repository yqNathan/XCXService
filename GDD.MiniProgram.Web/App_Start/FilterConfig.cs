using GDD.MiniProgram.Web.Filter;
using System.Web;
using System.Web.Mvc;

namespace GDD.MiniProgram.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CheckLoginFilterAttribute() { IsCheck = true });
        }
    }
}
