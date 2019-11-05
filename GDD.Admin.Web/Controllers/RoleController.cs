using AutoMapper;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.VO;
using GDD.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDD.Admin.Web.Controllers
{
    [RoutePrefix("Role")]
    public class RoleController : Controller
    {
        IMenuService menuService;
        IRoleService roleService;
        int treeid = 0;
        private static readonly ILog log = LogManager.GetLogger(typeof(EmployeeController));

        public RoleController()
        {
            roleService = new RoleServer();
            menuService = new MenuServer();
        }
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("List")]
        public JsonResult GetRoleList(string name, int pageIndex, int pageSize)
        {
            List<SYS_Role> list = null;
            List<RoleVO> listvo = null;
            JsonResult result = new JsonResult();
            int count = 0;
            string msg = "";
            try
            {
                list = roleService.GetRoleList(name, pageIndex, pageSize);
                count = roleService.GetRoleCount(name);
                listvo = Mapper.Map<List<RoleVO>>(list);
                msg = "success";
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
        public JsonResult InsertEmployee(SYS_Role btn)
        {
            JsonResult result = new JsonResult();
            try
            {
                btn.RoleID = Guid.NewGuid();
                btn.IsDel = 0;
                btn.CreateTime = DateTime.Now;
                btn.Creator = (Session["user"] as SYS_User)?.UserName;
                bool isSuccess = roleService.InsertRole(btn);
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
        public JsonResult UpdateRole(SYS_Role Role)
        {
            JsonResult result = new JsonResult();
            string msg = "";
            try
            {
                Role.ModifiedTime = DateTime.Now;
                Role.Modifier = (Session["user"] as SYS_User)?.UserName;
                bool isSuccess = roleService.UpdateRole(Role);
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
        public JsonResult DeleteRole(Guid id)
        {
            JsonResult result = new JsonResult();
            try
            {
                bool isSuccess = roleService.DeleteRole(id);
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
        [Route("RoleMenu")]
        public JsonResult GetRoleMenu(string roleCode)
        {
            List<PowerTreeVO> treeList = new List<PowerTreeVO>(); 
            List<MenuVO> listvo = null;
            JsonResult result = new JsonResult();
            string msg = "ok";
            treeid = 0;
            try
            {
                List<SYS_Menu> menuList = menuService.GetMenuList("", 1, int.MaxValue);
                List<MenuVO> btnList = menuService.GetMenuButtonList("");
                listvo = Mapper.Map<List<MenuVO>>(menuList);
                listvo = listvo.Concat(btnList).ToList();
                List<SYS_REL_RoleMenuButton> rmbList = roleService.GetRoleMenuButtonList(roleCode);
                treeList = GetPowerTreeVO(string.Empty,Guid.Empty,listvo, rmbList);
                
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            finally
            {
                result = Json(new { code = 0, msg = msg, data = treeList }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        public List<PowerTreeVO> GetPowerTreeVO(string menuParentCode, Guid menuParentID, List<MenuVO> menus, List<SYS_REL_RoleMenuButton> rmbList)
        {
            List<PowerTreeVO> treeList = new List<PowerTreeVO>();
            var pMenus = new List<MenuVO>();
            if (menuParentID == null || menuParentID == Guid.Empty)
            {
                pMenus = menus.Where(p => p.MenuParentID == null || p.MenuParentID == Guid.Empty).ToList();
            }
            else
            {
                pMenus = menus.Where(p => p.MenuParentID == menuParentID).ToList();
            }
            int length = pMenus.Count;
            for (int i = 0; i < length; i++)
            {
                var flag = false;
                int itemCount = menus.Where(p => p.MenuParentID == pMenus[i].MenuID).Count();
                if (pMenus[i].Type == "菜单")
                {
                    var count = rmbList.Where(p => p.MenuCode == pMenus[i].MenuCode).Count();
                    if (count > 0 && itemCount == 0)
                    {
                        flag = true;
                    }
                }
                else if (pMenus[i].Type == "按钮")
                {
                    var count = rmbList.Where(p => p.MenuCode == menuParentCode && p.BtnCode == pMenus[i].MenuCode).Count();
                    if (count > 0)
                    {
                        flag = true;
                    }
                }
                PowerTreeVO vo = new PowerTreeVO();
                vo.id = treeid++;
                vo.title = pMenus[i].MenuName;
                vo.Code = pMenus[i].MenuCode;
                vo.Type = pMenus[i].Type;
                vo.@checked = flag;
                vo.ParentId = menuParentID;
                if (itemCount == 0)
                {
                    vo.children = new List<PowerTreeVO>();
                }
                else
                {
                    vo.children = GetPowerTreeVO(pMenus[i].MenuCode, pMenus[i].MenuID, menus, rmbList);
                }
                treeList.Add(vo);
            }
            return treeList;
        }


        [HttpPost]
        [Route("EditRolePower")]
        public JsonResult UpdateRolePower(string roleCode,List<PowerTreeVO> powers)
        {
            JsonResult result = new JsonResult();
            string msg = "";
            try
            {
                bool isSuccess = roleService.UpdateRolePower(roleCode, powers);
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