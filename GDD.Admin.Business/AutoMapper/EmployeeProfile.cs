using AutoMapper;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.VO;
using GDD.Common;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.AutoMapper
{
    public class EmployeeProfile : Profile
    {
        IDepartmentService departmentService = new DepartmentServer();
        IFunctionalgroupService functionalgroupService = new FunctionalgroupServer();
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeVO>()
                .ForMember(dto => dto.HireTime, (map) => map.MapFrom(m => m.HireTime.Value.ToString("yyyy-MM-dd")))
                .ForMember(dto => dto.Sex, (map) => map.MapFrom(m => m.Sex == 0 ? "女" : "男"))
                .ForMember(dto => dto.DepartmentName, (map) => map.MapFrom(m => departmentService.GetDepartmentNameById(m.DepartmentID)))
                .ForMember(dto => dto.FunctionalgroupName, (map) => map.MapFrom(m => functionalgroupService.GetFunctionalgroupNameById(m.FunctionalgroupID)));
        }
    }
}
