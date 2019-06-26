using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Business
{
    /// <summary>
    /// 数据库
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// GDDSVSP数据库
        /// </summary>
        protected GDDSVSPEntities GDDSVSPDb
        {
            get
            {
                var db = new GDDSVSPEntities();
                return db;
            }
        }
    }
}
