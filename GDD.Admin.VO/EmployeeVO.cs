using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.VO
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
        //public Nullable<System.Guid> JobTypeID { get; set; }
        //public Nullable<System.Guid> RoleID { get; set; }
        //public Nullable<System.Guid> DegreeID { get; set; }
        //public string Position { get; set; }
        //public Nullable<int> Status { get; set; }
        //public Nullable<System.DateTime> CreateTime { get; set; }
        public string HireTime { get; set; }
        //public Nullable<System.DateTime> LoginTime { get; set; }
    }

    public class SubmittedEmployee
    {
        public System.Guid EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<int> Sex { get; set; }
        public string EmployeeNumber { get; set; }
        public string Phone { get; set; }
        public Nullable<System.Guid> DepartmentID { get; set; }
        public Nullable<System.Guid> FunctionalgroupID { get; set; }
        public Nullable<System.Guid> JobTypeID { get; set; }
        public Nullable<System.Guid> RoleID { get; set; }
        public Nullable<System.Guid> DegreeID { get; set; }
        public string Position { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> HireTime { get; set; }
        public Nullable<System.DateTime> LoginTime { get; set; }
        public string Password { get; set; }
        public int? IsSubmit { get; set; }
        public Nullable<System.DateTime> SubmitTime { get; set; }
    }

    public class SubmittedEmployeeVO
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Sex { get; set; }
        public string EmployeeNumber { get; set; }
        public string Phone { get; set; }
        public string DepartmentName { get; set; }
        public string FunctionalgroupName { get; set; }
        public string HireTime { get; set; }
        public string IsSubmit { get; set; }
        public string SubmitTime { get; set; }
    }
}
