using AutoMapper;
using GDD.Admin.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.AutoMapper
{
    public class ScoreChartProfile : Profile
    {
        public ScoreChartProfile()
        {
            CreateMap<ScoreChart, ScoreChartVO>()
                .ForMember(dto => dto.CreateTime, (map) => map.MapFrom(m => m.CreateTime.Value.ToString("yyyy-MM-dd")))
                .ForMember(dto => dto.QuestionNumber, (map) => map.MapFrom(m => m.QuestionNumber.ToString()))
                .ForMember(dto => dto.OptionNumber, (map) => map.MapFrom(m => m.OptionNumber.ToString()))
                .ForMember(dto => dto.OptionScore, (map) => map.MapFrom(m => m.OptionScore.ToString()));
        }
    }
}
