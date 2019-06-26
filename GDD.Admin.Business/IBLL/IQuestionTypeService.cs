using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.IBLL
{
    /// <summary>
    /// 题目类型服务
    /// </summary>
    public interface IQuestionTypeService
    {
        /// <summary>
        /// 获取题目类型集合
        /// </summary>
        /// <param name="typeName">题目类型名称</param>
        /// <param name="QuestionnaireId">问卷ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        List<QuestionType> GetQuestionTypeList(string typeName, Guid QuestionnaireId, int pageIndex, int pageSize);

        /// <summary>
        /// 获取题目类型总数
        /// </summary>
        /// <param name="typeName">题目类型名称</param>
        /// <param name="QuestionnaireId">问卷ID</param>
        /// <returns></returns>
        int GetQuestionTypeCount(string typeName, Guid QuestionnaireId);

        /// <summary>
        /// 添加题目类型
        /// </summary>
        /// <param name="Question">题目类型对象</param>
        /// <returns></returns>
        bool InsertQuestionType(QuestionType questionType);

        /// <summary>
        /// 修改题目类型
        /// </summary>
        /// <param name="Question">题目类型对象</param>
        /// <returns></returns>
        bool UpdateQuestionType(QuestionType questionType);


        /// <summary>
        /// 删除题目类型
        /// </summary>
        /// <param name="id">题目类型ID</param>
        /// <returns></returns>
        bool DeleteQuestionType(Guid id);

        /// <summary>
        /// 通过ID获取题目类型名称
        /// </summary>
        /// <param name="id">题目类型ID</param>
        /// <returns></returns>
        string GetQuestionTypeNameById(Guid? id);


    }
}
