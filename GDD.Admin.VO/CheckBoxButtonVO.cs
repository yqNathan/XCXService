using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.VO
{
    public class CheckBoxButtonVO
    {
        public Guid? ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? State { get; set; }
        public int? BtnOrder { get; set; }
    }
}
