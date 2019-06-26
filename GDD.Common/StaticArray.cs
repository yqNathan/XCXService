using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Common
{
    public enum QuestionState
    {
        禁用 = 0,
        启用 = 1
    }

    public enum QuestionIsAnswer
    {
        /// <summary>
        /// 非必答
        /// </summary>
        否 = 0,
        /// <summary>
        /// 必答
        /// </summary>
        是 = 1
    }


    public enum QuestionnaireState
    {
        未发布 = 0,
        调查中 = 1,
        调查完成 = 2
    }

    public enum QuestionnaireIsRelation
    {
        不关联 = 0,
        关联 = 1
    }

    public enum EmployeeStatus
    {
        在职 = 0,
        离职 = 1
    }

    public enum Sex
    {
        女 = 0,
        男 = 1
    }

    /// <summary>
    /// 导入的结果状态
    /// </summary>
    public enum ImportResultState
    {
        /// <summary>
        /// 导入成功
        /// </summary>
        Auccess = 0,
        /// <summary>
        /// 没有文件
        /// </summary>
        NoFile = 1,
        /// <summary>
        /// 非Excel文件
        /// </summary>
        UnExcel = 2,
        /// <summary>
        /// 异常
        /// </summary>
        Exception = 3,
        /// <summary>
        /// 没有内容
        /// </summary>
        NullValue = 4
    }

}
