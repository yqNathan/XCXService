using GDD.Admin.Business;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Common;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GDD.Admin.Web.Controllers
{
    public class MenuController : Controller
    {
        int index = 0;
        public MvcHtmlString Menu()
        {
            IMenuService menuService = new MenuServer();
            return CreateMenuHtml(menuService.GetMenuList());
        }

        public string GetViewString(Controller controller ,string viewName)
        {
            return RenderViewTostring.RenderPartialView(controller, viewName);
        }

        private MvcHtmlString CreateMenuHtml(List<Menu> menus)
        {
            StringBuilder html = new StringBuilder();

            var rootMenus = menus.Where(p=>p.MenuParentID == null).OrderBy(x => x.OrderBy).ToList();
            
            foreach (var root in rootMenus)
            {
                CreateMenuItem(html, menus, root ,false);
            }

            return new MvcHtmlString(html.ToString());
        }

        private void CreateMenuItem(StringBuilder html, List<Menu> menus, Menu menu,bool flag)
        {
            html.AppendLine("<li class='layui-nav-item "+ (index==0 ? "layui-this" : "") + "'>");
            var childMenus = menus.Where(x => x.MenuParentID == menu.MenuID).OrderBy(x => x.OrderBy).ToList();
            if (index == 0)
            {
                html.AppendLine(@"<a  href='javascript:;' class='site-demo-active' data-type='tabAdd' nonce='0'>" + menu.MenuName + "</a>");
            }
            else if (!flag)
            {
                html.AppendLine(@"<a  href='javascript:;'>" + menu.MenuName + "</a>");
            }
            else
            {
                html.AppendLine(@"<a href='javascript:;' lay-href='" + menu.MenuUrl + "' class='site-demo-active' data-type='tabAdd' nonce='" + index + "'>" + menu.MenuName + "</a>");
            }
            index++;
            if (childMenus.Count != 0) {
                html.AppendLine("<dl class='layui-nav-child'>");
                foreach (var child in childMenus)
                {
                    html.AppendLine("<dd>");
                    CreateMenuItem(html, menus, child, true);
                    html.AppendLine("</dd>");
                }
                html.AppendLine("</dl>");
            }
            html.AppendLine("</li>");
        }
    }
}