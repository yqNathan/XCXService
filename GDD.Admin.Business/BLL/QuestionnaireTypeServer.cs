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
    /// 问卷管理服务
    /// </summary>
    public class QuestionnaireTypeServer : Repository ,IQuestionnaireTypeService
    {

        /// <summary>
        /// 获取问卷类型集合
        /// </summary>
        /// <param name="name">问卷类型名称</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public List<QuestionnaireType> GetQuestionnaireTypeList(string name, int pageIndex, int pageSize)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<QuestionnaireType> baseQuery = db.QuestionnaireType;
                if (!String.IsNullOrEmpty(name))
                {
                    baseQuery = baseQuery.Where(p => p.QuestionnaireTypeName.Contains(name));
                }
                var list = baseQuery.OrderByDescending(p=>p.CreateTime).OrderBy(p=>p.QuestionnaireTypeID).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取问卷类型总数
        /// </summary>
        /// <param name="name">问卷类型名称</param>
        /// <returns></returns>
        public int GetQuestionnaireTypeCount(string name)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<QuestionnaireType> baseQuery = db.QuestionnaireType;
                if (!String.IsNullOrEmpty(name))
                {
                    baseQuery = baseQuery.Where(p => p.QuestionnaireTypeName.Contains(name));
                }
                var count = baseQuery.Count();
                return count;
            }
        }

        /// <summary>
        /// 添加问卷类型
        /// </summary>
        /// <param name="questionnaire">问卷类型对象</param>
        /// <returns></returns>
        public bool InsertQuestionnaireType(QuestionnaireType questionnaireType)
        {
            using (var db = base.GDDSVSPDb)
            {
                db.QuestionnaireType.Add(questionnaireType);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 修改问卷类型
        /// </summary>
        /// <param name="questionnaire">问卷类型对象</param>
        /// <returns></returns>
        public bool UpdateQuestionnaireType(QuestionnaireType obj)
        {
            try
            {
                using (var db = base.GDDSVSPDb)
                {
                    QuestionnaireType questionnaireType = db.QuestionnaireType.SingleOrDefault(p => p.QuestionnaireTypeID == obj.QuestionnaireTypeID);
                    questionnaireType.QuestionnaireTypeName = obj.QuestionnaireTypeName;
                    questionnaireType.ModifiedTime = obj.ModifiedTime;
                    questionnaireType.Modifier = obj.Modifier;
                    return db.SaveChanges() > 0;
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除问卷类型
        /// </summary>
        /// <param name="id">问卷类型ID</param>
        /// <returns></returns>
        public bool DeleteQuestionnaireType(Guid id)
        {
            using (var db = base.GDDSVSPDb)
            {
                QuestionnaireType questionnaireType = db.QuestionnaireType.SingleOrDefault(p => p.QuestionnaireTypeID == id);
                db.QuestionnaireType.Remove(questionnaireType);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 通过ID获取问卷类型名字
        /// </summary>
        /// <param name="id">问卷类型Id</param>
        /// <returns></returns>
        public string GetQuestionnaireTypeNameById(Guid? id)
        {
            using (var db = base.GDDSVSPDb)
            {
                QuestionnaireType questionnaireType = db.QuestionnaireType.SingleOrDefault(p => p.QuestionnaireTypeID == id);
                return questionnaireType?.QuestionnaireTypeName;
            }
        }

    }
}
