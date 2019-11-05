using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.MiniProgram.Business.IBLL
{
    public interface IFunctionalGroupService
    {
        /// <summary>
        /// 通过功能组ID获取名称
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        string GetFunctionalgroupNameById(Guid? id);

        /// <summary>
        /// 通过部门ID获取功能组
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        List<FunctionalGroup> GetFunctionalGroupByDepartmentId(Guid DepartmentId);

        /// <summary>
        /// 获取所有功能组
        /// </summary>
        /// <returns></returns>
        List<FunctionalGroup> GetFunctionalgroupList();
    }
}
