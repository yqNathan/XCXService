using GDD.Admin.Business.IBLL;
using GDD.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDD.Models;
using GDD.Common;
using System.Data.Entity.Validation;

namespace GDD.Admin.Business.BLL
{
    public class UserServer : Repository, IUserService
    {
       /// <summary>
       /// 获取用户集合
       /// </summary>
       /// <param name="name">用户名称</param>
       /// <param name="pageIndex">当前页</param>
       /// <param name="pageSize">分页大小</param>
       /// <returns></returns>
        public List<SYS_User> GetUserList(string name, int pageIndex, int pageSize)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<SYS_User> baseQuery = db.SYS_User;
                if (!string.IsNullOrEmpty(name))
                {
                    baseQuery = baseQuery.Where(p => p.UserName == name);
                }
                var list = baseQuery.OrderByDescending(p => p.CreateTime).ThenBy(p=>p.UserID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取用户数量
        /// </summary>
        /// <param name="name">用户名称</param>
        /// <returns></returns>
        public int GetUserCount(string name)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<SYS_User> baseQuery = db.SYS_User;
                if (!string.IsNullOrEmpty(name))
                {
                    baseQuery = baseQuery.Where(p => p.UserName == name);
                }
                var list = baseQuery.OrderByDescending(p => p.CreateTime).ThenBy(p => p.UserID).Count();
                return list;
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public bool InsertUser(SYS_User user)
        {
            using (var db = base.GDDSVSPDb)
            {
                db.SYS_User.Add(user);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public bool UpdateUser(SYS_User user)
        {
            try
            {
                using (var db = base.GDDSVSPDb)
                {
                    SYS_User obj = db.SYS_User.SingleOrDefault(p => p.UserID == user.UserID);
                    obj.UserAccount = user.UserAccount;
                    obj.UserName = user.UserName;
                    obj.UserPassword = user.UserPassword;
                    obj.RoleCode = user.RoleCode;
                    obj.ModifiedTime = user.ModifiedTime;
                    obj.Modifier = user.Modifier;
                    return db.SaveChanges() > 0;
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public bool DeleteUser(Guid id)
        {
            using (var db = base.GDDSVSPDb)
            {
                SYS_User user = db.SYS_User.SingleOrDefault(p => p.UserID == id);
                db.SYS_User.Remove(user);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 通过用户账号获取用户信息
        /// </summary>
        /// <param name="account">用户账号</param>
        /// <returns></returns>
        public SYS_User GetUserByAccount(string account)
        {
            using (var db = base.GDDSVSPDb)
            {
                SYS_User user = new SYS_User();
                if (!String.IsNullOrEmpty(account))
                {
                    user = db.SYS_User.SingleOrDefault(p => p.UserAccount.Equals(account));
                }
                return user;
            }
        }
    }
}
