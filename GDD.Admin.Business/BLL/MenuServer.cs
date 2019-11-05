using EntityFramework.Extensions;
using GDD.Admin.Business.IBLL;
using GDD.Admin.VO;
using GDD.Business;
using GDD.Common;
using GDD.Core;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GDD.Admin.Business.BLL
{
    public class MenuServer : Repository, IMenuService
    {
        /// <summary>
        /// 获取菜单集合
        /// </summary>
        /// <param name="menuName">菜单名称</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public List<SYS_Menu> GetMenuList(string menuName, int pageIndex, int pageSize)
        {
            using (var db = base.GDDSVSPDb)
            {
                //Guid newUserId = new Guid(userId);
                int isDel = Convert.ToInt32(IsDel.未删除);
                IQueryable<SYS_Menu> menu = db.SYS_Menu;
                if (!string.IsNullOrEmpty(menuName))
                {
                    menu = menu.Where(p => p.MenuName.Contains(menuName));
                }
                var menuList = menu.OrderBy(p => p.OrderBy).ThenBy(p => p.MenuID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return menuList;
            }
        }

        /// <summary>
        /// 获取权限菜单集合
        /// </summary>
        /// <param name="roleCode">角色代码</param>
        /// <returns></returns>
        public List<SYS_Menu> GetRoleMenuList(string roleCode)
        {
            using (var db = base.GDDSVSPDb)
            {
                //Guid newUserId = new Guid(userId);
                int isDel = Convert.ToInt32(IsDel.未删除);
                IQueryable<SYS_Menu> menu = db.SYS_Menu;
                var list  = db.SYS_REL_RoleMenuButton.Where(p=>p.RoleCode == roleCode && p.State == isDel).ToList();
                var rmblist = list.Where((x, i) => list.FindIndex(p => p.MenuCode == x.MenuCode) == i).ToList();
                var menuList = (from rmb in rmblist
                                join m1 in menu on rmb.MenuCode equals m1.MenuCode into data1
                                from obj1 in data1.DefaultIfEmpty()
                                select new SYS_Menu
                                {
                                     MenuCode = obj1.MenuCode,
                                     MenuID = obj1.MenuID,
                                     IconCode = obj1.IconCode,
                                     MenuName = obj1.MenuName,
                                     MenuParentID = obj1.MenuParentID,
                                     MenuUrl = obj1.MenuUrl,
                                     OrderBy = obj1.OrderBy
                                }).ToList();
                return menuList;
            }
        }


        /// <summary>
        /// 获取菜单按钮集合
        /// </summary>
        /// <param name="menuName">菜单名称</param>
        /// <returns></returns>
        public List<MenuVO> GetMenuButtonList(string menuName)
        {
            using (var db = base.GDDSVSPDb)
            {
                //Guid newUserId = new Guid(userId);
                int isDel = Convert.ToInt32(IsDel.未删除);
                IQueryable<SYS_Menu> menu = db.SYS_Menu;
                IQueryable<SYS_REL_MenuButton> menuButton = db.SYS_REL_MenuButton.Where(p => p.State == isDel);
                IQueryable<SYS_Button> button = db.SYS_Button.Where(p => p.IsDel == isDel);
                if (!string.IsNullOrEmpty(menuName))
                {
                    menu = menu.Where(p => p.MenuName.Contains(menuName));
                }
                var buttonList = (from mb1 in menuButton
                                  join m1 in menu on mb1.MenuCode equals m1.MenuCode into data1
                                  from obj1 in data1.DefaultIfEmpty()
                                  join b1 in button on mb1.BtnCode equals b1.BtnCode into data2
                                  from obj2 in data2.DefaultIfEmpty()
                                  select new MenuVO
                                  {
                                      MenuCode = mb1.BtnCode,
                                      IconCode = obj2.BtnIcon,
                                      MenuID = obj2.ButtonID,
                                      MenuName = obj2.BtnName,
                                      MenuParentID = obj1.MenuID,
                                      MenuUrl = "",
                                      OrderBy = obj2.BtnOrder,
                                      MenuParentName = obj1.MenuName,
                                      Type = "按钮"
                                  }).OrderBy(p=>p.OrderBy).ToList();
                return buttonList;
            }
        }

        


        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu">菜单对象</param>
        /// <returns></returns>
        public bool InsertMenu(SYS_Menu menu)
        {
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                {
                    using (var db = base.GDDSVSPDb)
                    {
                        menu.MenuID = Guid.NewGuid();
                        db.SYS_Menu.Add(menu);

                        int ndel = Convert.ToInt32(IsDel.未删除);
                        int isdel = Convert.ToInt32(IsDel.已删除);
                        List<SYS_Button> btnList = db.SYS_Button.Where(p => p.IsDel == ndel).ToList();
                        List<SYS_REL_MenuButton> mblist = new List<SYS_REL_MenuButton>();
                        foreach (var item in btnList)
                        {
                            SYS_REL_MenuButton mb = new SYS_REL_MenuButton()
                            {
                                ID = Guid.NewGuid(),
                                BtnCode = item.BtnCode,
                                MenuCode = menu.MenuCode,
                                State = isdel
                            };
                            mblist.Add(mb);
                        }
                        db.SYS_REL_MenuButton.BulkInsert(mblist);
                        db.BulkSaveChanges();
                    }
                    transaction.Complete();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="obj">菜单对象</param>
        /// <returns></returns>
        public bool UpdateMenu(MenuVO obj)
        {
            try
            {
                using (var db = base.GDDSVSPDb)
                {
                    SYS_Menu menu = db.SYS_Menu.SingleOrDefault(p => p.MenuID == obj.MenuID);
                    menu.MenuID = obj.MenuID;
                    menu.MenuName = obj.MenuName;
                    menu.MenuParentID = obj.MenuParentID;
                    menu.MenuCode = obj.MenuCode;
                    menu.MenuUrl = obj.MenuUrl;
                    menu.OrderBy = obj.OrderBy;
                    menu.IconCode = obj.IconCode;
                    return db.SaveChanges() > 0;
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id">菜单ID</param>
        /// <returns></returns>
        public bool DeleteMenu(Guid id)
        {
            using (var db = base.GDDSVSPDb)
            {
                SYS_Menu menu = db.SYS_Menu.SingleOrDefault(p => p.MenuID == id);
                db.SYS_Menu.Remove(menu);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 获取父级菜单集合
        /// </summary>
        /// <returns></returns>
        public List<SYS_Menu> GetMenuParents()
        {
            using (var db = base.GDDSVSPDb)
            {
                //Guid newUserId = new Guid(userId);
                List<SYS_Menu> list = db.SYS_Menu.Where(p => p.MenuParentID == null || p.MenuParentID == Guid.Empty).OrderBy(p => p.OrderBy).ThenBy(p => p.MenuID).ToList();
                return list;
            }
        }

        /// <summary>
        /// 通过ID获取菜单名称
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns></returns>
        public string GetMenuNameById(Guid? menuId)
        {
            using (var db = base.GDDSVSPDb)
            {
                //Guid newUserId = new Guid(userId);
                IQueryable<SYS_Menu> baseQuery = db.SYS_Menu;
                string menuName = "";
                if (menuId != Guid.Empty && menuId != null)
                {
                    menuName = baseQuery.FirstOrDefault(p => p.MenuID == menuId).MenuName;
                }
                return menuName;
            }
        }

        /// <summary>
        /// 获取菜单总数
        /// </summary>
        /// <param name="menuName">菜单名称</param>
        /// <returns></returns>
        public int GetMenuCount(string menuName)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<SYS_Menu> baseQuery = db.SYS_Menu;
                if (!string.IsNullOrEmpty(menuName))
                {
                    baseQuery = baseQuery.Where(p => p.MenuName.Contains(menuName));
                }
                var count = baseQuery.Count();
                return count;
            }
        }

        /// <summary>
        /// 修改菜单下的按钮
        /// </summary>
        /// <param name="menuCode">菜单对象</param>
        /// <param name="arr">按钮数组</param>
        /// <returns></returns>
        public bool UpdateMenuButton(string menuCode, List<string> arr)
        {
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                {
                    using (var db = base.GDDSVSPDb)
                    {
                        db.SYS_REL_MenuButton.Where(p => p.MenuCode == menuCode && arr.Contains(p.BtnCode)).UpdateFromQuery(p => new SYS_REL_MenuButton { State = 0 });
                        db.SYS_REL_MenuButton.Where(p => p.MenuCode == menuCode && !arr.Contains(p.BtnCode)).UpdateFromQuery(p => new SYS_REL_MenuButton { State = 1 });
                    }
                    transaction.Complete();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            return true;
        }


    }
}
