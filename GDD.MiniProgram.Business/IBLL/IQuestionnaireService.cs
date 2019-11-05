using GDD.MiniProgram.DTO;
using GDD.MiniProgram.VO;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.MiniProgram.Business.IBLL
{
    public interface IQuestionnaireService
    {
        /// <summary>
        /// 获取所有的问卷类型集合
        /// </summary>
        /// <returns></returns>
        List<QuestionnaireVO> GetQuestionnaireTypeList(Guid? employeeID);

        /// <summary>
        /// 通过类型ID获取问卷信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Questionnaire GetQuestionnaireByTypeId(Guid id);

        /// <summary>
        /// 通过问卷类型ID获取题目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<QuestionVO> GetQuestionListById(Guid questionnaireTypeID, Guid questionnaireID);

        /// <summary>
        /// 人员提交问卷数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool SubmitQuestionnaire(QuestionnaireDTO dto);
    }
}
