using AutoMapper;
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
    public class ButtonProfile : Profile
    {
        public ButtonProfile()
        {
            CreateMap<SYS_Button, ButtonVO>()
                .ForMember(dto => dto.ButtonID, (map) => map.MapFrom(m => m.ButtonID.ToString()))
                .ForMember(dto => dto.BtnType, (map) => map.MapFrom(m => ((BtnType)m.BtnType).ToString()))
                .ForMember(dto => dto.ShowType, (map) => map.MapFrom(m => ((BtnShowType)m.ShowType).ToString()))
                .ForMember(dto => dto.CreateTime, (map) => map.MapFrom(m => m.CreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dto => dto.ModifiedTime, (map) => map.MapFrom(m => m.ModifiedTime == null ?  "": m.ModifiedTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
        }
    }
}
