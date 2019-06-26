using GDD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.IBLL
{
    /// <summary>
    /// 导入服务
    /// </summary>
    public interface IImportService
    {
        bool ImportEmployee(List<Employee> employees);

        bool ImportOption(List<Option> options);

        bool ImportQuestionType(List<QuestionType> questionTypes);
    }
}
