using GDD.MiniProgram.VO;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.MiniProgram.Business.IBLL
{
    /// <summary>
    /// 人员服务
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// 通过员工编号获取员工信息
        /// </summary>
        /// <param name="employeeNumber">员工编号</param>
        /// <returns></returns>
        Employee GetEmployeeByEmployeeNumber(string employeeNumber, string openid);

        /// <summary>
        /// 通过员工ID修改员工密码
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="newPwd">员工密码</param>
        /// <returns></returns>
        Employee UpdatePassword(Guid? employeeId, string newPwd);

        /// <summary>
        /// 通过员工ID修改员工Openid
        /// </summary>
        /// <param name="employeeID">员工ID</param>
        /// <param name="openid">openid</param>
        /// <returns></returns>
        bool UpdateEmployeeOpenIdById(Guid employeeID, string openid);

        /// <summary>
        /// 获取提交人员名单
        /// </summary>
        /// <param name="questionnaireID">问卷ID</param>
        /// <param name="departmentID">部门ID</param>
        /// <param name="functionalGroupID">功能组ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        PageVO GetEmployeeList(Guid questionnaireID, Guid departmentID, Guid functionalGroupID, int isSubmit, int pageIndex, int pageSize);

        /// <summary>
        /// 获取通讯录
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <param name="functionalGroupID">功能组ID</param>
        /// <returns></returns>
        List<EmployeeVO> GetAddressBook(Guid departmentID, Guid functionalGroupID, string searchStr);
    }
}
