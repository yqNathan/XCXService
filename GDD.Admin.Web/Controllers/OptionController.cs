using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.Web.Filter;
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
    
    [RoutePrefix("Option")]
    public class OptionController : Controller
    {
        IOptionService optionService;
        private static readonly ILog log = LogManager.GetLogger(typeof(OptionController));
        public OptionController()
        {
            optionService = new OptionServer();
        }

        // GET: Option
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OptionDetail(string obj)
        {
            Option vo = new Option();
            if (!string.IsNullOrEmpty(obj))
            {
                vo = JsonConvert.DeserializeObject<Option>(obj);
                ViewData["OptionData"] = vo;
            }
            else
            {
                vo.OptionID = Guid.Empty;
                ViewData["OptionData"] = vo;
            }
            return View();
        }

        [HttpGet]
        [Route("List")]
        public JsonResult GetOptionList(string name, int pageIndex, int pageSize)
        {
            List<Option> list = null;
            JsonResult result = new JsonResult();
            int count = 0;
            try
            {
                list = optionService.GetOptionList(name, pageIndex, pageSize);
                count = optionService.GetOptionCount(name);
                log.Info("查询成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { code = 0, msg = "查询成功", count = count, data = list }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpGet]
        [Route("TypeList")]
        public JsonResult GetOptionTypeList()
        {
            List<OptionType> list = null;
            JsonResult result = new JsonResult();
            try
            {
                list = optionService.GetOptionTypeList();
                log.Info("查询成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { code = 0, msg = "获取选项类型集合", data = list }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [Route("Add")]
        public JsonResult InsertOption(Option option)
        {
            JsonResult result = new JsonResult();
            try
            {
                option.OptionID = Guid.NewGuid();
                option.CreateTime = DateTime.Now;
                option.Creator = (Session["user"] as Employee)?.EmployeeName;
                bool isSuccess = optionService.InsertOption(option);
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
        public JsonResult UpdateOption(Option option)
        {
            JsonResult result = new JsonResult();
            string msg = "";
            try
            {
                option.ModifiedTime = DateTime.Now;
                option.Modifier = (Session["user"] as Employee)?.EmployeeName;
                bool isSuccess = optionService.UpdateOption(option);
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
        public JsonResult DeleteOption(Guid id)
        {
            JsonResult result = new JsonResult();
            try
            {
                bool isSuccess = optionService.DeleteOption(id);
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