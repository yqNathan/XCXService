using GDD.Admin.Business.IBLL;
using GDD.Admin.DTO;
using GDD.Business;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Transactions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace GDD.Admin.Business.BLL
{
    /// <summary>
    /// 题目管理服务
    /// </summary>
    public class QuestionServer : Repository, IQuestionService
    {
        /// <summary>
        /// 获取题目集合
        /// </summary>
        /// <param name="title">题目名称</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns>题目集合</returns>
        public List<Question> GetQuestionList(string title, int pageIndex, int pageSize)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<Question> baseQuery = db.Question;
                if (!String.IsNullOrEmpty(title))
                {
                    baseQuery = baseQuery.Where(p => p.QuestionTitle.Contains(title));
                }
                var list = baseQuery.OrderBy(p => p.QuestionNumber).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取题目总数
        /// </summary>
        /// <param name="name">题目名称</param>
        /// <returns></returns>
        public int GetQuestionCount(Guid? questionnaireId ,string title)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<Question> baseQuery = db.Question;
                if (questionnaireId != null && questionnaireId != Guid.Empty)
                {
                    baseQuery = baseQuery.Where(p => p.QuestionnaireID == questionnaireId);  
                }
                if (!String.IsNullOrEmpty(title))
                {
                    baseQuery = baseQuery.Where(p => p.QuestionTitle.Contains(title));
                }
                var count = baseQuery.Count();
                return count;
            }
        }

        /// <summary>
        /// 添加题目
        /// </summary>
        /// <param name="questionDTO">题目对象</param>
        /// <returns></returns>
        public bool InsertQuestion(QuestionDTO questionDTO)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {
                    using (var db = base.GDDSVSPDb)
                    {
                        Guid questionID = Guid.NewGuid();
                        Question question = Mapper.Map<Question>(questionDTO);
                        question.QuestionID = questionID;
                        db.Question.Add(question);
                        db.SaveChanges();

                        List<QuestionOptionMapping> questionOptionMappings = new List<QuestionOptionMapping>();
                        for (int i = 0; i < questionDTO.Options.Count; i++)
                        {
                            QuestionOptionMapping obj = new QuestionOptionMapping();
                            obj.QuestionOptionID = Guid.NewGuid();
                            obj.QuestionID = questionID;
                            obj.OptionID = questionDTO.Options[i].OptionID;
                            questionOptionMappings.Add(obj);
                        }
                        db.QuestionOptionMapping.AddRange(questionOptionMappings);
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

        /// <summary>
        /// 修改题目
        /// </summary>
        /// <param name="Question">题目对象</param>
        /// <returns></returns>
        public bool UpdateQuestion(QuestionDTO questionDTO)
        {
             using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {
                    using (var db = base.GDDSVSPDb)
                    {
                        Question question = db.Question.SingleOrDefault(p => p.QuestionID == questionDTO.QuestionID);
                        question.QuestionnaireTypeID = questionDTO.QuestionnaireTypeID;
                        question.QuestionTitle = questionDTO.QuestionTitle;
                        question.QuestionNumber = questionDTO.QuestionNumber;
                        question.QuestionnaireID = questionDTO.QuestionnaireID;
                        question.QuestionTypeID = questionDTO.QuestionTypeID;
                        question.OptionTypeID = questionDTO.OptionTypeID;
                        question.MaxOptionNumber = questionDTO.MaxOptionNumber;
                        question.MinOptionNumber = questionDTO.MinOptionNumber;
                        question.IsAnswer = questionDTO.IsAnswer;
                        question.QuestionState = questionDTO.QuestionState;
                        db.SaveChanges();

                        var delList = db.QuestionOptionMapping.Where(p => p.QuestionID == questionDTO.QuestionID);
                        db.QuestionOptionMapping.RemoveRange(delList);
                        db.SaveChanges();

                        List<QuestionOptionMapping> questionOptionMappings = new List<QuestionOptionMapping>();
                        for (int i = 0; i < questionDTO.Options.Count; i++)
                        {
                            QuestionOptionMapping obj = new QuestionOptionMapping();
                            obj.QuestionOptionID = Guid.NewGuid();
                            obj.QuestionID = questionDTO.QuestionID;
                            obj.OptionID = questionDTO.Options[i].OptionID;
                            questionOptionMappings.Add(obj);
                        }
                        db.QuestionOptionMapping.AddRange(questionOptionMappings);
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

        /// <summary>
        /// 删除题目
        /// </summary>
        /// <param name="id">题目ID</param>
        /// <returns></returns>
        public bool DeleteQuestion(Guid id)
        {
            using (var db = base.GDDSVSPDb)
            {
                Question Question = db.Question.SingleOrDefault(p => p.QuestionID == id);
                db.Question.Remove(Question);
                return db.SaveChanges() > 0;
            }
        }

    }
}
