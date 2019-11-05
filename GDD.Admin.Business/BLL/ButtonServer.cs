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
    public class ButtonServer : Repository, IButtonService
    {
        /// <summary>
        /// 获取按钮集合
        /// </summary>
        /// <param name="ButtonName">按钮名称</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public List<SYS_Button> GetButtonList(string ButtonName, int pageIndex, int pageSize)
        {
            using (var db = base.GDDSVSPDb)
            {
                //Guid newUserId = new Guid(userId);
                int isdel = Convert.ToInt32(IsDel.未删除);
                IQueryable<SYS_Button> baseQuery = db.SYS_Button.Where(p=>p.IsDel == isdel);
                if (!string.IsNullOrEmpty(ButtonName))
                {
                    baseQuery = baseQuery.Where(p => p.BtnName.Contains(ButtonName));
                }
                var ButtonList = baseQuery.OrderBy(p => p.BtnOrder).ThenBy(p => p.ButtonID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return ButtonList;
            }
        }

        /// <summary>
        /// 通过菜单代码获取按钮集合
        /// </summary>
        /// <param name="menuCode">菜单代码</param>
        /// <returns></returns>
        public List<CheckBoxButtonVO> GetMenuButtonList(string menuCode)
        {
            using (var db = base.GDDSVSPDb)
            {
                //Guid newUserId = new Guid(userId);
                int isdel = Convert.ToInt32(IsDel.未删除);
                IQueryable<SYS_REL_MenuButton> menuButton = db.SYS_REL_MenuButton.Where(p => p.MenuCode == menuCode && p.State == isdel);
                IQueryable<SYS_Button> button = db.SYS_Button.Where(p => p.IsDel == isdel);
                var buttonList = (from b1 in button
                                  join m1 in menuButton on b1.BtnCode equals m1.BtnCode into data1
                                  from obj1 in data1.DefaultIfEmpty()
                                  select new CheckBoxButtonVO {
                                      ID = b1.ButtonID,
                                      Name = b1.BtnName,
                                      Code = b1.BtnCode,
                                      State = obj1.State == null ? 1 : obj1.State,
                                      BtnOrder = b1.BtnOrder
                                 }).OrderBy(p=>p.BtnOrder).ToList();
                return buttonList;
            }
        }

        /// <summary>
        /// 通过菜单代码获取按钮集合
        /// </summary>
        /// <param name="menuCode">菜单代码</param>
        /// <returns></returns>
        public List<SYS_Button> GetButtonListByRoleMenu(string menuCode,string roleCode)
        {
            using (var db = base.GDDSVSPDb)
            {
                //Guid newUserId = new Guid(userId);
                int isdel = Convert.ToInt32(IsDel.未删除);
                IQueryable<SYS_REL_RoleMenuButton> sysrmb = db.SYS_REL_RoleMenuButton.Where(p => p.MenuCode == menuCode && p.RoleCode == roleCode && p.State == isdel && p.BtnCode != null && p.BtnCode != string.Empty);
                IQueryable<SYS_Button> button = db.SYS_Button.Where(p => p.IsDel == isdel);
                var buttonList = (from rmb in sysrmb
                                  join btn in button on rmb.BtnCode equals btn.BtnCode into data1
                                  from obj1 in data1.DefaultIfEmpty()
                                  select obj1).OrderBy(p => p.BtnOrder).ToList();
                return buttonList;
            }
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="Button">按钮对象</param>
        /// <returns></returns>
        public bool InsertButton(SYS_Button Button)
        {
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                {
                    using (var db = base.GDDSVSPDb)
                    {
                        db.SYS_Button.Add(Button);
                        db.SaveChanges();

                        List<SYS_Menu> menuList = db.SYS_Menu.ToList();
                        List<SYS_REL_MenuButton> mblist = new List<SYS_REL_MenuButton>();
                        int isdel = Convert.ToInt32(IsDel.已删除);
                        foreach (var menu in menuList)
                        {
                            SYS_REL_MenuButton mb = new SYS_REL_MenuButton() {
                                ID = Guid.NewGuid(),
                                BtnCode = Button.BtnCode,
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
        /// 修改按钮
        /// </summary>
        /// <param name="obj">按钮对象</param>
        /// <returns></returns>
        public bool UpdateButton(SYS_Button obj)
        {
            try
            {
                using (var db = base.GDDSVSPDb)
                {
                    int isdel = Convert.ToInt32(IsDel.未删除);
                    SYS_Button Button = db.SYS_Button.SingleOrDefault(p => p.ButtonID == obj.ButtonID && p.IsDel == isdel);
                    Button.BtnCode = obj.BtnCode;
                    Button.BtnName = obj.BtnName;
                    Button.BtnOrder = obj.BtnOrder;
                    Button.BtnDescribe = obj.BtnDescribe;
                    Button.BtnIcon = obj.BtnIcon;
                    Button.BtnType = obj.BtnType;
                    Button.ShowType = obj.ShowType;
                    return db.SaveChanges() > 0;
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="id">按钮ID</param>
        /// <returns></returns>
        public bool DeleteButton(Guid id)
        {
            using (var db = base.GDDSVSPDb)
            {
                int isdel = Convert.ToInt32(IsDel.未删除);
                SYS_Button Button = db.SYS_Button.SingleOrDefault(p => p.ButtonID == id && p.IsDel == isdel);
                Button.IsDel = Convert.ToInt32(IsDel.已删除);
                return db.SaveChanges() > 0;
            }
        }



        /// <summary>
        /// 通过ID获取按钮名称
        /// </summary>
        /// <param name="ButtonId">按钮ID</param>
        /// <returns></returns>
        public string GetButtonNameById(Guid? ButtonId)
        {
            using (var db = base.GDDSVSPDb)
            {
                int isdel = Convert.ToInt32(IsDel.未删除);
                IQueryable<SYS_Button> baseQuery = db.SYS_Button.Where(p => p.IsDel == isdel);
                string ButtonName = "";
                if (ButtonId != Guid.Empty && ButtonId != null)
                {
                    ButtonName = baseQuery.FirstOrDefault(p => p.ButtonID == ButtonId).BtnName;
                }
                return ButtonName;
            }
        }

        /// <summary>
        /// 获取按钮总数
        /// </summary>
        /// <param name="ButtonName">按钮名称</param>
        /// <returns></returns>
        public int GetButtonCount(string ButtonName)
        {
            using (var db = base.GDDSVSPDb)
            {
                int isdel = Convert.ToInt32(IsDel.未删除);
                IQueryable<SYS_Button> baseQuery = db.SYS_Button.Where(p => p.IsDel == isdel);
                if (!string.IsNullOrEmpty(ButtonName))
                {
                    baseQuery = baseQuery.Where(p => p.BtnName.Contains(ButtonName));
                }
                var count = baseQuery.Count();
                return count;
            }
        }
    }
}
