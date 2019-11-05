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
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<SYS_User, UserVO>()
                .ForMember(dto => dto.UserID, (map) => map.MapFrom(m => m.UserID.ToString()))
                .ForMember(dto => dto.CreateTime, (map) => map.MapFrom(m => m.CreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dto => dto.ModifiedTime, (map) => map.MapFrom(m => m.ModifiedTime == null ? "" : m.ModifiedTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
        }
    }
}
