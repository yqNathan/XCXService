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
    public class QuestionnaireServer : Repository ,IQuestionnaireService
    {

        /// <summary>
        /// 获取问卷集合
        /// </summary>
        /// <param name="name">问卷名称</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public List<Questionnaire> GetQuestionnaireList(string name,Guid typeId, int pageIndex, int pageSize)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<Questionnaire> baseQuery = db.Questionnaire;
                if (!String.IsNullOrEmpty(name))
                {
                    baseQuery = baseQuery.Where(p => p.QuestionnaireName.Contains(name));
                }
                if (typeId != Guid.Empty)
                {
                    baseQuery = baseQuery.Where(p => p.QuestionnaireTypeID == typeId);
                }
                var list = baseQuery.OrderBy(p=>p.CreateTime).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取问卷总数
        /// </summary>
        /// <param name="name">问卷名称</param>
        /// <returns></returns>
        public int GetQuestionnaireCount(string name,Guid typeId)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<Questionnaire> baseQuery = db.Questionnaire;
                if (!String.IsNullOrEmpty(name))
                {
                    baseQuery = baseQuery.Where(p => p.QuestionnaireName.Contains(name));
                }
                if (typeId != Guid.Empty)
                {
                    baseQuery = baseQuery.Where(p => p.QuestionnaireTypeID == typeId);
                }
                var count = baseQuery.Count();
                return count;
            }
        }

        /// <summary>
        /// 添加问卷
        /// </summary>
        /// <param name="questionnaire">问卷对象</param>
        /// <returns></returns>
        public bool InsertQuestionnaire(Questionnaire questionnaire)
        {
            using (var db = base.GDDSVSPDb)
            {
                questionnaire.QuestionnaireID = Guid.NewGuid();
                questionnaire.CreateTime = DateTime.Now;
                db.Questionnaire.Add(questionnaire);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 修改问卷
        /// </summary>
        /// <param name="questionnaire">问卷对象</param>
        /// <returns></returns>
        public bool UpdateQuestionnaire(Questionnaire obj)
        {
            try
            {
                using (var db = base.GDDSVSPDb)
                {
                    Questionnaire questionnaire = db.Questionnaire.SingleOrDefault(p => p.QuestionnaireID == obj.QuestionnaireID);
                    questionnaire.QuestionnaireName = obj.QuestionnaireName;
                    questionnaire.Version = obj.Version;
                    questionnaire.LowestScore = obj.LowestScore;
                    questionnaire.IsRelation = obj.IsRelation;
                    questionnaire.Describe = obj.Describe;
                    questionnaire.StartTime = obj.StartTime;
                    questionnaire.EndTime = obj.EndTime;
                    questionnaire.State = obj.State;
                    return db.SaveChanges() > 0;
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            
        }


        /// <summary>
        /// 删除问卷
        /// </summary>
        /// <param name="id">问卷ID</param>
        /// <returns></returns>
        public bool DeleteQuestionnaire(Guid id)
        {
            using (var db = base.GDDSVSPDb)
            {
                Questionnaire questionnaire = db.Questionnaire.SingleOrDefault(p => p.QuestionnaireID == id);
                db.Questionnaire.Remove(questionnaire);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 通过ID获取问卷名称
        /// </summary>
        /// <param name="id">问卷ID</param>
        /// <returns></returns>
        public string GetQuestionnaireNameById(Guid? id)
        {
            using (var db = base.GDDSVSPDb)
            {
                Questionnaire questionnaire = db.Questionnaire.SingleOrDefault(p => p.QuestionnaireID == id);
                return questionnaire?.QuestionnaireName;
            }
        }


        /// <summary>
        /// 获取已提交人员总数
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="departmentId">部门ID</param>
        /// <param name="questionnaireId">问卷ID</param>
        /// <param name="isSubmit">是否已提交  0：否  1：是</param>
        /// <returns></returns>
        public int GetSubmittedCountByQuestionnaireId(string name ,Guid? questionnaireId, Guid? departmentId, int isSubmit)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<Employee> employees = db.Employee;
                IQueryable<QuestionnaireSubmission> questionnaireSubmission = db.QuestionnaireSubmission;
                if (!String.IsNullOrEmpty(name))
                {
                    employees = employees.Where(p => p.EmployeeName.Contains(name));
                }
                if (departmentId != Guid.Empty)
                {
                    employees = employees.Where(p => p.DepartmentID == departmentId);
                }
                if (questionnaireId != Guid.Empty)
                {
                    questionnaireSubmission = questionnaireSubmission.Where(p => p.QuestionnaireID == questionnaireId);
                }
                var query = (from e in employees
                                join q in questionnaireSubmission on e.EmployeeID equals q.EmployeeID
                                into obj from tab in obj.DefaultIfEmpty()
                                select new{  IsSubmit = tab.IsSubmit == null ? 0 : tab.IsSubmit });
                if (isSubmit == -1)
                {
                    query = query.Where(p => p.IsSubmit == 1);
                }
                else
                {
                    query = query.Where(p => p.IsSubmit == isSubmit);
                }

                int submittedCount = query.Count();
                return submittedCount;
            }
        }

        
    }
}
