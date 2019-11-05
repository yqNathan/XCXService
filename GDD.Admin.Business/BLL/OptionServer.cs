using GDD.Admin.Business.IBLL;
using GDD.Admin.VO;
using GDD.Business;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Admin.Business.BLL
{
    public class OptionServer : Repository, IOptionService
    {
        /// <summary>
        /// 获取选项集合
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public List<Option> GetOptionList(string name, Guid? QuestionnaireTypeID, int pageIndex, int pageSize)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<Option> baseQuery = db.Option;
                if (!String.IsNullOrEmpty(name))
                {
                    baseQuery = baseQuery.Where(p => p.OptionName.Contains(name));
                }
                if (QuestionnaireTypeID != null && QuestionnaireTypeID.ToString() != "" && QuestionnaireTypeID  != Guid.Empty)
                {
                    baseQuery = baseQuery.Where(p => p.QuestionnaireTypeID == QuestionnaireTypeID);
                }
                var list = baseQuery.OrderByDescending(p => p.CreateTime).ThenBy(x => x.OptionID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取选项总数
        /// </summary>
        /// <param name="name">选项名称</param>
        /// <returns></returns>
        public int GetOptionCount(string name, Guid? QuestionnaireTypeID)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<Option> baseQuery = db.Option;
                if (!String.IsNullOrEmpty(name))
                {
                    baseQuery = baseQuery.Where(p => p.OptionName.Contains(name));
                }
                if (QuestionnaireTypeID != null && QuestionnaireTypeID.ToString() != "" && QuestionnaireTypeID != Guid.Empty)
                {
                    baseQuery = baseQuery.Where(p => p.QuestionnaireTypeID == QuestionnaireTypeID);
                }
                var count = baseQuery.Count();
                return count;
            }
        }

        public List<OptionType> GetOptionTypeList()
        {
            using (var db = base.GDDSVSPDb)
            {
                return db.OptionType.ToList();
            }
        }

        /// <summary>
        /// 添加选项
        /// </summary>
        /// <param name="option">选项对象</param>
        /// <returns></returns>
        public bool InsertOption(Option option)
        {
            using (var db = base.GDDSVSPDb)
            {
                db.Option.Add(option);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 修改选项
        /// </summary>
        /// <param name="option">选项对象</param>
        /// <returns></returns>
        public bool UpdateOption(Option obj)
        {
            try
            {
                using (var db = base.GDDSVSPDb)
                {
                    Option Option = db.Option.SingleOrDefault(p => p.OptionID == obj.OptionID);
                    Option.OptionName = obj.OptionName;
                    Option.OptionNumber = obj.OptionNumber;
                    Option.OptionScore = obj.OptionScore;
                    Option.QuestionnaireTypeID = obj.QuestionnaireTypeID;
                    return db.SaveChanges() > 0;
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 删除选项
        /// </summary>
        /// <param name="id">选项ID</param>
        /// <returns></returns>
        public bool DeleteOption(Guid id)
        {
            using (var db = base.GDDSVSPDb)
            {
                Option Option = db.Option.SingleOrDefault(p => p.OptionID == id);
                db.Option.Remove(Option);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 通过ID获取选项名称
        /// </summary>
        /// <param name="id">选项ID</param>
        /// <returns></returns>
        public string GetOptionNameById(Guid? id)
        {
            using (var db = base.GDDSVSPDb)
            {
                Option option = db.Option.SingleOrDefault(p => p.OptionID == id);
                return option?.OptionName;
            }
        }

        /// <summary>
        /// 通过问题ID获取相应的选项集合
        /// </summary>
        /// <param name="id">选项ID</param>
        /// <returns></returns>
        public List<Option> GetOptionByQuestionId(Guid? questionId)
        {
            using (var db = base.GDDSVSPDb)
            {
                var list = new List<Option>();
                if (questionId != null)
                {
                    list = (from q in db.QuestionOptionMapping
                            join o in db.Option on q.OptionID equals o.OptionID
                            where q.QuestionID == questionId
                            select o).ToList();
                }
                return list;
            }
        }

        /// <summary>
        /// 获取分数图表集合
        /// </summary>
        /// <param name="name">题目名称</param>
        /// <param name="departmentId">部门ID</param>
        /// <param name="questionnaireId">问卷ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public ScoreCharResult GetScoreChartList(string name, Guid? departmentId, Guid? functionalGroupID, Guid? questionnaireId, Guid? optionTypeId, Guid? questionTypeID, object[] qIds, int pageIndex, int pageSize)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<QuestionAnswerMapping> query = db.QuestionAnswerMapping;
                IQueryable<QuestionOptionMapping> questionOptionMapping = db.QuestionOptionMapping;
                IQueryable<Option> option = db.Option;
                IQueryable<Question> question = db.Question;
                List<Guid?> questionIds = new List<Guid?>();
                if (departmentId != Guid.Empty && departmentId != null)
                {
                    query = query.Where(p => p.DepartmentID == departmentId);
                }
                if (functionalGroupID != Guid.Empty && functionalGroupID != null)
                {
                    query = query.Where(p => p.FunctionalGroupID == functionalGroupID);
                }
                if (questionnaireId != Guid.Empty && questionnaireId != null)
                {
                    query = query.Where(p => p.QuestionnaireID == questionnaireId);
                }
                if (optionTypeId != Guid.Empty && optionTypeId != null)
                {
                    question = question.Where(p => p.OptionTypeID == optionTypeId);
                }
                if (questionTypeID != Guid.Empty && questionTypeID != null)
                {
                    question = question.Where(p => p.QuestionTypeID == questionTypeID);
                }
                if (qIds.Length > 0)
                {
                    for (int i = 0; i < qIds.Length; i++)
                    {
                        questionIds.Add(Guid.Parse(qIds[i].ToString()));
                    }
                    question = question.Where(p => questionIds.Contains(p.QuestionID));
                    questionOptionMapping = questionOptionMapping.Where(p => questionIds.Contains(p.QuestionID));
                    query = query.Where(p => questionIds.Contains(p.QuestionID));
                }

                var scoreChartList = from q1 in query
                                     join e1 in db.Employee on q1.EmployeeID equals e1.EmployeeID into data1
                                     from obj1 in data1.DefaultIfEmpty()
                                     join d1 in db.Department on q1.DepartmentID equals d1.DepartmentID into data2
                                     from obj2 in data2.DefaultIfEmpty()
                                     join f1 in db.FunctionalGroup on q1.FunctionalGroupID equals f1.FunctionalGroupID into data3
                                     from obj3 in data3.DefaultIfEmpty()
                                     join q2 in question on q1.QuestionID equals q2.QuestionID into data4
                                     from obj4 in data4.DefaultIfEmpty()
                                     join o1 in option on q1.OptionID equals o1.OptionID into data5
                                     from obj5 in data5.DefaultIfEmpty()
                                     join qt1 in db.QuestionType on obj4.QuestionTypeID equals qt1.QuestionTypeID into data6
                                     from obj6 in data6.DefaultIfEmpty()
                                     join ot1 in db.OptionType on obj4.OptionTypeID equals ot1.OptionTypeID into data7
                                     from obj7 in data7.DefaultIfEmpty()
                                     select new ScoreChart
                                     {
                                         EmployeeName = obj1.EmployeeName,
                                         DepartmentName = obj2.DepartmentName,
                                         FunctionalGroupName = obj3.FunctionalGroupName,
                                         OptionName = obj5.OptionName,
                                         OptionId = obj5.OptionID,
                                         OptionNumber = obj5.OptionNumber,
                                         OptionReason = q1.OptionReason,
                                         OptionScore = obj5.OptionScore,
                                         QuestionID = obj4.QuestionID,
                                         QuestionNumber = obj4.QuestionNumber,
                                         QuestionName = obj4.QuestionTitle,
                                         QuestionTypeName = obj6.QuestionTypeName,
                                         CreateTime = q1.CreateTime,
                                         OptionTypeName = obj7.OptionTypeName,
                                         OptionTypeID = obj7.OptionTypeID
                                     };

                if (!string.IsNullOrEmpty(name))
                {
                    scoreChartList = scoreChartList.Where(p => p.QuestionName.Contains(name));
                }
                if (optionTypeId != Guid.Empty && optionTypeId != null)
                {
                    scoreChartList = scoreChartList.Where(p => p.OptionTypeID == optionTypeId);
                }

                var questionIdList = questionOptionMapping.GroupBy(p => p.QuestionID).Select(p => p.Key).ToList();
                var optionIdList = from obj in scoreChartList where questionIdList.Contains(obj.QuestionID) select obj.OptionId;
                var maxValue = (from obj in option where optionIdList.Contains(obj.OptionID) select obj.OptionNumber).Max(p => p.Value);


                var optionsList = (from u in scoreChartList
                                   group u by new { QuestionNumber = u.QuestionNumber, OptionId = u.OptionId, OptionNumber = u.OptionNumber, OptionName = u.OptionName } into g
                                   select new { g.Key.QuestionNumber, g.Key.OptionNumber, g.Key.OptionId, g.Key.OptionName }).OrderBy(p => p.QuestionNumber).ThenBy(p => p.OptionNumber).ToList();

                var questionList = (from u in scoreChartList
                                    group u by new { QuestionNumber = u.QuestionNumber, QuestionName = u.QuestionName, OptionTypeName = u.OptionTypeName } into g
                                    select new { g.Key.QuestionNumber, g.Key.QuestionName, g.Key.OptionTypeName }).OrderBy(p => p.QuestionNumber).ToList();

                var optionsNumberList = (from u in scoreChartList
                                         group u by new { OptionNumber = u.OptionNumber } into g
                                         select new { g.Key.OptionNumber }).OrderBy(p => p.OptionNumber).ToList();

                //List<QuestionChart> questionCharts = questionList.Where(p => p.OptionTypeCode != "textarea").Select(questionChart => new QuestionChart()
                List <QuestionChart> questionCharts = questionList.Select(questionChart => new QuestionChart()
                {
                    QuestionName = questionChart.QuestionName,
                    QuestionNumber = questionChart.QuestionNumber,
                    OptionChartList = optionsList.Where(p => p.QuestionNumber == questionChart.QuestionNumber).Select(p => new OptionChart()
                    {
                        OptionName = p.OptionName,
                        OptionNumber = p.OptionNumber,
                        OptionCount = scoreChartList.Where(x => x.OptionId == p.OptionId && x.QuestionNumber == questionChart.QuestionNumber).Count()
                    }).ToList()
                }).ToList();

                //for循环效率要快一点，代码可读性相对较差
                //for (int i = 0; i < questionList.Count; i++)
                //{
                //    QuestionChart questionChart = new QuestionChart();
                //    questionChart.QuestionName = questionList[i].QuestionName;
                //    questionChart.QuestionNumber = questionList[i].QuestionNumber;
                //    //List<OptionChart> optionCharts = new List<OptionChart>();
                //    List<OptionChart> optionCharts = optionsList.Where(p => p.QuestionNumber == questionList[i].QuestionNumber).Select(p => new OptionChart()
                //    {
                //        OptionName = p.OptionName,
                //        OptionNumber = p.OptionNumber,
                //        OptionCount = scoreChartList.Where(x => x.OptionId == p.OptionId).Count()
                //    }).ToList();
                //    //for (int j = 0; j < optionsList.Count; j++)
                //    //{
                //    //    var item = optionsList[j];
                //    //    if (item.QuestionNumber == questionList[i].QuestionNumber)
                //    //    {
                //    //        OptionChart optionChart = new OptionChart();
                //    //        optionChart.OptionName = item.OptionName;
                //    //        optionChart.OptionNumber = item.OptionNumber;
                //    //        optionChart.OptionCount = scoreChartList.Where(p => p.OptionId == item.OptionId).Count();
                //    //        optionCharts.Add(optionChart);
                //    //    }
                //    //}
                //    questionChart.OptionChartList = optionCharts;
                //    questionCharts.Add(questionChart);
                //}
                List<string> optionsNumbers = new List<string>();
                //for (int i = 0; i < optionsNumberList.Count; i++)
                //{
                //    optionsNumbers.Add(optionsNumberList[i].OptionNumber.ToString());
                //}
                for (int i = 0; i < maxValue; i++)
                {
                    optionsNumbers.Add((i + 1).ToString());
                }
                var list = scoreChartList.OrderByDescending(p => p.CreateTime).ThenBy(p => p.QuestionNumber).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                ScoreCharResult scoreCharResult = new ScoreCharResult();
                scoreCharResult.OptionNumberList = optionsNumbers;
                scoreCharResult.QuestionChartList = questionCharts;
                scoreCharResult.ScoreChartList = list;
                return scoreCharResult;
            }
        }

        /// <summary>x
        /// 获取分数图表数量
        /// </summary>
        /// <param name="name">题目名称</param>
        /// <param name="departmentId">部门ID</param>
        /// <param name="questionnaireId">问卷ID</param>
        /// <returns></returns>
        public int GetScoreChartCount(string name, Guid? departmentId, Guid? functionalGroupID, Guid? questionnaireId, Guid? optionTypeId,Guid? questionTypeID,object[] qIds)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<QuestionAnswerMapping> query = db.QuestionAnswerMapping;
                IQueryable<Question> question = db.Question;
                List<Guid?> questionIds = new List<Guid?>();
                if (departmentId != Guid.Empty && departmentId != null)
                {
                    query = query.Where(p => p.DepartmentID == departmentId);
                }
                if (functionalGroupID != Guid.Empty && functionalGroupID != null)
                {
                    query = query.Where(p => p.FunctionalGroupID == functionalGroupID);
                }
                if (questionnaireId != Guid.Empty && questionnaireId != null)
                {
                    query = query.Where(p => p.QuestionnaireID == questionnaireId);
                }
                if (optionTypeId != Guid.Empty && optionTypeId != null)
                {
                    question = question.Where(p => p.OptionTypeID == optionTypeId);
                }
                if (questionTypeID != Guid.Empty && questionTypeID != null)
                {
                    question = question.Where(p => p.QuestionTypeID == questionTypeID);
                }
                if (qIds.Length > 0)
                {
                    for (int i = 0; i < qIds.Length; i++)
                    {
                        questionIds.Add(Guid.Parse(qIds[i].ToString()));
                    }
                    question = question.Where(p => questionIds.Contains(p.QuestionID));
                    query = query.Where(p => questionIds.Contains(p.QuestionID));
                }

                var scoreChartList = from q1 in query
                                     join e1 in db.Employee on q1.EmployeeID equals e1.EmployeeID into data1
                                     from obj1 in data1.DefaultIfEmpty()
                                     join d1 in db.Department on q1.DepartmentID equals d1.DepartmentID into data2
                                     from obj2 in data2.DefaultIfEmpty()
                                     join f1 in db.FunctionalGroup on q1.FunctionalGroupID equals f1.FunctionalGroupID into data3
                                     from obj3 in data3.DefaultIfEmpty()
                                     join q2 in question on q1.QuestionID equals q2.QuestionID into data4
                                     from obj4 in data4.DefaultIfEmpty()
                                     join o1 in db.Option on q1.OptionID equals o1.OptionID into data5
                                     from obj5 in data5.DefaultIfEmpty()
                                     join qt1 in db.QuestionType on obj4.QuestionTypeID equals qt1.QuestionTypeID into data6
                                     from obj6 in data6.DefaultIfEmpty()
                                     join ot1 in db.OptionType on obj4.OptionTypeID equals ot1.OptionTypeID into data7
                                     from obj7 in data7.DefaultIfEmpty()
                                     select new ScoreChart
                                     {
                                         EmployeeName = obj1.EmployeeName,
                                         DepartmentName = obj2.DepartmentName,
                                         FunctionalGroupName = obj3.FunctionalGroupName,
                                         OptionName = obj5.OptionName,
                                         OptionNumber = obj5.OptionNumber,
                                         OptionReason = q1.OptionReason,
                                         OptionScore = obj5.OptionScore,
                                         QuestionName = obj4.QuestionTitle,
                                         QuestionTypeName = obj6.QuestionTypeName,
                                         CreateTime = q1.CreateTime,
                                         OptionTypeName = obj7.OptionTypeName,
                                         OptionTypeID = obj7.OptionTypeID
                                     };
                if (!string.IsNullOrEmpty(name))
                {
                    scoreChartList = scoreChartList.Where(p => p.QuestionName.Contains(name));
                }
                if (optionTypeId != Guid.Empty && optionTypeId != null)
                {
                    scoreChartList = scoreChartList.Where(p => p.OptionTypeID == optionTypeId);
                }
                var count = scoreChartList.Count();
                return count;
            }
        }
    }
}
