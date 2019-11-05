using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDD.Admin.Web.Controllers
{
    public class AuthorityController : Controller
    {
        // GET: Authority
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuList()
        {
            return View();
        }

        public ActionResult BtnList()
        {
            return View();
        }

        public ActionResult RoleList()
        {
            return View();
        }
    }
}