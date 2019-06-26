using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.Web.Extensions;
using GDD.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDD.Admin.Web.Controllers
{
    [RoutePrefix("QuestionnaireType")]
    public class QuestionnaireTypeController : BaseController
    {
        IQuestionnaireTypeService questionnaireTypeService;
        private static readonly ILog log = LogManager.GetLogger(typeof(QuestionnaireTypeController));
            
        public QuestionnaireTypeController()
        {
            questionnaireTypeService = new QuestionnaireTypeServer();
        }

        // GET: QuestionnaireType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuestionnaireTypeDetail(string obj)
        {
            QuestionnaireType vo = new QuestionnaireType();
            if (!string.IsNullOrEmpty(obj))
            {
                vo = JsonConvert.DeserializeObject<QuestionnaireType>(obj);
                ViewData["QuestionnaireTypeData"] = vo;
            }
            else
            {
                vo.QuestionnaireTypeID = Guid.Empty;
                ViewData["QuestionnaireTypeData"] = vo;
            }
            return View();
        }

        [HttpGet]
        [Route("List")]
        public JsonResult GetQuestionnaireTypeList(string name, int pageIndex, int pageSize)
        {
            List<QuestionnaireType> list = null;
            JsonResult result = new JsonResult();
            int count = 0;
            try
            {
                list = questionnaireTypeService.GetQuestionnaireTypeList(name, pageIndex, pageSize);
                count = questionnaireTypeService.GetQuestionnaireTypeCount(name);
                log.Info("查询成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { code = 0, msg = "获取问卷类型", count = count, data = list }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [Route("Add")]
        public JsonResult InsertQuestionnaireType(QuestionnaireType questionnaireType)
        {
            JsonResult result = new JsonResult();
            try
            {
                questionnaireType.QuestionnaireTypeID = Guid.NewGuid();
                questionnaireType.CreateTime = DateTime.Now;
                questionnaireType.Creator = (Session["user"] as Employee)?.EmployeeName;
                bool isSuccess = questionnaireTypeService.InsertQuestionnaireType(questionnaireType);
                log.Info("添加成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { msg = "添加成功" }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [Route("Edit")]
        public JsonResult UpdateQuestionnaireType(QuestionnaireType questionnaireType)
        {
            JsonResult result = new JsonResult();
            string msg = "";
            try
            {
                questionnaireType.ModifiedTime = DateTime.Now;
                questionnaireType.Modifier = (Session["user"] as Employee)?.EmployeeName;
                bool isSuccess = questionnaireTypeService.UpdateQuestionnaireType(questionnaireType);
                if (isSuccess)
                {
                    msg = "修改成功";
                }
                else
                {
                    msg = "修改失败";
                }
                log.Info(msg);
            }
            catch (DbEntityValidationException e)
            {
                log.Error(e.Message);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { msg = msg }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [Route("Del")]
        public JsonResult DeleteQuestionnaireType(Guid id)
        {
            JsonResult result = new JsonResult();
            try
            {
                bool isSuccess = questionnaireTypeService.DeleteQuestionnaireType(id);
                log.Info("删除成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { msg = "删除成功" }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
    }
}