using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.VO
{
    public class PowerTreeVO
    {
        public string title { get; set; }
        public Guid? ParentId { get; set; }
        public int id { get; set; }
        public string Code { get; set; }
        public List<PowerTreeVO> children { get; set; }
        public bool @checked { get; set; }
        public string Type { get; set; }
    }
}
