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
    public class QuestionTypeProfile : Profile
    {
        IQuestionnaireTypeService questionnaireTypeService = new QuestionnaireTypeServer();
        public QuestionTypeProfile()
        {
            CreateMap<QuestionType, QuestionTypeVO>()
                .ForMember(dto => dto.QuestionnaireTypeName, (map) => map.MapFrom(m => questionnaireTypeService.GetQuestionnaireTypeNameById(m.QuestionnaireTypeID)));
        }
    }
}
