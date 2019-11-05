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
    public class OptionProfile : Profile
    {
        IQuestionnaireTypeService questionnaireTypeService = new QuestionnaireTypeServer();
        public OptionProfile()
        {
            CreateMap<Option, OptionVO>()
                .ForMember(dto => dto.OptionID, (map) => map.MapFrom(m => m.OptionID.ToString()))
                .ForMember(dto => dto.OptionNumber, (map) => map.MapFrom(m => m.OptionNumber.ToString()))
                .ForMember(dto => dto.OptionScore, (map) => map.MapFrom(m => m.OptionScore.ToString()))
                //.ForMember(dto => dto.CreateTime, (map) => map.MapFrom(m => Convert.ToDateTime(m.CreateTime).ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dto => dto.QuestionnaireTypeName, (map) => map.MapFrom(m => questionnaireTypeService.GetQuestionnaireTypeNameById(m.QuestionnaireTypeID)));
        }
    }
}
