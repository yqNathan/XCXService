using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.MiniProgram.Business.IBLL
{
    /// <summary>
    /// 部门服务
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// 通过ID获取部门名称
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        string GetDepartmentNameById(Guid? id);

        /// <summary>
        /// 获取所有的部门数据
        /// </summary>
        /// <returns></returns>
        List<Department> GetDepartmentList();
    }
}
