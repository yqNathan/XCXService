using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.MiniProgram.VO
{
    public class EmployeeVO
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Sex { get; set; }
        public string EmployeeNumber { get; set; }
        public string Phone { get; set; }
        public string DepartmentName { get; set; }
        public string FunctionalgroupName { get; set; }
        public DateTime? HireTime { get; set; }
        public DateTime? SubmitTime { get; set; }
        public string IsSubmit { get; set; }
    }

    public class PageVO
    {
        public List<EmployeeVO> EmployeeVOs { get; set; }
        public int PageCount { get; set; }
    }
}
