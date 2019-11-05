
using GDD.Business;
using GDD.MiniProgram.Business.IBLL;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.MiniProgram.Business.BLL
{
    public class FunctionalGroupServer : Repository, IFunctionalGroupService
    {
        /// <summary>
        /// 通过功能组ID获取名称
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public string GetFunctionalgroupNameById(Guid? id)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<FunctionalGroup> baseQuery = db.FunctionalGroup;
                if (id != Guid.Empty && id != null)
                {
                    return db.FunctionalGroup.SingleOrDefault(p => p.FunctionalGroupID == id)?.FunctionalGroupName;
                }
            }
            return "";
        }

        /// <summary>
        /// 获取所有的功能组数据
        /// </summary>
        /// <returns></returns>
        public List<FunctionalGroup> GetFunctionalgroupList()
        {
            using (var db = base.GDDSVSPDb)
            {
                return db.FunctionalGroup.ToList();
            }
        }

        /// <summary>
        /// 通过部门ID获取功能组
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public List<FunctionalGroup> GetFunctionalGroupByDepartmentId(Guid departmentId)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<FunctionalGroup> baseQuery = db.FunctionalGroup;
                if (departmentId != Guid.Empty && departmentId != null)
                {
                    baseQuery = baseQuery.Where(p => p.DepartmentID == departmentId);
                }
                var list = baseQuery.ToList();
                return list;
            }
        }
    }
}
