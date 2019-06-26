using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.IBLL
{
    public interface IQuestionnaireService
    {
        /// <summary>
        /// 获取问卷集合
        /// </summary>
        /// <param name="name">问卷名称</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        List<Questionnaire> GetQuestionnaireList(string name,Guid typeId, int pageIndex, int pageSize);

        /// <summary>
        /// 获取问卷总数
        /// </summary>
        /// <param name="name">问卷名称</param>
        /// <returns></returns>
        int GetQuestionnaireCount(string name,Guid typeId);

        /// <summary>
        /// 添加问卷
        /// </summary>
        /// <param name="questionnaire">问卷对象</param>
        /// <returns></returns>
        bool InsertQuestionnaire(Questionnaire questionnaire);

        /// <summary>
        /// 修改问卷
        /// </summary>
        /// <param name="questionnaire">问卷对象</param>
        /// <returns></returns>
        bool UpdateQuestionnaire(Questionnaire questionnaire);


        /// <summary>
        /// 删除问卷
        /// </summary>
        /// <param name="id">问卷ID</param>
        /// <returns></returns>
        bool DeleteQuestionnaire(Guid id);

        /// <summary>
        /// 通过ID获取问卷名称
        /// </summary>
        /// <param name="id">问卷ID</param>
        /// <returns></returns>
        string GetQuestionnaireNameById(Guid? id);

        /// <summary>
        /// 通过ID获取问卷已提交人数
        /// </summary>
        /// <param name="id">问卷ID</param>
        /// <returns></returns>
        int GetSubmittedCountByQuestionnaireId(string name, Guid? questionnaireId, Guid? departmentId, int isSubmit);
    }
}
