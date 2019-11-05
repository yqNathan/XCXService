using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.DTO
{
    public class QuestionDTO
    {
        public System.Guid QuestionID { get; set; }
        public string QuestionTitle { get; set; }
        public Nullable<int> QuestionNumber { get; set; }
        public Nullable<System.Guid> QuestionWarehouseID { get; set; }
        public Nullable<System.Guid> QuestionTypeID { get; set; }
        public Nullable<System.Guid> OptionTypeID { get; set; }
        public Nullable<int> MaxOptionNumber { get; set; }
        public Nullable<int> MinOptionNumber { get; set; }
        public Nullable<int> IsAnswer { get; set; }
        public Nullable<int> QuestionState { get; set; }
        public List<Option> Options { get; set; }
        public Nullable<System.Guid> QuestionnaireTypeID { get; set; }
    }
}
