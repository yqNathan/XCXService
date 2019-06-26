using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.IBLL
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public interface IRoleService
    {
        List<Role> GetRoles();
    }
}
