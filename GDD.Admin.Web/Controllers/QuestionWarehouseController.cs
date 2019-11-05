using AutoMapper;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
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
    [RoutePrefix("QuestionWarehouse")]
    public class QuestionWarehouseController : Controller
    {
        IQuestionWarehouseService questionWarehouseService;
        private static readonly ILog log = LogManager.GetLogger(typeof(QuestionnaireTypeController));

        public QuestionWarehouseController()
        {
            questionWarehouseService = new QuestionWarehouseServer();
        }

        // GET: QuestionWarehouse
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuestionWarehouseDetail(string obj)
        {
            QuestionWarehouseVO vo = new QuestionWarehouseVO();
            if (!string.IsNullOrEmpty(obj))
            {
                vo = JsonConvert.DeserializeObject<QuestionWarehouseVO>(obj);
                ViewData["QuestionWarehouseData"] = vo;
            }
            else
            {
                vo.QuestionWarehouseID = "";
                ViewData["QuestionWarehouseData"] = vo;
            }
            return View();
        }

        [HttpGet]
        [Route("GetQuestionWarehouseID")]
        public JsonResult GetQuestionWarehouseIDByQuestionnaireID( Guid questionnaireId)
        {
            JsonResult result = new JsonResult();
            string id = "";
            try
            {
                id = questionWarehouseService.GetQuestionWarehouseIDByQuestionnaireID(questionnaireId);
                log.Info("查询成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { code = 0, msg = "获取问卷类型",  data = id }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpGet]
        [Route("List")]
        public JsonResult GetQuestionWarehouseList(string name, Guid questionnaireTypeId, int pageIndex, int pageSize)
        {
            List<QuestionWarehouse> list = null;
            List<QuestionWarehouseVO> listvo = null;
            JsonResult result = new JsonResult();
            int count = 0;
            try
            {
                list = questionWarehouseService.GetQuestionWarehouseList(name, questionnaireTypeId, pageIndex, pageSize);
                count = questionWarehouseService.GetQuestionWarehouseCount(name, questionnaireTypeId);
                listvo = Mapper.Map<List<QuestionWarehouseVO>>(list);
                log.Info("查询成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { code = 0, msg = "获取问卷类型", count = count, data = listvo }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpGet]
        [Route("ALLList")]
        public JsonResult GetALLQuestionWarehouseList(Guid questionnaireTypeId)
        {
            List<QuestionWarehouse> list = null;
            List<QuestionWarehouseVO> listvo = null;
            JsonResult result = new JsonResult();
            int count = 0;
            try
            {
                list = questionWarehouseService.GetQuestionWarehouseList("", questionnaireTypeId, 1, int.MaxValue);
                count = questionWarehouseService.GetQuestionWarehouseCount("", questionnaireTypeId);
                listvo = Mapper.Map<List<QuestionWarehouseVO>>(list);
                log.Info("查询成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { code = 0, msg = "获取当前站点信息",  data = listvo }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [Route("Add")]
        public JsonResult InsertQuestionnaireType(QuestionWarehouse questionWarehouse)
        {
            JsonResult result = new JsonResult();
            try
            {
                questionWarehouse.QuestionWarehouseID = Guid.NewGuid();
                questionWarehouse.CreateTime = DateTime.Now;
                questionWarehouse.Creator = (Session["user"] as SYS_User)?.UserName;
                bool isSuccess = questionWarehouseService.InsertQuestionWarehouse(questionWarehouse);
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
        public JsonResult UpdateQuestionnaireType(QuestionWarehouse questionWarehouse)
        {
            JsonResult result = new JsonResult();
            string msg = "";
            try
            {
                questionWarehouse.ModifiedTime = DateTime.Now;
                questionWarehouse.Modifier = (Session["user"] as SYS_User)?.UserName;
                bool isSuccess = questionWarehouseService.UpdateQuestionWarehouse(questionWarehouse);
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
                bool isSuccess = questionWarehouseService.DeleteQuestionWarehouse(id);
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