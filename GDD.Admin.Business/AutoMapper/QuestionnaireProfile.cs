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
    public class QuestionnaireProfile : Profile
    {
        IQuestionnaireTypeService questionnaireTypeService = new QuestionnaireTypeServer();
        public QuestionnaireProfile()
        {
            CreateMap<Questionnaire, QuestionnaireVO>()
                .ForMember(dto => dto.QuestionnaireID, (map) => map.MapFrom(m => m.QuestionnaireID.ToString()))
                .ForMember(dto => dto.QuestionnaireTypeName, (map) => map.MapFrom(m => questionnaireTypeService.GetQuestionnaireTypeNameById(m.QuestionnaireTypeID)))
                .ForMember(dto => dto.CreateTime, (map) => map.MapFrom(m => m.CreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dto => dto.IsRelation, (map) => map.MapFrom(m => ((QuestionnaireIsRelation)m.IsRelation).ToString() ))
                .ForMember(dto => dto.StartTime, (map) => map.MapFrom(m => m.StartTime.Value.ToString("yyyy-MM-dd")))
                .ForMember(dto => dto.EndTime, (map) => map.MapFrom(m => m.EndTime.Value.ToString("yyyy-MM-dd")))
                .ForMember(dto => dto.State, (map) => map.MapFrom(m => ((QuestionnaireState)m.State).ToString()));
        }
    }
}
