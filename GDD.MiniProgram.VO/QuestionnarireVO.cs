using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.MiniProgram.VO
{
    public class QuestionnaireVO
    {
        public Guid? QuestionnaireTypeID { get; set; }
        public string QuestionnaireTypeName { get; set; }

        public Guid? QuestionnaireID { get; set; }
        public string QuestionnaireName { get; set; }

        public string State { get; set; }

        public string IsSubmit { get; set; }

        public DateTime? CreateTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? IsRelation { get; set; }
        public string Describe { get; set; }

        public int? LowestScore { get; set; }
    }
}
