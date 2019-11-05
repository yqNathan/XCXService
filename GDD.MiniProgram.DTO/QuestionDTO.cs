using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.MiniProgram.DTO
{
    public class QuestionDTO
    {
        public Guid? QuestionID { get; set; }
        public Guid? OptionID { get; set; }
        public string OptionReason { get; set; }
    }
}
