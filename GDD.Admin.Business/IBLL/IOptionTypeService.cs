using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.IBLL
{
    /// <summary>
    /// 选项类型
    /// </summary>
    public interface IOptionTypeService
    {
        /// <summary>
        /// 通过ID获取选项类型名称
        /// </summary>
        /// <param name="id">选项类型ID</param>
        /// <returns></returns>
        string GetOptionTypeNameById(Guid? id);
    }
}
