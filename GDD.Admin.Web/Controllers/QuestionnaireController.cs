using AutoMapper;
using GDD.Admin.Business;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.VO;
using GDD.Admin.Web.Extensions;
using GDD.Common;
using GDD.Models;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

[assembly: XmlConfigurator(Watch = true)]
namespace GDD.Admin.Web.Controllers
{
    [RoutePrefix("Questionnaire")]
    public class QuestionnaireController : BaseController
    {
        IQuestionnaireService questionnaireService;
        
        private static readonly ILog log = LogManager.GetLogger(typeof(QuestionnaireController));
        public QuestionnaireController()
        {
            questionnaireService = new QuestionnaireServer();
        }
        // GET: Questionnaire
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuestionList()
        {
            return View();
        }

        public ActionResult QuestionnaireDetail(string obj)
        {
            QuestionnaireVO vo = new QuestionnaireVO();
            if (!string.IsNullOrEmpty(obj))
            {
                vo = JsonConvert.DeserializeObject<QuestionnaireVO>(obj);
                ViewData["QuestionnaireData"] = vo;
            }
            else
            {
                vo.QuestionnaireID = "";
                ViewData["QuestionnaireData"] = vo;
            }
            return View();
        }

        [HttpGet]
        [Route("List")]
        public JsonResult GetQuestionnaireList(string name, Guid typeId, int pageIndex, int pageSize)
        {
            List<Questionnaire> list = null;
            List<QuestionnaireVO> listvo = null;
            JsonResult result = new JsonResult();
            int count = 0;
            try
            {
                list = questionnaireService.GetQuestionnaireList(name, typeId, pageIndex, pageSize);
                count = questionnaireService.GetQuestionnaireCount(name, typeId);
                listvo = Mapper.Map<List<QuestionnaireVO>>(list);
                log.Info("查询成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { code = 0, msg = "获取当前站点信息", count = count, data = listvo }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [Route("AllList")]
        public JsonResult GetAllQuestionnaireList(string name, Guid typeId)
        {
            List<Questionnaire> list = null;
            List<QuestionnaireVO> listvo = null;
            JsonResult result = new JsonResult();
            int count = 0;
            try
            {
                list = questionnaireService.GetQuestionnaireList(name, typeId, 1, int.MaxValue);
                count = questionnaireService.GetQuestionnaireCount(name, typeId);
                listvo = Mapper.Map<List<QuestionnaireVO>>(list);
                log.Info("查询成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { code = 0, msg = "获取当前站点信息", count = count, data = listvo }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [Route("Add")]
        public JsonResult InsertQuestionnaire(Questionnaire questionnaire)
        {
            JsonResult result = new JsonResult();
            try
            {
                bool isSuccess = questionnaireService.InsertQuestionnaire(questionnaire);
                log.Info("添加成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { msg = "添加成功"}, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [Route("Edit")]
        public JsonResult UpdateQuestionnaire(Questionnaire questionnaire)
        {
            JsonResult result = new JsonResult();
            string msg = "";
            try
            {
                bool isSuccess = questionnaireService.UpdateQuestionnaire(questionnaire);
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
        public JsonResult DeleteQuestionnaire(Guid id)
        {
            JsonResult result = new JsonResult();
            try
            {
                bool isSuccess = questionnaireService.DeleteQuestionnaire(id);
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