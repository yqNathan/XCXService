using GDD.MiniProgram.Business.BLL;
using GDD.MiniProgram.Business.IBLL;
using GDD.MiniProgram.Web.Filter;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDD.MiniProgram.VO;
using GDD.MiniProgram.DTO;

namespace GDD.MiniProgram.Web.Controllers
{
    //[CheckLoginFilterAttribute(IsCheck = false)]
    [RoutePrefix("Questionnaire")]
    public class QuestionnaireController : Controller
    {
        IQuestionnaireService questionnaireService;
        public QuestionnaireController()
        {
            questionnaireService = new QuestionnaireServer();
        }

        [HttpGet]
        [Route("TypeList")]
        public JsonResult GetQuestionnaireTypeList(Guid employeeID)
        {
            List<QuestionnaireVO> list = null;
            JsonResult result = new JsonResult();
            try
            {
                list = questionnaireService.GetQuestionnaireTypeList(employeeID);
            }
            catch (Exception e)
            {
            }
            finally
            {
                result = Json(new { code = 0, msg = "成功", data = list }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpGet]
        [Route("Get")]
        public JsonResult GetQuestionnaireByTypeId(Guid id)
        {
            Questionnaire obj = null;
            JsonResult result = new JsonResult();
            try
            {
                if (id == null && id == Guid.Empty)
                {
                    result = Json(new { code = 1, msg = "问卷类型不存在", data = id }, JsonRequestBehavior.AllowGet);
                }
                obj = questionnaireService.GetQuestionnaireByTypeId(id);
            }
            catch (Exception e)
            {
            }
            finally
            {
                result = Json(new { code = 0, msg = "成功", data = obj }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpGet]
        [Route("GetQuestionList")]
        public JsonResult GetQuestionListById(Guid qTypeID, Guid qID)
        {
            List<QuestionVO> obj = null;
            JsonResult result = new JsonResult();
            try
            {
                if (qTypeID == null && qTypeID == Guid.Empty)
                {
                    return Json(new { code = 1, msg = "问卷类型不存在", data = qTypeID }, JsonRequestBehavior.AllowGet);
                }
                if (qID == null && qID == Guid.Empty)
                {
                    return Json(new { code = 1, msg = "问卷不存在", data = qID }, JsonRequestBehavior.AllowGet);
                }
                obj = questionnaireService.GetQuestionListById(qTypeID, qID);
            }
            catch (Exception e)
            {
            }
            finally
            {
                result = Json(new { code = 0, msg = "成功", data = obj }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [Route("Submit")]
        public JsonResult SubmitQuestionnaire(QuestionnaireDTO dto)
        {
            JsonResult result = new JsonResult();
            var flag = false;
            var message = "";
            var code = 0;
            try
            {
                flag = questionnaireService.SubmitQuestionnaire(dto);
                if (flag)
                {
                    message = "提交成功";
                    code = 0;
                }
                else
                {
                    message = "提交失败";
                    code = 1;
                }
            }
            catch (Exception e)
            {
                message = "提交失败";
                code = 1;
            }
            finally
            {
                result = Json(new { code = code, msg = message}, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
    }
}