using GDD.Admin.Business.IBLL;
using GDD.Business;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.BLL
{
    /// <summary>
    /// 导入服务
    /// </summary>
    public class ImportServer : Repository, IImportService
    {
        public bool ImportEmployee(List<Employee> employees)
        {
            using (var db = base.GDDSVSPDb)
            {
                db.Employee.AddRange(employees);
                return db.SaveChanges() > 0;
            }
        }

        public bool ImportOption(List<Option> options)
        {
            using (var db = base.GDDSVSPDb)
            {
                db.Option.AddRange(options);
                return db.SaveChanges() > 0;
            }
        }

        public bool ImportQuestionType(List<QuestionType> questionTypes)
        {
            using (var db = base.GDDSVSPDb)
            {
                db.QuestionType.AddRange(questionTypes);
                return db.SaveChanges() > 0;
            }
        }
    }
}
