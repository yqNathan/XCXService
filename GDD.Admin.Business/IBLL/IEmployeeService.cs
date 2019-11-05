using GDD.Admin.VO;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.IBLL
{
    public interface IEmployeeService
    {
        /// <summary>
        /// 通过员工编号获取员工信息
        /// </summary>
        /// <param name="employeeNumber">员工编号</param>
        /// <returns></returns>
        Employee GetEmployeeByEmployeeNumber(string employeeNumber);

        /// <summary>
        /// 获取人员集合
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        List<Employee> GetEmployeeList(string name, Guid? departmentId, Guid? functionalGroupId , int pageIndex, int pageSize);

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
        List<SubmittedEmployee> GetSubmittedEmployeeList(string name, Guid? departmentId, Guid? functionalGroupId, Guid? questionnaireId, int pageIndex, int pageSize, int isSubmit);

        /// <summary>
        /// 获取人员总数
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="departmentId">人员名称</param>
        /// <returns></returns>
        int GetEmployeeCount(string name, Guid? departmentId, Guid? functionalGroupId);

        /// <summary>
        /// 添加人员
        /// </summary>
        /// <param name="Employee">人员对象</param>
        /// <returns></returns>
        bool InsertEmployee(Employee employee);

        /// <summary>
        /// 修改人员
        /// </summary>
        /// <param name="Employee">人员对象</param>
        /// <returns></returns>
        bool UpdateEmployee(Employee employee);


        /// <summary>
        /// 删除人员
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        bool DeleteEmployee(Guid id);

        /// <summary>
        /// 获取提交人员总数
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="departmentId">部门ID</param>
        /// <param name="questionnaireId">问卷ID</param>
        /// <param name="isSubmit">是否已提交  0：否  1：是</param>
        /// <returns></returns>
        int GetSubmittedEmployeeCount(string name, Guid? departmentId, Guid? functionalGroupId, Guid? questionnaireId, int isSubmit);
    }
}
