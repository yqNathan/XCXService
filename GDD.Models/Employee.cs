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
    
    public partial class Employee
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
        public string Position { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> HireTime { get; set; }
        public Nullable<System.DateTime> LoginTime { get; set; }
        public string Password { get; set; }
        public string OpenID { get; set; }
    }
}
