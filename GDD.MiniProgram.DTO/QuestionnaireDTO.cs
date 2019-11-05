using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.MiniProgram.DTO
{
    public class QuestionnaireDTO
    {
        public Guid QuestionnaireID { get; set; }
        public Guid EmployeeID { get; set; }
        public Guid DepartmentID { get; set; }
        public Guid FunctionalGroupID { get; set; }
        public int IsRelation { get; set; }
        public List<QuestionDTO> Questions{ get; set; }
    }
}
