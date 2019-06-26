using GDD.Admin.Business.IBLL;
using GDD.Business;
using GDD.Core;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.BLL
{
    public class MenuServer : Repository, IMenuService
    {
        public List<Menu> GetMenuList()
        {
            using (var db = base.GDDSVSPDb)
            {
                //Guid newUserId = new Guid(userId);
                IQueryable<Menu> baseQuery = db.Menu;
                var menuList = baseQuery.ToList();
                return menuList;
            }
        }
    }
}
