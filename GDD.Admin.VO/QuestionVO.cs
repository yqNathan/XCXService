using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.VO
{
    public class QuestionVO
    {
        public string QuestionID { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionNumber { get; set; }
        //public string QuestionnaireName { get; set; }
        public string QWName { get; set; }
        public string QuestionTypeName { get; set; }
        public string QuestionnaireTypeName { get; set; }
        public string OptionTypeName { get; set; }
        public string MaxOptionNumber { get; set; }
        public string MinOptionNumber { get; set; }
        public string IsAnswer { get; set; }
        public string QuestionState { get; set; }
        public List<Option> Options { get; set; }
    }
}
