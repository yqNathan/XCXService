using AutoMapper;
using GDD.Admin.Business;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.VO;
using GDD.Common;
using GDD.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GDD.Admin.Web.Controllers
{
    [RoutePrefix("Menu")]
    public class MenuController : Controller
    {
        IMenuService menuService;
        private static readonly ILog log = LogManager.GetLogger(typeof(EmployeeController));
        int index = 0;
        public MenuController()
        {
            menuService = new MenuServer();
        }

        public MvcHtmlString CreateMenuHtml()
        {
            List<SYS_Menu> menus = menuService.GetRoleMenuList((Session["user"] as SYS_User)?.RoleCode);
            StringBuilder html = new StringBuilder();
            var rootMenus = menus.Where(p => p.MenuParentID == null || p.MenuParentID == Guid.Empty).OrderBy(x => x.OrderBy).ToList();
            foreach (var root in rootMenus)
            {
                CreateMenuItem(html, menus, root, false);
            }
            return new MvcHtmlString(html.ToString());
        }

        [HttpGet]
        [Route("List")]
        public JsonResult GetMenuList(string menuName, int pageIndex, int pageSize)
        {
            List<MenuVO> listvo = null;
            JsonResult result = new JsonResult();
            int count = 0;
            string msg = "ok";
            try
            {
                List<SYS_Menu> menuList = menuService.GetMenuList(menuName, pageIndex, pageSize);
                List<MenuVO> btnList = menuService.GetMenuButtonList(menuName);
                listvo = Mapper.Map<List<MenuVO>>(menuList);
                listvo = listvo.Concat(btnList).ToList();
                count = menuService.GetMenuCount(menuName);
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
        public JsonResult InsertMenu(SYS_Menu menu)
        {
            JsonResult result = new JsonResult();
            try
            {
                bool isSuccess = menuService.InsertMenu(menu);
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
        public JsonResult UpdateMenu(MenuVO menuVO)
        {
            JsonResult result = new JsonResult();
            string msg = "";
            try
            {
                bool isSuccess = menuService.UpdateMenu(menuVO);
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
        public JsonResult DeleteMenu(Guid id)
        {
            JsonResult result = new JsonResult();
            try
            {
                bool isSuccess = menuService.DeleteMenu(id);
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

        [HttpGet]
        [Route("Parent")]
        public JsonResult GetMenuParents()
        {
            List<SYS_Menu> list = null;
            List<MenuVO> listvo = null;
            JsonResult result = new JsonResult();
            string msg = "ok";
            try
            {
                list = menuService.GetMenuParents();
                listvo = Mapper.Map<List<MenuVO>>(list);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            finally
            {
                result = Json(new { code = 0, msg = msg, data = listvo }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        public string GetViewString(Controller controller, string viewName)
        {
            return RenderViewTostring.RenderPartialView(controller, viewName);
        }

        private void CreateMenuItem(StringBuilder html, List<SYS_Menu> menus, SYS_Menu menu, bool flag)
        {
            html.AppendLine("<li  class='layui-nav-item " + (index == 0 ? "layui-this" : "") + "'>");
            var childMenus = menus.Where(x => x.MenuParentID == menu.MenuID).OrderBy(x => x.OrderBy).ToList();
            if (index == 0)
            {
                html.AppendLine(@"<a title='" + menu.MenuName + "' href='javascript:;' class='menu site-demo-active' data-type='tabAdd' nonce='0'><i  class='layui-icon " + menu.IconCode + "'></i> <span>" + menu.MenuName + "</span></a>");
            }
            else if (!flag)
            {
                html.AppendLine(@"<a title='" + menu.MenuName + "'  href='javascript:;'  class='menu'><i  class='layui-icon " + menu.IconCode + "'></i> <span>" + menu.MenuName + "</span></a>");
            }
            else
            {
                html.AppendLine(@"<a title='" + menu.MenuName + "' href='javascript:;' lay-href='" + menu.MenuUrl + "' class='menu site-demo-active' data-type='tabAdd' nonce='" + index + "'> <span>" + menu.MenuName + "</span></a>");
            }
            index++;
            if (childMenus.Count != 0)
            {
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

        [HttpPost]
        [Route("EditMenuButton")]
        public JsonResult UpdateMenuButton(string menuCode, List<string> arr)
        {
            JsonResult result = new JsonResult();
            string msg = "";
            try
            {
                if (arr == null)
                {
                    arr = new List<string>();
                }
                bool isSuccess = menuService.UpdateMenuButton(menuCode, arr);
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
    }
}