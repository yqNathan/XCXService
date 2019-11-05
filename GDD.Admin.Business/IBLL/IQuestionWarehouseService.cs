using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.IBLL
{
    public interface IQuestionWarehouseService
    {
        /// <summary>
        /// 根据问卷ID获取题库ID
        /// </summary>
        /// <param name="questionnaireId">问卷ID</param>
        /// <returns></returns>
        string GetQuestionWarehouseIDByQuestionnaireID(Guid questionnaireId);

        /// <summary>
        /// 获取题库集合
        /// </summary>
        /// <param name="qwName">题库名称</param>
        /// <param name="questionnaireTypeId">问卷类型ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        List<QuestionWarehouse> GetQuestionWarehouseList(string qwName, Guid questionnaireTypeId, int pageIndex, int pageSize);

        /// <summary>
        /// 根据题库ID获取题库名称
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        string GetQuestionWarehouseNameById(Guid? id);

        /// <summary>
        /// 获取题库总数
        /// </summary>
        /// <param name="qwName">题库名称</param>
        /// <param name="QuestionnaireId">问卷ID</param>
        /// <returns></returns>
        int GetQuestionWarehouseCount(string qwName, Guid QuestionnaireId);

        /// <summary>
        /// 添加题库
        /// </summary>
        /// <param name="questionWarehouse">题库对象</param>
        /// <returns></returns>
        bool InsertQuestionWarehouse(QuestionWarehouse questionType);

        /// <summary>
        /// 修改题库
        /// </summary>
        /// <param name="Question">题库对象</param>
        /// <returns></returns>
        bool UpdateQuestionWarehouse(QuestionWarehouse questionType);


        /// <summary>
        /// 删除题库
        /// </summary>
        /// <param name="id">题库ID</param>
        /// <returns></returns>
        bool DeleteQuestionWarehouse(Guid id);
    }
}
