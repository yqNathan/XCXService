using GDD.Business;
using GDD.Common;
using GDD.MiniProgram.Business.IBLL;
using GDD.MiniProgram.VO;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GDD.MiniProgram.Business.BLL
{
    public class EmployeeServer : Repository, IEmployeeService
    {
        /// <summary>
        /// 通过员工编号获取员工信息
        /// </summary>
        /// <param name="employeeNumber">员工编号</param>
        /// <returns></returns>
        public Employee GetEmployeeByEmployeeNumber(string employeeNumber , string openid )
        {
            using (var db = base.GDDSVSPDb)
            {
                Employee employee = null;
                
                if (!String.IsNullOrEmpty(employeeNumber))
                {
                    employee = db.Employee.FirstOrDefault(p => p.EmployeeNumber == employeeNumber);
                }
                else if (!String.IsNullOrEmpty(openid))
                {
                    employee = db.Employee.FirstOrDefault(p => p.OpenID == openid);
                }
                return employee;
            }
        }

        /// <summary>
        /// 通过员工ID修改员工密码
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="newPwd">员工密码</param>
        /// <returns></returns>
        public Employee UpdatePassword(Guid? employeeId, string newPwd)
        {
            using (var db = base.GDDSVSPDb)
            {
                Employee employee = db.Employee.FirstOrDefault(p => p.EmployeeID == employeeId);
                employee.Password = MD5.GetMd5Hash(newPwd);
                db.SaveChanges();
                return employee;
            }
        }



        /// <summary>
        /// 通过员工ID修改员工Openid
        /// </summary>
        /// <param name="employeeID">员工ID</param>
        /// <param name="openid">openid</param>
        /// <returns></returns>
        public bool UpdateEmployeeOpenIdById(Guid employeeID, string openid)
        {

            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                {
                    using (var db = base.GDDSVSPDb)
                    {
                        db.Employee.Where(p => p.OpenID == openid && p.Status == 0).UpdateFromQuery(p => new Employee { OpenID = "" });
                        db.SaveChanges();

                        Employee employee = db.Employee.FirstOrDefault(p => p.EmployeeID == employeeID);
                        employee.OpenID = openid;
                        db.SaveChanges();
                    }
                    transaction.Complete();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// 获取提交人员名单
        /// </summary>
        /// <param name="questionnaireID">问卷ID</param>
        /// <param name="departmentID">部门ID</param>
        /// <param name="functionalGroupID">功能组ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public PageVO GetEmployeeList(Guid questionnaireID, Guid departmentID, Guid functionalGroupID, int isSubmit,int pageIndex, int pageSize)
        {
            using (var db = base.GDDSVSPDb)
            {
                int isDel = Convert.ToInt32(IsDel.未删除);
                int status = Convert.ToInt32(EmployeeStatus.在职);
                IQueryable<Employee> employee = db.Employee.Where(p=>p.Status == status);
                IQueryable<QuestionnaireSubmission> submission = db.QuestionnaireSubmission.Where(p=>p.IsDel == isDel);
                IQueryable<Department> department = db.Department;
                IQueryable<FunctionalGroup> functionalGroup = db.FunctionalGroup;
                DateTime nowDate = DateTime.Now;
                
                if (departmentID != Guid.Empty && departmentID != null)
                {
                    employee = db.Employee.Where(p => p.DepartmentID == departmentID);
                }
                if (functionalGroupID != Guid.Empty && functionalGroupID != null)
                {
                    employee = db.Employee.Where(p => p.FunctionalgroupID == functionalGroupID);
                }
                if (questionnaireID != Guid.Empty && questionnaireID != null)
                {
                    submission = submission.Where(p => p.QuestionnaireID == questionnaireID );
                }
               

                var voQuery = from e1 in employee
                              join s1 in submission on e1.EmployeeID equals s1.EmployeeID into data2
                              from obj2 in data2.DefaultIfEmpty()
                              join d1 in department on e1.DepartmentID equals d1.DepartmentID into data3
                              from obj3 in data3.DefaultIfEmpty()
                              join f1 in functionalGroup on e1.FunctionalgroupID equals f1.FunctionalGroupID into data4
                              from obj4 in data4.DefaultIfEmpty()
                              select new EmployeeVO
                              {
                                  EmployeeID = e1.EmployeeID.ToString(),
                                  EmployeeName = e1.EmployeeName,
                                  Sex = e1.Sex == 1 ? "男" : "女",
                                  EmployeeNumber = e1.EmployeeNumber,
                                  Phone = e1.Phone,
                                  DepartmentName = obj3.DepartmentName,
                                  FunctionalgroupName = obj4.FunctionalGroupName,
                                  HireTime = e1.HireTime,
                                  IsSubmit = obj2.IsSubmit != null ? "已提交" : "未提交",
                                  SubmitTime = obj2.CreateTime
                              };
                if (isSubmit == 1)
                {
                    voQuery = voQuery.Where(p => p.IsSubmit == "已提交");
                }
                else if (isSubmit == 0)
                {
                    voQuery = voQuery.Where(p => p.IsSubmit == "未提交");
                }
                var volist = voQuery.OrderBy(p => p.SubmitTime).OrderBy(p => p.EmployeeID).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                var pageCount = voQuery.Count() / pageSize;
                PageVO obj = new PageVO();
                obj.EmployeeVOs = volist;
                obj.PageCount = pageCount;
                return obj;
            }
        }


        /// <summary>
        /// 获取通讯录
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <param name="functionalGroupID">功能组ID</param>
        /// <returns></returns>
        public List<EmployeeVO> GetAddressBook( Guid departmentID, Guid functionalGroupID,string searchStr)
        {
            using (var db = base.GDDSVSPDb)
            {
                int isDel = Convert.ToInt32(IsDel.未删除);
                int status = Convert.ToInt32(EmployeeStatus.在职);
                IQueryable<Employee> employee = db.Employee.Where(p => p.Status == status);
                IQueryable<Department> department = db.Department;
                IQueryable<FunctionalGroup> functionalGroup = db.FunctionalGroup;
                DateTime nowDate = DateTime.Now;

                if (departmentID != Guid.Empty && departmentID != null)
                {
                    employee = db.Employee.Where(p => p.DepartmentID == departmentID);
                }
                if (functionalGroupID != Guid.Empty && functionalGroupID != null)
                {
                    employee = db.Employee.Where(p => p.FunctionalgroupID == functionalGroupID);
                }
                if (!string.IsNullOrEmpty(searchStr))
                {
                    employee = db.Employee.Where(p => p.EmployeeName.Contains(searchStr));
                }

                var voQuery = from e1 in employee
                              join d1 in department on e1.DepartmentID equals d1.DepartmentID into data3
                              from obj3 in data3.DefaultIfEmpty()
                              join f1 in functionalGroup on e1.FunctionalgroupID equals f1.FunctionalGroupID into data4
                              from obj4 in data4.DefaultIfEmpty()
                              select new EmployeeVO
                              {
                                  EmployeeID = e1.EmployeeID.ToString(),
                                  EmployeeName = e1.EmployeeName,
                                  Sex = e1.Sex == 1 ? "男" : "女",
                                  EmployeeNumber = e1.EmployeeNumber,
                                  Phone = e1.Phone,
                                  DepartmentName = obj3.DepartmentName,
                                  FunctionalgroupName = obj4.FunctionalGroupName,
                                  HireTime = e1.HireTime
                              };
                var volist = voQuery.OrderBy(p => p.EmployeeName).OrderBy(p => p.EmployeeID).ToList();
                return volist;
            }
        }
    }
}
