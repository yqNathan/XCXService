using AutoMapper;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.DTO;
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
    public class QuestionProfile : Profile
    {
        IQuestionnaireService questionnaireService = new QuestionnaireServer();
        IQuestionnaireTypeService questionnaireTypeService = new QuestionnaireTypeServer();
        IQuestionTypeService questionTypeService = new QuestionTypeServer();
        IOptionTypeService optionTypeService = new OptionTypeServer();
        IOptionService optionService = new OptionServer();
        public QuestionProfile()
        {
            CreateMap<Question, QuestionVO>()
                .ForMember(dto => dto.QuestionID, (map) => map.MapFrom(m => m.QuestionID.ToString()))
                .ForMember(dto => dto.QuestionnaireName, (map) => map.MapFrom(m => questionnaireService.GetQuestionnaireNameById(m.QuestionnaireID)))
                .ForMember(dto => dto.QuestionTypeName, (map) => map.MapFrom(m => questionTypeService.GetQuestionTypeNameById(m.QuestionTypeID)))
                .ForMember(dto => dto.OptionTypeName, (map) => map.MapFrom(m => optionTypeService.GetOptionTypeNameById(m.OptionTypeID)))
                .ForMember(dto => dto.QuestionnaireTypeName, (map) => map.MapFrom(m => questionnaireTypeService.GetQuestionnaireTypeNameById(m.QuestionnaireTypeID)))
                .ForMember(dto => dto.IsAnswer, (map) => map.MapFrom(m => ((QuestionIsAnswer)m.IsAnswer).ToString()))
                .ForMember(dto => dto.QuestionState, (map) => map.MapFrom(m => ((QuestionState)m.QuestionState).ToString()))
                .ForMember(dto => dto.Options, (map) => map.MapFrom(m => optionService.GetOptionByQuestionId(m.QuestionID)));

            CreateMap<QuestionDTO, Question>();
        }
    }
}
