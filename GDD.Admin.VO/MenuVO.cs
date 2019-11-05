using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.VO
{
    public partial class MenuVO
    {
        public System.Guid MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuCode { get; set; }
        public string MenuUrl { get; set; }
        public Nullable<System.Guid> MenuParentID { get; set; }
        public string MenuParentName { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public string IconCode { get; set; }
        public string Type { get; set; }
    }
}
