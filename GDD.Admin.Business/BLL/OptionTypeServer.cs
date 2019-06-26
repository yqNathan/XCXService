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
    /// <summary>
    /// 选项类型服务
    /// </summary>
    public class OptionTypeServer : Repository, IOptionTypeService
    {
        /// <summary>
        /// 通过ID获取选项类型名称
        /// </summary>
        /// <param name="id">选项类型ID</param>
        /// <returns></returns>
        public string GetOptionTypeNameById(Guid? id)
        {
            using (var db = base.GDDSVSPDb)
            {
                OptionType optionType = db.OptionType.SingleOrDefault(p => p.OptionTypeID == id);
                return optionType?.OptionTypeName;
            }
        }
    }
}
