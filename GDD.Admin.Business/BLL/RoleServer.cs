using GDD.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDD.Models;
using GDD.Admin.Business.IBLL;
using GDD.Common;
using System.Data.Entity.Validation;
using GDD.Admin.VO;
using System.Transactions;

namespace GDD.Admin.Business.BLL
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class RoleServer : Repository, IRoleService
    {
        /// <summary>
        /// 获取角色集合
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public List<SYS_Role> GetRoleList(string name, int pageIndex, int pageSize)
        {
            try
            {
                using (var db = base.GDDSVSPDb)
                {
                    int isdel = Convert.ToInt32(IsDel.未删除);
                    IQueryable<SYS_Role> baseQuery = db.SYS_Role.Where(p => p.IsDel == isdel);
                    if (!string.IsNullOrEmpty(name))
                    {
                        baseQuery = baseQuery.Where(p => p.RoleName.Contains(name));
                    }
                    var RoleList = baseQuery.OrderBy(p => p.CreateTime).ThenBy(p => p.RoleID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    return RoleList;
                }
            }
            catch (DbEntityValidationException ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 获取角色总数
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <returns></returns>
        public int GetRoleCount(string name)
        {
            try
            {
                using (var db = base.GDDSVSPDb)
                {
                    int isdel = Convert.ToInt32(IsDel.未删除);
                    IQueryable<SYS_Role> baseQuery = db.SYS_Role.Where(p => p.IsDel == isdel);
                    if (!string.IsNullOrEmpty(name))
                    {
                        baseQuery = baseQuery.Where(p => p.RoleName.Contains(name));
                    }
                    var count = baseQuery.Count();
                    return count;
                }
            }
            catch (DbEntityValidationException ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public List<SYS_Role> GetRoles()
        {
            try
            {
                using (var db = base.GDDSVSPDb)
                {
                    int isdel = Convert.ToInt32(IsDel.未删除);
                    return db.SYS_Role.Where(p => p.IsDel == isdel).ToList();
                }
            }
            catch (DbEntityValidationException ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="role">角色对象</param>
        /// <returns></returns>
        public bool InsertRole(SYS_Role role)
        {
            try
            {
                using (var db = base.GDDSVSPDb)
                {
                    db.SYS_Role.Add(role);
                    return db.SaveChanges() > 0;
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="role">角色对象</param>
        /// <returns></returns>
        public bool UpdateRole(SYS_Role role)
        {
            try
            {
                using (var db = base.GDDSVSPDb)
                {
                    SYS_Role sysRole = db.SYS_Role.SingleOrDefault(p => p.RoleID == role.RoleID);
                    sysRole.RoleCode = role.RoleCode;
                    sysRole.RoleName = role.RoleName;
                    sysRole.Description = role.Description;
                    sysRole.Type = role.Type;
                    return db.SaveChanges() > 0;
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public bool DeleteRole(Guid id)
        {
            using (var db = base.GDDSVSPDb)
            {
                int isdel = Convert.ToInt32(IsDel.已删除);
                SYS_Role role = db.SYS_Role.SingleOrDefault(p => p.RoleID == id);
                role.IsDel = isdel;
                return db.SaveChanges() > 0;
            }
        }

        public bool UpdateRolePower(string roleCode, List<PowerTreeVO> powers)
        {
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                {
                    using (var db = base.GDDSVSPDb)
                    {
                        var baseQuery = db.SYS_REL_RoleMenuButton.Where(p => p.RoleCode == roleCode);
                        db.SYS_REL_RoleMenuButton.BulkDelete(baseQuery);
                        db.SaveChanges();
                        
                        List<SYS_REL_RoleMenuButton> rmb = new List<SYS_REL_RoleMenuButton>();
                        rmb = GetRoleMenuButtonList(roleCode,"", powers);
                        db.SYS_REL_RoleMenuButton.BulkInsert(rmb);
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

        public List<SYS_REL_RoleMenuButton> GetRoleMenuButtonList(string roleCode, string menuCode,List<PowerTreeVO> powers)
        {
            int isdel = Convert.ToInt32(IsDel.未删除);
            List<SYS_REL_RoleMenuButton> list = new List<SYS_REL_RoleMenuButton>();
            foreach (var item in powers)
            {
                //if (item.@checked)
                //{
                    SYS_REL_RoleMenuButton rmb = new SYS_REL_RoleMenuButton();
                    if (item.Type == "按钮")
                    {
                        rmb.BtnCode = item.Code;
                        rmb.MenuCode = menuCode;
                    }
                    else if (item.Type == "菜单")
                    {
                        rmb.MenuCode = item.Code;
                    }
                    rmb.ID = Guid.NewGuid();
                    rmb.RoleCode = roleCode;
                    rmb.State = isdel;
                    list.Add(rmb);   
                //}
                if (item.children != null)
                {
                    list = list.Concat(GetRoleMenuButtonList(roleCode, item.Code, item.children)).ToList();
                }
            }
            return list;
        }

        /// <summary>
        /// 获取角色权限集合
        /// </summary>
        /// <param name="roleCode">角色代码</param>
        /// <returns></returns>
        public List<SYS_REL_RoleMenuButton> GetRoleMenuButtonList(string roleCode)
        {
            using (var db = base.GDDSVSPDb)
            {
                //Guid newUserId = new Guid(userId);
                int isDel = Convert.ToInt32(IsDel.未删除);
                IQueryable<SYS_REL_RoleMenuButton>  rmb = db.SYS_REL_RoleMenuButton.Where(p => p.RoleCode == roleCode &&  p.State == isDel);
                return rmb.ToList();
            }
        }
    }
}
