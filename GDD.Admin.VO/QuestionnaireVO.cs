using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.VO
{
    public class QuestionnaireVO
    {
        public string QuestionnaireID { get; set; }
        public string QuestionnaireName { get; set; }
        public string Version { get; set; }
        public string Describe { get; set; }
        public Nullable<int> LowestScore { get; set; }
        public string CreateTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string IsRelation { get; set; }
        public string State { get; set; }
        public string QuestionnaireTypeName { get; set; }
        
    }
}
