using AutoMapper;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.VO;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.AutoMapper
{
    public class MenuProfile : Profile
    {
        IMenuService menuService = new MenuServer();
        public MenuProfile()
        {
            CreateMap<SYS_Menu, MenuVO>()
                .ForMember(dto => dto.MenuParentName, (map) => map.MapFrom(m => menuService.GetMenuNameById(m.MenuParentID)))
                .ForMember(dto => dto.MenuParentID, (map) => map.MapFrom(m => m.MenuParentID == null ? Guid.Empty : m.MenuParentID ))
                .ForMember(dto => dto.Type, (map) => map.MapFrom(m => "菜单"));
        }
    }
}
