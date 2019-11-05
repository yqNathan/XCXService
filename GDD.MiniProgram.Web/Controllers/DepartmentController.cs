using GDD.MiniProgram.Business.BLL;
using GDD.MiniProgram.Business.IBLL;
using GDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDD.MiniProgram.Web.Controllers
{
    [RoutePrefix("Department")]
    public class DepartmentController : Controller
    {
        IDepartmentService departmentService;
        IFunctionalGroupService functionalgroupService;
        public DepartmentController()
        {
            departmentService = new DepartmentServer();
            functionalgroupService = new FunctionalGroupServer();
        }

        // GET: Department
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("List")]
        public JsonResult GetDepartmentList()
        {
            List<Department> list = null;
            JsonResult result = new JsonResult();
            try
            {
                list = departmentService.GetDepartmentList();
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
        [Route("GetFunctionalGroup")]
        public JsonResult GetFunctionalgroupListById(Guid departmentId)
        {
            List<FunctionalGroup> list = null;
            JsonResult result = new JsonResult();
            try
            {
                list = functionalgroupService.GetFunctionalGroupByDepartmentId(departmentId);
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

    }
}