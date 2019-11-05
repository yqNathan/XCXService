using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.MiniProgram.VO
{
    public class QustionTypeVO
    {
        public Guid? QuestionTypeID { get; set; }
        public string QuestionTypeName { get; set; }
        public Guid? QuestionID { get; set; }
        public int? QuestionNumber { get; set; }
        public string QuestionTitle { get; set; }
        public string OptionTypeCode { get; set; }
        public int? MaxOptionNumber { get; set; }
        public int? MinOptionNumber { get; set; }
        public int? IsAnswer { get; set; }
        public int? QuestionState { get; set; }
        public Guid? OptionID { get; set; }
        public string OptionName { get; set; }
        public int? OptionNumber { get; set; }
        public int? OptionScore { get; set; }
        public string OptionTitle { get; set; }
    }

    public class QuestionVO
    {
        public Guid? QuestionTypeID { get; set; }
        public string QuestionTypeName { get; set; }
        public List<QuestionOptionVO> Questions { get; set; }

    }

    public class QuestionOptionVO
    {
        public Guid? QuestionID { get; set; }
        public string OptionTypeCode { get; set; }
        public string QuestionTitle { get; set; }
        public int? QuestionNumber { get; set; }
        public int? MaxOptionNumber { get; set; }
        public int? MinOptionNumber { get; set; }
        public int? IsAnswer { get; set; }
        public int? QuestionState { get; set; }
        public List<OptionVO> Options { get; set; }
    }

    public class OptionVO
    {
        public Guid? OptionID { get; set; }
        public string OptionName { get; set; }
        public int? OptionNumber { get; set; }
        public int? OptionScore { get; set; }
        public string OptionTitle { get; set; }
    }
}
