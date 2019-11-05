using GDD.Admin.VO;
using GDD.Business;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.IBLL
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// 获取菜单集合
        /// </summary>
        /// <param name="menuName">菜单名称</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        List<SYS_Menu> GetMenuList(string menuName, int pageIndex, int pageSize);

        /// <summary>
        /// 获取菜单按钮集合
        /// </summary>
        /// <param name="menuName">菜单名称</param>
        /// <returns></returns>
        List<MenuVO> GetMenuButtonList(string menuName);

        /// <summary>
        /// 获取菜单总数
        /// </summary>
        /// <param name="menuName">菜单名称</param>
        /// <returns></returns>
        int GetMenuCount(string menuName);

        /// <summary>
        /// 根据菜单ID获取菜单名称
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns></returns>
        string GetMenuNameById(Guid? menuId);

        /// <summary>
        /// 获取父级菜单集合
        /// </summary>
        /// <returns></returns>
        List<SYS_Menu> GetMenuParents();

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu">菜单对象</param>
        /// <returns></returns>
        bool InsertMenu(SYS_Menu menu);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="obj">菜单对象</param>
        /// <returns></returns>
        bool UpdateMenu(MenuVO obj);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id">菜单ID</param>
        /// <returns></returns>
        bool DeleteMenu(Guid id);

        /// <summary>
        /// 修改菜单下的按钮
        /// </summary>
        /// <param name="menuCode">菜单对象</param>
        /// <param name="arr">按钮数组</param>
        /// <returns></returns>
        bool UpdateMenuButton(string menuCode, List<string> arr);

        /// <summary>
        /// 获取权限菜单集合
        /// </summary>
        /// <param name="roleCode">角色代码</param>
        /// <returns></returns>
        List<SYS_Menu> GetRoleMenuList(string roleCode);
    }
}
