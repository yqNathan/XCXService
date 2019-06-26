using GDD.Admin.Business.IBLL;
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
    /// <summary>
    /// 题目类型服务
    /// </summary>
    public class QuestionTypeServer : Repository, IQuestionTypeService
    {
        /// <summary>
        /// 获取题目类型集合
        /// </summary>
        /// <param name="typeName">题目类型名称</param>
        /// <param name="questionTypeID">问卷类型ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public List<QuestionType> GetQuestionTypeList(string typeName, Guid questionnaireTypeID, int pageIndex, int pageSize)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<QuestionType> baseQuery = db.QuestionType;
                if (!String.IsNullOrEmpty(typeName))
                {
                    baseQuery = baseQuery.Where(p => p.QuestionTypeName.Contains(typeName));
                }
                if (questionnaireTypeID != Guid.Empty)
                {
                    baseQuery = baseQuery.Where(p => p.QuestionnaireTypeID == questionnaireTypeID);
                }
                
                var list = baseQuery.OrderByDescending(p => p.CreateTime).ThenBy(p => p.QuestionTypeID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取题目类型总数
        /// </summary>
        /// <param name="name">题目类型名称</param>
        /// <returns></returns>
        public int GetQuestionTypeCount(string typeName, Guid questionnaireTypeID)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<QuestionType> baseQuery = db.QuestionType;
                if (!String.IsNullOrEmpty(typeName))
                {
                    baseQuery = baseQuery.Where(p => p.QuestionTypeName.Contains(typeName));
                }
                if (questionnaireTypeID != Guid.Empty)
                {
                    baseQuery = baseQuery.Where(p => p.QuestionnaireTypeID == questionnaireTypeID);
                }
                var count = baseQuery.Count();
                return count;
            }
        }

        /// <summary>
        /// 添加题目类型
        /// </summary>
        /// <param name="Question">题目类型对象</param>
        /// <returns></returns>
        public bool InsertQuestionType(QuestionType QuestionType)
        {
            using (var db = base.GDDSVSPDb)
            {
                db.QuestionType.Add(QuestionType);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 修改题目类型
        /// </summary>
        /// <param name="Question">题目类型对象</param>
        /// <returns></returns>
        public bool UpdateQuestionType(QuestionType obj)
        {
            try
            {
                using (var db = base.GDDSVSPDb)
                {
                    QuestionType questionType = db.QuestionType.SingleOrDefault(p => p.QuestionTypeID == obj.QuestionTypeID);
                    questionType.QuestionnaireTypeID = obj.QuestionnaireTypeID;
                    questionType.QuestionTypeName = obj.QuestionTypeName;
                    questionType.ModifiedTime = obj.ModifiedTime;
                    questionType.Modifier = obj.Modifier;
                    return db.SaveChanges() > 0;
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除题目类型
        /// </summary>
        /// <param name="id">题目类型ID</param>
        /// <returns></returns>
        public bool DeleteQuestionType(Guid id)
        {
            using (var db = base.GDDSVSPDb)
            {
                QuestionType QuestionType = db.QuestionType.SingleOrDefault(p => p.QuestionTypeID == id);
                db.QuestionType.Remove(QuestionType);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 通过ID获取题目类型名称
        /// </summary>
        /// <param name="id">题目类型ID</param>
        /// <returns></returns>
        public string GetQuestionTypeNameById(Guid? id)
        {
            using (var db = base.GDDSVSPDb)
            {
                QuestionType questionType = db.QuestionType.SingleOrDefault(p => p.QuestionTypeID == id);
                return questionType?.QuestionTypeName;
            }
        }
    }
}
