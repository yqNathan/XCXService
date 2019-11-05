using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.VO
{
    public class ScoreChart
    {
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string FunctionalGroupName { get; set; }
        public string QuestionTypeName { get; set; }
        public string QuestionName { get; set; }
        public Guid? QuestionID { get; set; }
        public int? QuestionNumber { get; set; }
        
        public int? OptionNumber { get; set; }
        public Guid? OptionId { get; set; }
        public string OptionName { get; set; }
        public int? OptionScore { get; set; }
        public string OptionReason { get; set; }
        public DateTime? CreateTime { get; set; }
        public Guid? OptionTypeID { get; set; }
        public string OptionTypeName { get; set; }
    }

    public class ScoreChartVO
    {
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string FunctionalGroupName { get; set; }
        public string QuestionTypeName { get; set; }
        public string QuestionName { get; set; }
        public string QuestionNumber { get; set; }
        public string OptionNumber { get; set; }
        public string OptionName { get; set; }
        public string OptionTypeName { get; set; }
        public string OptionScore { get; set; }
        public string OptionReason { get; set; }
        public string CreateTime { get; set; }
    }

    public class QuestionChart
    {
        public string QuestionName { get; set; }
        public int? QuestionNumber { get; set; }
        public List<OptionChart> OptionChartList { get; set; }
}

    public class OptionChart
    {
        public string OptionName { get; set; }
        public int? OptionNumber { get; set; }
        public int OptionCount { get; set; }
    }

    public class ScoreCharResult
    {
        public List<ScoreChart> ScoreChartList { get; set; }
        
        public List<QuestionChart> QuestionChartList { get; set; }
        public List<string> OptionNumberList { get; set; }
    }
}
