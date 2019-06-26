using AutoMapper;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.VO;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDD.Admin.Web.Controllers
{
    [RoutePrefix("Chart")]
    public class ChartController : Controller
    {
        IEmployeeService employeeService;
        IQuestionnaireService questionnaireService;
        IOptionService optionService;
        IQuestionService questionService;
        private static readonly ILog log = LogManager.GetLogger(typeof(QuestionnaireMGTController));

        public ChartController()
        {
            employeeService = new EmployeeServer();
            questionnaireService = new QuestionnaireServer();
            optionService = new OptionServer();
            questionService = new QuestionServer();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SubmissionList()
        {
            return View();
        }

        public ActionResult ScoreChart()
        {
            return View();
        }

        [Route("SubmittedCount")]
        public JsonResult GetSubmittedCountByQuestionnaireId(string name,Guid? questionnaireId, Guid? departmentId,int isSubmit)
        {
            JsonResult result = new JsonResult();
            int submittedCount = 0;
            int employeeCount = 0;
            try
            {
                submittedCount = questionnaireService.GetSubmittedCountByQuestionnaireId(name, questionnaireId, departmentId, isSubmit);
                if (isSubmit == -1)
                {
                    employeeCount = employeeService.GetEmployeeCount(name, departmentId);
                }
                else if (isSubmit == 1)
                {
                    employeeCount = submittedCount;
                }
                else if (isSubmit == 0)
                {
                    employeeCount = submittedCount;
                    submittedCount = 0;
                }
                log.Info("查询成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { code = 0, msg = "获取提交人数成功", submittedCount = submittedCount, employeeCount = employeeCount }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        /// <summary>
        /// 获取题目分数列表
        /// </summary>
        /// <param name="questionName"></param>
        /// <param name="departmentId"></param>
        /// <param name="functionalGroupID"></param>
        /// <param name="questionnaireId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ScoreChartList")]
        public JsonResult GetScoreChartList(string questionName, Guid? departmentId, Guid? functionalGroupID, Guid? questionnaireId, int pageIndex, int pageSize)
        {
            ScoreCharResult scoreCharResult = new ScoreCharResult();
            List<ScoreChart> list = null;
            List<ScoreChartVO> listvo = null;
            List<QuestionChart> qlist = null;
            List<string> olist = null;
            JsonResult result = new JsonResult();
            int count = 0;
            try
            {
                scoreCharResult = optionService.GetScoreChartList(questionName, departmentId, functionalGroupID, questionnaireId, pageIndex, pageSize);
                list = scoreCharResult.ScoreChartList;
                olist = scoreCharResult.OptionNumberList;
                qlist = scoreCharResult.QuestionChartList;
                count = optionService.GetScoreChartCount(questionName, departmentId, functionalGroupID, questionnaireId);
                listvo = Mapper.Map<List<ScoreChartVO>>(list);
                log.Info("查询成功");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                result = Json(new { code = 0, msg = "查询成功", count = count, data = listvo, qlist = qlist ,olist = olist }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

    }
}