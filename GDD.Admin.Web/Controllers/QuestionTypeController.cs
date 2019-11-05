using AutoMapper;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.VO;
using GDD.Common;
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
    [RoutePrefix("QuestionType")]
    public class QuestionTypeController : Controller
    {
        IQuestionTypeService questionTypeService;
        IButtonService buttonService;
        private static readonly ILog log = LogManager.GetLogger(typeof(QuestionTypeController));
        public QuestionTypeController()
        {
            questionTypeService = new QuestionTypeServer();
            buttonService = new ButtonServer();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuestionTypeDetail(string obj)
        {
            QuestionTypeVO vo = new QuestionTypeVO();
            if (!string.IsNullOrEmpty(obj))
            {
                vo = JsonConvert.DeserializeObject<QuestionTypeVO>(obj);
                ViewData["QuestionTypeData"] = vo;
            }
            else
            {
                vo.QuestionTypeID = "";
                ViewData["QuestionTypeData"] = vo;
            }
            return View();
        }

        [HttpGet]
        [Route("List")]
        public JsonResult GetQuestionTypeList(string typeName,Guid questionnaireTypeID, int pageIndex, int pageSize)
        {
            List<QuestionType> list = null;
            List<QuestionTypeVO> listvo = null;
            JsonResult result = new JsonResult();
            int count = 0;
            try
            {
                list = questionTypeService.GetQuestionTypeList(typeName, questionnaireTypeID, pageIndex, pageSize);
                count = questionTypeService.GetQuestionTypeCount(typeName, questionnaireTypeID);
                listvo = Mapper.Map<List<QuestionTypeVO>>(list);
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

        [HttpGet]
        [Route("ALLList")]
        public JsonResult GetALLQuestionTypeList(string typeName, Guid questionnaireTypeID)
        {
            List<QuestionType> list = null;
            List<QuestionTypeVO> listvo = null;
            JsonResult result = new JsonResult();
            int count = 0;
            try
            {
                list = questionTypeService.GetQuestionTypeList(typeName, questionnaireTypeID, 1, int.MaxValue);
                count = questionTypeService.GetQuestionTypeCount(typeName, questionnaireTypeID);
                listvo = Mapper.Map<List<QuestionTypeVO>>(list);
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
        public JsonResult InsertQuestionType(QuestionType questionType)
        {
            JsonResult result = new JsonResult();
            try
            {
                questionType.QuestionTypeID = Guid.NewGuid();
                questionType.CreateTime = DateTime.Now;
                questionType.Creator = (Session["user"] as SYS_User)?.UserName;
                bool isSuccess = questionTypeService.InsertQuestionType(questionType);
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
        public JsonResult UpdateQuestionType(QuestionType questionType)
        {
            JsonResult result = new JsonResult();
            string msg = "";
            try
            {
                questionType.ModifiedTime = DateTime.Now;
                questionType.Modifier = (Session["user"] as SYS_User)?.UserName;
                bool isSuccess = questionTypeService.UpdateQuestionType(questionType);
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
        public JsonResult DeleteQuestionType(Guid id)
        {
            JsonResult result = new JsonResult();
            try
            {
                bool isSuccess = questionTypeService.DeleteQuestionType(id);
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