using GDD.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDD.Models;
using GDD.Admin.Business.IBLL;

namespace GDD.Admin.Business.BLL
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class RoleServer : Repository, IRoleService
    {
        public List<Role> GetRoles()
        {
            using (var db = base.GDDSVSPDb)
            {
                return db.Role.ToList();
            }
        }
    }
}
