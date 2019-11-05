using AutoMapper;
using GDD.Admin.VO;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.AutoMapper
{
    public class RoleProfile :Profile
    {
        public RoleProfile()
        {
            CreateMap<SYS_Role, RoleVO>()
                .ForMember(dto => dto.RoleID, (map) => map.MapFrom(m => m.RoleID.ToString()))
                .ForMember(dto => dto.CreateTime, (map) => map.MapFrom(m => m.CreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dto => dto.ModifiedTime, (map) => map.MapFrom(m => m.ModifiedTime == null ? "" : m.ModifiedTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
        }
    }
}
