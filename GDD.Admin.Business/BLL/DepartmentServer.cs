using GDD.Admin.Business.IBLL;
using GDD.Business;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.BLL
{
    public class DepartmentServer : Repository, IDepartmentService
    {
        /// <summary>
        /// 通过ID获取部门名称
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public string GetDepartmentNameById(Guid? id)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<Department> baseQuery = db.Department;
                if (id != Guid.Empty)
                {
                   return db.Department.SingleOrDefault(p => p.DepartmentID == id)?.DepartmentName;
                }
            }
            return "";
        }

        /// <summary>
        /// 获取所有的部门数据
        /// </summary>
        /// <returns></returns>
        public List<Department> GetDepartmentList()
        {
            using (var db = base.GDDSVSPDb)
            {
                return db.Department.ToList();
            }
        }
        
    }
}
