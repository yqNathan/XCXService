using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.VO
{
    public class UserVO
    {
        public string UserID { get; set; }
        public string UserAccount { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public string RoleCode { get; set; }
        public string CreateTime { get; set; }
        public string Creator { get; set; }
        public string ModifiedTime { get; set; }
        public string Modifier { get; set; }
    }
}
