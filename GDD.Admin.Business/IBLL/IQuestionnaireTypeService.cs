using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.IBLL
{
    public interface IQuestionnaireTypeService
    {
        /// <summary>
        /// 获取问卷类型集合
        /// </summary>
        /// <param name="name">问卷名称</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        List<QuestionnaireType> GetQuestionnaireTypeList(string name, int pageIndex, int pageSize);

        /// <summary>
        /// 获取问卷类型总数
        /// </summary>
        /// <param name="name">问卷类型名称</param>
        /// <returns></returns>
        int GetQuestionnaireTypeCount(string name);

        /// <summary>
        /// 添加问卷类型
        /// </summary>
        /// <param name="questionnaire">问卷类型对象</param>
        /// <returns></returns>
        bool InsertQuestionnaireType(QuestionnaireType questionnaireType);

        /// <summary>
        /// 修改问卷类型
        /// </summary>
        /// <param name="questionnaire">问卷类型对象</param>
        /// <returns></returns>
        bool UpdateQuestionnaireType(QuestionnaireType questionnaireType);


        /// <summary>
        /// 删除问卷类型
        /// </summary>
        /// <param name="id">问卷类型ID</param>
        /// <returns></returns>
        bool DeleteQuestionnaireType(Guid id);

        /// <summary>
        /// 通过ID获取问卷类型名字
        /// </summary>
        /// <param name="id">问卷类型Id</param>
        /// <returns></returns>
        string GetQuestionnaireTypeNameById(Guid? id);
    }
}
