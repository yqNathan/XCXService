using AutoMapper;
using GDD.Admin.Business.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<QuestionnaireProfile>();
                cfg.AddProfile<EmployeeProfile>();
                cfg.AddProfile<QuestionProfile>();
                cfg.AddProfile<QuestionTypeProfile>();
                cfg.AddProfile<SubmittedEmployeeProfile>();
                cfg.AddProfile<ScoreChartProfile>();
                cfg.AddProfile<QuestionWarehouseProfile>();
                cfg.AddProfile<MenuProfile>();
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<ButtonProfile>();
                cfg.AddProfile<RoleProfile>();
                cfg.AddProfile<OptionProfile>();
            });
        }
    }
}
