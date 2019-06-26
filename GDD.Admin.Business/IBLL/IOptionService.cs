using GDD.Admin.VO;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.IBLL
{
    public interface IOptionService
    {
        /// <summary>
        /// 获取选项集合
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        List<Option> GetOptionList(string name, int pageIndex, int pageSize);


        /// <summary>
        /// 获取选项总数
        /// </summary>
        /// <param name="name">选项名称</param>
        /// <returns></returns>
        int GetOptionCount(string name);

        /// <summary>
        /// 获取选项类型集合
        /// </summary>
        /// <returns></returns>
        List<OptionType> GetOptionTypeList();


        /// <summary>
        /// 添加选项
        /// </summary>
        /// <param name="option">选项对象</param>
        /// <returns></returns>
        bool InsertOption(Option option);

        /// <summary>
        /// 修改选项
        /// </summary>
        /// <param name="option">选项对象</param>
        /// <returns></returns>
        bool UpdateOption(Option option);


        /// <summary>
        /// 删除选项
        /// </summary>
        /// <param name="id">选项ID</param>
        /// <returns></returns>
        bool DeleteOption(Guid id);

        /// <summary>
        /// 通过问题ID获取相应的选项集合
        /// </summary>
        /// <param name="id">选项ID</param>
        /// <returns></returns>
        List<Option> GetOptionByQuestionId(Guid? questionId);

        /// <summary>
        /// 获取分数图表集合
        /// </summary>
        /// <param name="name">题目名称</param>
        /// <param name="departmentId">部门ID</param>
        /// <param name="questionnaireId">问卷ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        ScoreCharResult GetScoreChartList(string name, Guid? departmentId, Guid? functionalGroupID, Guid? questionnaireId, int pageIndex, int pageSize);

        /// <summary>
        /// 获取分数图表数量
        /// </summary>
        /// <param name="name">题目名称</param>
        /// <param name="departmentId">部门ID</param>
        /// <param name="questionnaireId">问卷ID</param>
        /// <returns></returns>
        int GetScoreChartCount(string name, Guid? departmentId, Guid? functionalGroupID, Guid? questionnaireId);
    }
}
