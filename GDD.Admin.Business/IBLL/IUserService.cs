using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.IBLL
{
    public interface IUserService
    {
        /// <summary>
        /// 获取用户集合
        /// </summary>
        /// <param name="name">用户名称</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        List<SYS_User> GetUserList(string name, int pageIndex, int pageSize);

        /// <summary>
        /// 获取用户总数
        /// </summary>
        /// <param name="name">用户名称</param>
        /// <returns></returns>
        int GetUserCount(string name);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="User">用户对象</param>
        /// <returns></returns>
        bool InsertUser(SYS_User User);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="User">用户对象</param>
        /// <returns></returns>
        bool UpdateUser(SYS_User User);


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        bool DeleteUser(Guid id);

        /// <summary>
        /// 通过用户账号获取用户信息
        /// </summary>
        /// <param name="account">用户账号</param>
        /// <returns></returns>
        SYS_User GetUserByAccount(string account);
    }
}
