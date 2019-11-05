using GDD.Admin.Business.IBLL;
using GDD.Admin.VO;
using GDD.Business;
using GDD.Common;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.BLL
{
    /// <summary>
    /// 人员服务
    /// </summary>
    public class EmployeeServer : Repository, IEmployeeService
    {

        /// <summary>
        /// 通过员工编号获取员工信息
        /// </summary>
        /// <param name="employeeNumber">员工编号</param>
        /// <returns></returns>
        public Employee GetEmployeeByEmployeeNumber(string employeeNumber)
        {
            using (var db = base.GDDSVSPDb)
            {
                Employee employee = new Employee();
                if (!String.IsNullOrEmpty(employeeNumber))
                {
                    employee = db.Employee.SingleOrDefault(p => p.EmployeeNumber.Equals(employeeNumber));
                }
                return employee;
            }
        }

        /// <summary>
        /// 获取人员集合
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public List<Employee> GetEmployeeList(string name, Guid? departmentId, Guid? functionalGroupId, int pageIndex, int pageSize)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<Employee> baseQuery = db.Employee;
                if (departmentId != Guid.Empty && departmentId != null)
                {
                    baseQuery = baseQuery.Where(p => p.DepartmentID == departmentId);
                }
                if (functionalGroupId != Guid.Empty && functionalGroupId != null)
                {
                    baseQuery = baseQuery.Where(p => p.FunctionalgroupID == functionalGroupId);
                }
                if (!String.IsNullOrEmpty(name))
                {
                    baseQuery = baseQuery.Where(p => p.EmployeeName.Contains(name));
                }
                var list = baseQuery.OrderByDescending(p => p.CreateTime).ThenBy(p=>p.EmployeeID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取问卷提交人员集合
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="departmentId">部门ID</param>
        /// <param name="questionnaireId">问卷ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="isSubmit">是否已提交  0：否  1：是</param>
        /// <returns></returns>
        public List<SubmittedEmployee> GetSubmittedEmployeeList(string name,Guid? departmentId, Guid? functionalGroupId, Guid? questionnaireId, int pageIndex, int pageSize, int isSubmit)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<Employee> employees = db.Employee;
                IQueryable<QuestionnaireSubmission> questionnaireSubmission = db.QuestionnaireSubmission;
                if (!String.IsNullOrEmpty(name))
                {
                    employees = employees.Where(p => p.EmployeeName.Contains(name));
                }
                if (departmentId != Guid.Empty && departmentId != null)
                {
                    employees = employees.Where(p => p.DepartmentID == departmentId);
                }
                if (functionalGroupId != Guid.Empty && functionalGroupId != null)
                {
                    employees = employees.Where(p => p.FunctionalgroupID == functionalGroupId);
                }
                if (questionnaireId != Guid.Empty && questionnaireId != null)
                {
                    questionnaireSubmission = questionnaireSubmission.Where(p => p.QuestionnaireID == questionnaireId);
                }
                var query = from e in employees
                            join q in questionnaireSubmission on e.EmployeeID equals q.EmployeeID into gc
                            from gci in gc.DefaultIfEmpty()
                            select new SubmittedEmployee
                            {
                                EmployeeID = e.EmployeeID,
                                EmployeeName = e.EmployeeName,
                                EmployeeNumber = e.EmployeeNumber,
                                Phone = e.Phone,
                                DepartmentID = e.DepartmentID,
                                FunctionalgroupID = e.FunctionalgroupID,
                                JobTypeID = e.JobTypeID,
                                RoleID = e.RoleID,
                                Position = e.Position,
                                Status = e.Status,
                                CreateTime = e.CreateTime,
                                HireTime = e.HireTime,
                                LoginTime = e.LoginTime,
                                Password = e.Password,
                                IsSubmit = gci.IsSubmit == null ? 0 : gci.IsSubmit,
                                SubmitTime = gci.CreateTime
                            };
                if (isSubmit != -1)
                {
                    query = query.Where(p => p.IsSubmit == isSubmit);
                }
                var list = query.OrderByDescending(p => p.SubmitTime).ThenBy(p => p.EmployeeID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取提交人员总数
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="departmentId">部门ID</param>
        /// <param name="questionnaireId">问卷ID</param>
        /// <param name="isSubmit">是否已提交  0：否  1：是</param>
        /// <returns></returns>
        public int GetSubmittedEmployeeCount(string name, Guid? departmentId, Guid? functionalGroupId, Guid? questionnaireId, int isSubmit)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<Employee> employees = db.Employee;
                IQueryable<QuestionnaireSubmission> questionnaireSubmission = db.QuestionnaireSubmission;
                if (!String.IsNullOrEmpty(name))
                {
                    employees = employees.Where(p => p.EmployeeName.Contains(name));
                }
                if (departmentId != Guid.Empty && departmentId != null)
                {
                    employees = employees.Where(p => p.DepartmentID == departmentId);
                }
                if (functionalGroupId != Guid.Empty && functionalGroupId != null)
                {
                    employees = employees.Where(p => p.FunctionalgroupID == functionalGroupId);
                }
                if (questionnaireId != Guid.Empty && questionnaireId != null)
                {
                    questionnaireSubmission = questionnaireSubmission.Where(p => p.QuestionnaireID == questionnaireId);
                }

                var query = from e in employees
                            join q in questionnaireSubmission on e.EmployeeID equals q.EmployeeID into gc
                            from gci in gc.DefaultIfEmpty()
                            select new SubmittedEmployee
                            {
                                EmployeeID = e.EmployeeID,
                                EmployeeName = e.EmployeeName,
                                EmployeeNumber = e.EmployeeName,
                                Phone = e.EmployeeName,
                                DepartmentID = e.DepartmentID,
                                FunctionalgroupID = e.FunctionalgroupID,
                                JobTypeID = e.JobTypeID,
                                RoleID = e.RoleID,
                                Position = e.Position,
                                Status = e.Status,
                                CreateTime = e.CreateTime,
                                HireTime = e.HireTime,
                                LoginTime = e.LoginTime,
                                Password = e.Password,
                                IsSubmit = gci.IsSubmit == null ? 0 : gci.IsSubmit
                            };
                if (isSubmit != -1)
                {
                    query = query.Where(p => p.IsSubmit == isSubmit);
                }
                var count = query.Count();
                return count;
            }
        }
        

        /// <summary>
        /// 获取人员总数
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public int GetEmployeeCount(string name,Guid? departmentId , Guid? functionalGroupId)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<Employee> baseQuery = db.Employee;
                if (!String.IsNullOrEmpty(name))
                {
                    baseQuery = baseQuery.Where(p => p.EmployeeName.Contains(name));
                }
                if (departmentId != Guid.Empty && departmentId != null)
                {
                    baseQuery = baseQuery.Where(p => p.DepartmentID == departmentId);
                }
                if (functionalGroupId != Guid.Empty && functionalGroupId != null)
                {
                    baseQuery = baseQuery.Where(p => p.FunctionalgroupID == functionalGroupId);
                }
                var count = baseQuery.Count();
                return count;
            }
        }

        /// <summary>
        /// 添加人员
        /// </summary>
        /// <param name="Employee">人员对象</param>
        /// <returns></returns>
        public bool InsertEmployee(Employee employee)
        {
            using (var db = base.GDDSVSPDb)
            {
                employee.Password = MD5.GetMd5Hash("123456");//默认密码
                employee.EmployeeID = Guid.NewGuid();
                employee.CreateTime = DateTime.Now;
                employee.Status = Convert.ToInt32(EmployeeStatus.在职);
                db.Employee.Add(employee);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 修改人员
        /// </summary>
        /// <param name="Employee">人员对象</param>
        /// <returns></returns>
        public bool UpdateEmployee(Employee obj)
        {
            try
            {
                using (var db = base.GDDSVSPDb)
                {
                    Employee employee = db.Employee.SingleOrDefault(p => p.EmployeeID == obj.EmployeeID);
                    employee.EmployeeName = obj.EmployeeName;
                    employee.Sex = obj.Sex;
                    employee.EmployeeNumber = obj.EmployeeNumber;
                    employee.Phone = obj.Phone;
                    employee.DepartmentID = obj.DepartmentID;
                    employee.FunctionalgroupID = obj.FunctionalgroupID;
                    employee.HireTime = obj.HireTime;
                    return db.SaveChanges() > 0;
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 删除人员
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        public bool DeleteEmployee(Guid id)
        {
            using (var db = base.GDDSVSPDb)
            {
                Employee employee = db.Employee.SingleOrDefault(p => p.EmployeeID == id);
                db.Employee.Remove(employee);
                return db.SaveChanges() > 0;
            }
        }

    }
}
