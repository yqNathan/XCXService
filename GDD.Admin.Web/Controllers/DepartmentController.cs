using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.Web.Extensions;
using GDD.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDD.Admin.Web.Controllers
{
    [RoutePrefix("Department")]
    public class DepartmentController : BaseController
    {
        IDepartmentService departmentService;
        private static readonly ILog log = LogManager.GetLogger(typeof(QuestionnaireController));

        public DepartmentController()
        {
            departmentService = new DepartmentServer();
        }


        // GET: Department
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("List")]
        public JsonResult GetFunctionalGroupByDepartmentId()
        {
            List<Department> list = null;
            JsonResult result = new JsonResult();
            try
            {
                list = departmentService.GetDepartmentList();
                log.Info("查询成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { code = 0, msg = "查询成功", count = list.Count, data = list }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }
    }
}