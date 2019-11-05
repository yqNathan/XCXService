using GDD.Admin.VO;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.IBLL
{
    /// <summary>
    /// 按钮服务
    /// </summary>
    public interface IButtonService
    {
        /// <summary>
        /// 获取按钮集合
        /// </summary>
        /// <param name="ButtonName">按钮名称</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        List<SYS_Button> GetButtonList(string ButtonName, int pageIndex, int pageSize);

        /// <summary>
        /// 通过菜单代码获取按钮集合
        /// </summary>
        /// <param name="menuCode">菜单代码</param>
        /// <returns></returns>
        List<CheckBoxButtonVO> GetMenuButtonList(string menuCode);

            /// <summary>
            /// 获取按钮总数
            /// </summary>
            /// <param name="ButtonName">按钮名称</param>
            /// <returns></returns>
            int GetButtonCount(string ButtonName);

        /// <summary>
        /// 根据按钮ID获取按钮名称
        /// </summary>
        /// <param name="ButtonId">按钮ID</param>
        /// <returns></returns>
        string GetButtonNameById(Guid? ButtonId);

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="Button">按钮对象</param>
        /// <returns></returns>
        bool InsertButton(SYS_Button Button);

        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="obj">按钮对象</param>
        /// <returns></returns>
        bool UpdateButton(SYS_Button obj);

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="id">按钮ID</param>
        /// <returns></returns>
        bool DeleteButton(Guid id);

        /// <summary>
        /// 通过角色和菜单代码获取按钮集合
        /// </summary>
        /// <param name="menuCode"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        List<SYS_Button> GetButtonListByRoleMenu(string menuCode, string roleCode);
    }
}
