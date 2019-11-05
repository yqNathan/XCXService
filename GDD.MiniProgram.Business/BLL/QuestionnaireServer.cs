using GDD.Business;
using GDD.Common;
using GDD.MiniProgram.Business.IBLL;
using GDD.MiniProgram.DTO;
using GDD.MiniProgram.VO;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GDD.MiniProgram.Business.BLL
{
    public class QuestionnaireServer : Repository, IQuestionnaireService
    {
        /// <summary>
        /// 获取问所有的问卷类型集合
        /// </summary>
        /// <returns></returns>
        public List<QuestionnaireVO> GetQuestionnaireTypeList(Guid? employeeID)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<QuestionnaireType> baseQuery = db.QuestionnaireType;
                IQueryable<Questionnaire> questionnaire = db.Questionnaire;
                IQueryable<QuestionnaireSubmission> submission = db.QuestionnaireSubmission;
                DateTime nowDate = DateTime.Now;
                questionnaire = questionnaire.Where(p => p.StartTime <= nowDate && p.EndTime >= nowDate);
                submission = submission.Where(p => p.IsSubmit == 1 && p.EmployeeID == employeeID);

                var voQuery = from t1 in baseQuery
                                join q1 in questionnaire on t1.QuestionnaireTypeID equals q1.QuestionnaireTypeID into data1
                                from obj1 in data1.DefaultIfEmpty()
                                join s1 in submission on obj1.QuestionnaireID equals s1.QuestionnaireID into data2
                                from obj2 in data2.DefaultIfEmpty()
                                select new QuestionnaireVO
                                {
                                    QuestionnaireTypeID = t1.QuestionnaireTypeID,
                                    QuestionnaireTypeName = t1.QuestionnaireTypeName,
                                    QuestionnaireID = obj1.QuestionnaireID,
                                    QuestionnaireName = obj1.QuestionnaireName,
                                    LowestScore = obj1.LowestScore,
                                    State = obj1.QuestionnaireID != null ? "已发布" : "未发布",
                                    IsSubmit = obj2.IsSubmit != null ? "已提交" : "未提交",
                                    CreateTime = t1.CreateTime,
                                    Describe = obj1.Describe,
                                    IsRelation = obj1.IsRelation,
                                    StartTime = obj1.StartTime,
                                    EndTime = obj1.EndTime
                                };

                var volist = voQuery.OrderByDescending(p => p.CreateTime).OrderBy(p => p.QuestionnaireTypeID).ToList();
                return volist;
            }
        }

        /// <summary>
        /// 通过类型ID获取问卷信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Questionnaire GetQuestionnaireByTypeId(Guid id)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<Questionnaire> baseQuery = db.Questionnaire;
                if (id != null && id != Guid.Empty)
                {
                    baseQuery = baseQuery.Where(p => p.QuestionnaireTypeID == id);
                }
                DateTime nowDate = DateTime.Now;
                var obj = baseQuery.SingleOrDefault(p => p.StartTime <= nowDate && p.EndTime >= nowDate);
                return obj;
            }
        }

        /// <summary>
        /// 通过问卷类型ID获取题目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<QuestionVO> GetQuestionListById(Guid questionnaireTypeID, Guid questionnaireID)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<QuestionWarehouse> questionWarehouse = db.QuestionWarehouse;
                IQueryable<QuestionOptionMapping> questionOptionMapping = db.QuestionOptionMapping;
                IQueryable<Option> option = db.Option;
                IQueryable<Question> question = db.Question;
                IQueryable<QuestionType> questionType = db.QuestionType;
                IQueryable<Questionnaire> questionnaire = db.Questionnaire;
                if (questionnaireTypeID != null && questionnaireTypeID != Guid.Empty)
                {
                    question = question.Where(p => p.QuestionnaireTypeID == questionnaireTypeID);
                    questionType = questionType.Where(p => p.QuestionnaireTypeID == questionnaireTypeID);
                }
                if (questionnaireID != null && questionnaireID != Guid.Empty)
                {
                    var obj = questionnaire.FirstOrDefault(p => p.QuestionnaireID == questionnaireID);
                    question = question.Where(p => p.QuestionWarehouseID == obj.QuestionWarehouseID);
                    questionOptionMapping = questionOptionMapping.Where(p => p.QuestionWarehouseID == obj.QuestionWarehouseID);
                }

                var voQuery = from qom in questionOptionMapping
                              join q1 in question on qom.QuestionID equals q1.QuestionID into data1
                              from obj1 in data1.DefaultIfEmpty()
                              join o1 in option on qom.OptionID equals o1.OptionID into data2
                              from obj2 in data2.DefaultIfEmpty()
                              join t1 in questionType on obj1.QuestionTypeID equals t1.QuestionTypeID into data3
                              from obj3 in data3.DefaultIfEmpty()
                              join ot in db.OptionType on obj1.OptionTypeID equals ot.OptionTypeID into data4
                              from obj4 in data4.DefaultIfEmpty()
                              select new QustionTypeVO
                              {
                                  QuestionTypeID = obj3.QuestionTypeID,
                                  QuestionTypeName = obj3.QuestionTypeName,
                                  QuestionID = qom.QuestionID,
                                  QuestionTitle = obj1.QuestionTitle,
                                  QuestionNumber = obj1.QuestionNumber,
                                  OptionTypeCode = obj4.OptionTypeCode,
                                  MaxOptionNumber = obj1.MaxOptionNumber,
                                  MinOptionNumber = obj1.MinOptionNumber,
                                  IsAnswer = obj1.IsAnswer,
                                  QuestionState = obj1.QuestionState,
                                  OptionID = qom.OptionID,
                                  OptionName = obj2.OptionName,
                                  OptionNumber = obj2.OptionNumber,
                                  OptionScore = obj2.OptionScore,
                                  OptionTitle = obj2.OptionTitle
                              }
                              into data5
                              group data5 by new
                              {
                                  data5.QuestionTypeID,
                                  data5.QuestionTypeName
                              }
                              into grp
                              select new QuestionVO()
                              {
                                  QuestionTypeID = grp.Key.QuestionTypeID,
                                  QuestionTypeName = grp.Key.QuestionTypeName,
                                  Questions = (from qo in grp
                                               group qo by new
                                               {
                                                   qo.QuestionID,
                                                   qo.OptionTypeCode,
                                                   qo.QuestionTitle,
                                                   qo.MaxOptionNumber,
                                                   qo.MinOptionNumber,
                                                   qo.IsAnswer,
                                                   qo.QuestionState,
                                                   qo.QuestionNumber
                                               }
                                   into grp1
                                               select new QuestionOptionVO()
                                               {
                                                   QuestionID = grp1.Key.QuestionID,
                                                   OptionTypeCode = grp1.Key.OptionTypeCode,
                                                   QuestionTitle = grp1.Key.QuestionTitle,
                                                   QuestionNumber = grp1.Key.QuestionNumber,
                                                   MaxOptionNumber = grp1.Key.MaxOptionNumber,
                                                   MinOptionNumber = grp1.Key.MinOptionNumber,
                                                   IsAnswer = grp1.Key.IsAnswer,
                                                   QuestionState = grp1.Key.QuestionState,
                                                   Options = (from opt in grp1
                                                              group opt by new
                                                              {
                                                                  opt.OptionID,
                                                                  opt.OptionName,
                                                                  opt.OptionNumber,
                                                                  opt.OptionScore,
                                                                  opt.OptionTitle,
                                                              }
                                                   into grp2
                                                              select new OptionVO()
                                                              {
                                                                  OptionID = grp2.Key.OptionID,
                                                                  OptionName = grp2.Key.OptionName,
                                                                  OptionNumber = grp2.Key.OptionNumber,
                                                                  OptionScore = grp2.Key.OptionScore,
                                                                  OptionTitle = grp2.Key.OptionTitle
                                                              }).OrderBy(p => p.OptionNumber).ToList()
                                               }).OrderBy(p => p.QuestionNumber).ToList()
                              };
                return voQuery.ToList();
            }
        }

        /// <summary>
        /// 人员提交问卷数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SubmitQuestionnaire(QuestionnaireDTO dto)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {
                    using (var db = base.GDDSVSPDb)
                    {
                        Guid id = Guid.NewGuid();
                        DateTime date = DateTime.Now;
                        int isDel = Convert.ToInt32(IsDel.未删除);
                        Guid? employeeId = dto.EmployeeID;
                        Guid questionnaireId = dto.QuestionnaireID;
                        Guid departmentId = dto.DepartmentID;
                        Guid functionalGroupId = dto.FunctionalGroupID;
                        int isRelation = dto.IsRelation;
                        QuestionnaireSubmission qs = new QuestionnaireSubmission();
                        qs.ID = id;
                        qs.QuestionnaireID = questionnaireId;
                        qs.EmployeeID = employeeId;
                        qs.IsSubmit = 1;
                        qs.CreateTime = date;
                        qs.IsDel = isDel;
                        db.QuestionnaireSubmission.Add(qs);
                        db.SaveChanges();

                        if (isRelation == Convert.ToInt32(QuestionnaireIsRelation.不关联))
                        {
                            employeeId = null;
                        }
                        List<QuestionAnswerMapping> list = new List<QuestionAnswerMapping>();
                        List<QuestionDTO> questions = dto.Questions;
                        for (int i = 0; i < questions.Count; i++)
                        {
                            QuestionAnswerMapping obj = new QuestionAnswerMapping();
                            obj.QuestionAnswerID = Guid.NewGuid();
                            obj.QuestionID = questions[i].QuestionID;
                            obj.OptionID = questions[i].OptionID;
                            obj.OptionReason = questions[i].OptionReason;
                            obj.EmployeeID = employeeId;
                            obj.QuestionnaireID = questionnaireId;
                            obj.DepartmentID = departmentId;
                            obj.FunctionalGroupID = functionalGroupId;
                            obj.CreateTime = date;
                            obj.IsDel = isDel;
                            list.Add(obj);
                        }
                        db.QuestionAnswerMapping.AddRange(list);
                        db.SaveChanges();

                        transaction.Complete();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                    return false;
                }
            }
            return true;
        }
    }
}
