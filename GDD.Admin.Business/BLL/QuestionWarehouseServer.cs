using GDD.Admin.Business.IBLL;
using GDD.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDD.Models;

namespace GDD.Admin.Business.BLL
{
    /// <summary>
    /// 题库服务
    /// </summary>
    public class QuestionWarehouseServer : Repository, IQuestionWarehouseService
    {

        /// <summary>
        /// 根据问卷ID获取题库ID
        /// </summary>
        /// <param name="questionnaireId">问卷ID</param>
        /// <returns></returns>
        public string GetQuestionWarehouseIDByQuestionnaireID(Guid questionnaireId)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<Questionnaire> baseQuery = db.Questionnaire;
                var id = "";
                if (questionnaireId != Guid.Empty)
                {
                    id = baseQuery.SingleOrDefault(p => p.QuestionnaireID == questionnaireId).QuestionWarehouseID.ToString();
                }
                return id;
            }
        }

        /// <summary>
        /// 获取题库集合
        /// </summary>
        /// <param name="qwName">题库名称</param>
        /// <param name="questionnaireTypeId">问卷ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public List<QuestionWarehouse> GetQuestionWarehouseList(string qwName, Guid questionnaireTypeId, int pageIndex, int pageSize)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<QuestionWarehouse> baseQuery = db.QuestionWarehouse;
                if (!String.IsNullOrEmpty(qwName))
                {
                    baseQuery = baseQuery.Where(p => p.QWName.Contains(qwName));
                }
                if (questionnaireTypeId != Guid.Empty)
                {
                    baseQuery = baseQuery.Where(p => p.QuestionnaireTypeID == questionnaireTypeId);
                }

                var list = baseQuery.OrderByDescending(p => p.CreateTime).ThenBy(p => p.QuestionWarehouseID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return list;
            }
        }

        /// <summary>
        /// 根据题库ID获取题库名称
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public string GetQuestionWarehouseNameById(Guid? id)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<QuestionWarehouse> baseQuery = db.QuestionWarehouse;
                if (id != Guid.Empty)
                {
                    return baseQuery.FirstOrDefault(p => p.QuestionWarehouseID == id).QWName;
                }
                return "";
            }
        }

        /// <summary>
        /// 获取题库总数
        /// </summary>
        /// <param name="qwName">题库名称</param>
        /// <param name="QuestionnaireId">问卷ID</param>
        /// <returns></returns>
        public int GetQuestionWarehouseCount(string qwName, Guid questionnaireTypeId)
        {
            using (var db = base.GDDSVSPDb)
            {
                IQueryable<QuestionWarehouse> baseQuery = db.QuestionWarehouse;
                if (!String.IsNullOrEmpty(qwName))
                {
                    baseQuery = baseQuery.Where(p => p.QWName.Contains(qwName));
                }
                if (questionnaireTypeId != Guid.Empty)
                {
                    baseQuery = baseQuery.Where(p => p.QuestionnaireTypeID == questionnaireTypeId);
                }

                var count = baseQuery.Count();
                return count;
            }
        }

        /// <summary>
        /// 添加题库
        /// </summary>
        /// <param name="questionWarehouse">题库对象</param>
        /// <returns></returns>
        public bool InsertQuestionWarehouse(QuestionWarehouse questionWarehouse)
        {
            using (var db = base.GDDSVSPDb)
            {
                db.QuestionWarehouse.Add(questionWarehouse);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 修改题库
        /// </summary>
        /// <param name="Question">题库对象</param>
        /// <returns></returns>
        public bool UpdateQuestionWarehouse(QuestionWarehouse obj)
        {
            using (var db = base.GDDSVSPDb)
            {
                QuestionWarehouse questionWarehouse = db.QuestionWarehouse.SingleOrDefault(p => p.QuestionWarehouseID == obj.QuestionWarehouseID);
                questionWarehouse.QuestionnaireTypeID = obj.QuestionnaireTypeID;
                questionWarehouse.QWName = obj.QWName;
                questionWarehouse.Version = obj.Version;
                questionWarehouse.ModifiedTime = obj.ModifiedTime;
                questionWarehouse.Modifier = obj.Modifier;
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// 删除题库
        /// </summary>
        /// <param name="id">题库ID</param>
        /// <returns></returns>
        public bool DeleteQuestionWarehouse(Guid id)
        {
            using (var db = base.GDDSVSPDb)
            {
                QuestionWarehouse questionWarehouse = db.QuestionWarehouse.SingleOrDefault(p => p.QuestionWarehouseID == id);
                db.QuestionWarehouse.Remove(questionWarehouse);
                return db.SaveChanges() > 0;
            }
        }
    }
}
