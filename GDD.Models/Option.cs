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
    
    public partial class Option
    {
        public System.Guid OptionID { get; set; }
        public string OptionName { get; set; }
        public Nullable<int> OptionNumber { get; set; }
        public Nullable<int> OptionScore { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Creator { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
        public string Modifier { get; set; }
        public Nullable<System.Guid> QuestionnaireTypeID { get; set; }
    }
}
