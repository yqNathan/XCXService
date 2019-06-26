using AutoMapper;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.DTO;
using GDD.Admin.VO;
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
    [RoutePrefix("Question")]
    public class QuestionController : Controller
    {
        IQuestionService questionService;
        private static readonly ILog log = LogManager.GetLogger(typeof(QuestionController));
        public QuestionController()
        {
            questionService = new QuestionServer();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuestionDetail(string obj)
        {
            QuestionVO vo = new QuestionVO();
            if (!string.IsNullOrEmpty(obj))
            {
                vo = JsonConvert.DeserializeObject<QuestionVO>(obj);
                ViewData["QuestionData"] = vo;
            }
            else
            {
                vo.QuestionID = "";
                vo.Options = new List<Option>();
                ViewData["QuestionData"] = vo;
            }
            return View();
        }

        [HttpGet]
        [Route("List")]
        public JsonResult GetQuestionList(string title, int pageIndex, int pageSize)
        {
            List<Question> list = null;
            List<QuestionVO> listvo = null;
            JsonResult result = new JsonResult();
            int count = 0;
            try
            {
                list = questionService.GetQuestionList(title, pageIndex, pageSize);
                count = questionService.GetQuestionCount(Guid.Empty,title);
                listvo = Mapper.Map<List<QuestionVO>>(list);
                log.Info("查询成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { code = 0, msg = "获取题目集合", count = count, data = listvo }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [Route("Add")]
        public JsonResult InsertQuestion(QuestionDTO questionDTO)
        {
            JsonResult result = new JsonResult();
            try
            {
                bool isSuccess = questionService.InsertQuestion(questionDTO);
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
        public JsonResult UpdateQuestion(QuestionDTO questionDTO)
        {
            JsonResult result = new JsonResult();
            string msg = "";
            try
            {
                bool isSuccess = questionService.UpdateQuestion(questionDTO);
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
                msg = "修改失败";
                log.Error(e.Message);
            }
            catch (Exception e)
            {
                msg = "修改失败";
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
        public JsonResult DeleteQuestion(Guid id)
        {
            JsonResult result = new JsonResult();
            try
            {
                bool isSuccess = questionService.DeleteQuestion(id);
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