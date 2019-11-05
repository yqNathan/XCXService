using AutoMapper;
using GDD.Admin.Business.BLL;
using GDD.Admin.Business.IBLL;
using GDD.Admin.VO;
using GDD.Common;
using GDD.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GDD.Admin.Web.Controllers
{
    [RoutePrefix("Button")]
    public class ButtonController : Controller
    {
        IButtonService buttonService;
        private static readonly ILog log = LogManager.GetLogger(typeof(EmployeeController));

        public ButtonController()
        {
            buttonService = new ButtonServer();
        }
        // GET: Button
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("List")]
        public JsonResult GetButtonList(string name, int pageIndex, int pageSize)
        {
            List<SYS_Button> list = null;
            List<ButtonVO> listvo = null;
            JsonResult result = new JsonResult();
            int count = 0;
            string msg = "";
            try
            {
                list = buttonService.GetButtonList(name, pageIndex, pageSize);
                count = buttonService.GetButtonCount(name);
                listvo = Mapper.Map<List<ButtonVO>>(list);
                msg = "success";
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            finally
            {
                result = Json(new { code = 0, msg = msg, count = count, data = listvo }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }


        [HttpPost]
        [Route("Add")]
        public JsonResult InsertEmployee(SYS_Button btn)
        {
            JsonResult result = new JsonResult();
            try
            {
                btn.ButtonID = Guid.NewGuid();
                btn.IsDel = 0;
                btn.CreateTime = DateTime.Now;
                btn.Creator = (Session["user"] as SYS_User)?.UserName;
                bool isSuccess = buttonService.InsertButton(btn);
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
        public JsonResult UpdateButton(SYS_Button button)
        {
            JsonResult result = new JsonResult();
            string msg = "";
            try
            {
                button.ModifiedTime = DateTime.Now;
                button.Modifier = (Session["user"] as SYS_User)?.UserName;
                bool isSuccess = buttonService.UpdateButton(button);
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
        public JsonResult DeleteButton(Guid id)
        {
            JsonResult result = new JsonResult();
            try
            {
                bool isSuccess = buttonService.DeleteButton(id);
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


        [HttpGet]
        [Route("MenuButtonHtml")]
        public MvcHtmlString GetMenuButtonHtml(string menuCode)
        {
            List<CheckBoxButtonVO> buttons = buttonService.GetMenuButtonList(menuCode);
            StringBuilder html = new StringBuilder();
            var checkedstr = "";
            foreach (var item in buttons) 
            {
                checkedstr = "";
                if (item.State == 0)
                {
                    checkedstr = "checked=''";
                }
                html.AppendLine("<input type='checkbox' name='btn["+ item.Code + "]' title='"+ item.Name +"' value='"+ item.Code + "' "+ checkedstr + ">");
            }
            return new MvcHtmlString(html.ToString());
        }

        /// <summary>
        /// 创建按钮HTML
        /// </summary>
        /// <param name="type">按钮类型</param>
        /// <param name="menu">菜单代码</param>
        /// <returns></returns>
        public MvcHtmlString CreateButtonHtml(int type,string menu)
        {
            List<SYS_Button> list = buttonService.GetButtonListByRoleMenu(menu, (Session["user"] as SYS_User)?.RoleCode);
            var buttons = list.Where(p => p.BtnType == type).ToList();
            int count = buttons.Count;
            List<string> colorList = new List<string>() { "layui-btn-primary",  " layui-btn-danger", "layui-bg-green",  "layui-bg-cyan", "layui-bg-orange", "layui-bg-red", "layui-bg-gray",  "layui-bg-black" };
            StringBuilder html = new StringBuilder();
            if (type == Convert.ToInt32(BtnType.表格外))
            {
                html.AppendLine("<div class='layui-inline'>");
                int num = 0;
                for (int i = 0; i < count; i++)
                {
                    num++;
                    if (num == 2)
                    {
                        html.AppendLine("</div>");
                        html.AppendLine("<div class='layui-inline'>");
                        num = 0;
                    }
                    var btn = buttons[i];
                    string dataType = "";
                    string innerHtml = ButtonInnerHtml(btn);
                    if (btn.BtnCode == "Query")
                    {
                        dataType = "data-type='reload'";
                    }
                    html.AppendLine("<button class='layui-btn topBtnFontSzie' " + dataType + " title='"+ btn.BtnDescribe +"' type='button' onclick='" + btn.BtnCode + "();' >" + innerHtml + "</button>");
                }
                html.AppendLine("</div>");
            }
            else if (type == Convert.ToInt32(BtnType.表格头部))
            {

            }
            else if (type == Convert.ToInt32(BtnType.表格操作列))
            {
                for (int i = 0; i < count; i++)
                {
                    var btn = buttons[i];
                    string innerHtml = ButtonInnerHtml(btn);
                    html.AppendLine("<a class='layui-btn layui-btn-xs " + colorList[i] + " _"+ btn.BtnCode + "' lay-event='"+ btn.BtnCode +"'>" + innerHtml + "</a>");
                }
            }
            return new MvcHtmlString(html.ToString());
        }

        /// <summary>
        /// 按钮内容HTML
        /// </summary>
        /// <param name="btn">按钮对象</param>
        /// <returns></returns>
        public string ButtonInnerHtml(SYS_Button btn)
        {
            string icon = "<i class='layui-icon " + btn.BtnIcon + "'></i>";
            string text = btn.BtnName;
            string innerHtml = "按钮";
            var showType = (BtnShowType)btn.ShowType;
            switch (showType)
            {
                case BtnShowType.图标加文字:
                    innerHtml = icon + text;
                    break;
                case BtnShowType.纯图标:
                    innerHtml = icon;
                    break;
                case BtnShowType.纯文字:
                    innerHtml = text;
                    break;
                default:
                    break;
            }
            return innerHtml;
        }
    }
}