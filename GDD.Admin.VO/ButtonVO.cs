using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.VO
{
    public class ButtonVO
    {
        public System.Guid ButtonID { get; set; }
        public string BtnCode { get; set; }
        public string BtnName { get; set; }
        public Nullable<int> BtnOrder { get; set; }
        public string BtnDescribe { get; set; }
        public string BtnIcon { get; set; }
        public string BtnType { get; set; }
        public string CreateTime { get; set; }
        public string Creator { get; set; }
        public string ModifiedTime { get; set; }
        public string Modifier { get; set; }
        public string  ShowType { get; set; }
    }
}
