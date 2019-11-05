using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDD.Admin.VO;

namespace GDD.Admin.Business.IBLL
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public interface IRoleService
    {
        List<SYS_Role> GetRoles();
        List<SYS_Role> GetRoleList(string name, int pageIndex, int pageSize);
        int GetRoleCount(string name);
        bool InsertRole(SYS_Role btn);
        bool UpdateRole(SYS_Role role);
        bool DeleteRole(Guid id);
        bool UpdateRolePower(string roleCode, List<PowerTreeVO> powers);

        /// <summary>
        /// 获取角色权限集合
        /// </summary>
        /// <param name="roleCode">角色代码</param>
        /// <returns></returns>
        List<SYS_REL_RoleMenuButton> GetRoleMenuButtonList(string roleCode);
    }
}
