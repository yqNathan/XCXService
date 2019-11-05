using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.VO
{
    public class OptionVO
    {
        public string OptionID { get; set; }
        public string OptionName { get; set; }
        public string OptionNumber { get; set; }
        public string OptionScore { get; set; }
        public string CreateTime { get; set; }
        public string Creator { get; set; }
        public string QuestionnaireTypeName { get; set; }
    }
}
