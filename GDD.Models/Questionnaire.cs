//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GDD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Questionnaire
    {
        public System.Guid QuestionnaireID { get; set; }
        public string QuestionnaireName { get; set; }
        public string Version { get; set; }
        public string Describe { get; set; }
        public Nullable<int> LowestScore { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<int> IsRelation { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<System.Guid> QuestionnaireTypeID { get; set; }
        public Nullable<System.Guid> QuestionWarehouseID { get; set; }
    }
}
