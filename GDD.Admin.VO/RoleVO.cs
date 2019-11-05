using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.VO
{
    public class RoleVO
    {
        public string RoleID { get; set; }
        public string RoleCode { get; set; }
        public string Description { get; set; }
        public string RoleName { get; set; }
        public string CreateTime { get; set; }
        public string Creator { get; set; }
        public string  ModifiedTime { get; set; }
        public string Modifier { get; set; }
    }
}
