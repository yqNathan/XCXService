using GDD.Admin.DTO;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.IBLL
{
    public interface IQuestionService
    {
        /// <summary>
        /// 获取题目集合
        /// </summary>
        /// <param name="questionWarehouseId">题库ID</param>
        /// <param name="title">题目名称</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        List<Question> GetQuestionList(Guid? typeId, Guid? questionWarehouseId, Guid? optionTypeId, Guid? questionTypeId, string title, int pageIndex, int pageSize);

        /// <summary>
        /// 获取题目总数
        /// </summary>
        /// <param name="questionWarehouseId">题库ID</param>
        /// <param name="name">题目名称</param>
        /// <returns></returns>
        int GetQuestionCount(Guid? typeId, Guid? questionWarehouseId, Guid? optionTypeId, Guid? questionTypeId, string title);

        /// <summary>
        /// 添加题目
        /// </summary>
        /// <param name="Question">题目对象</param>
        /// <returns></returns>
        bool InsertQuestion(QuestionDTO questionDTO);

        /// <summary>
        /// 修改题目
        /// </summary>
        /// <param name="Question">题目对象</param>
        /// <returns></returns>
        bool UpdateQuestion(QuestionDTO questionDTO);


        /// <summary>
        /// 删除题目
        /// </summary>
        /// <param name="id">题目ID</param>
        /// <returns></returns>
        bool DeleteQuestion(Guid id);
    }
}
