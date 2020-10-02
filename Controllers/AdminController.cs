using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Add_TemplateAdmin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            //di chuyen den Area Admin co duong dan la /Admin/Login/Index
            return RedirectToAction("Index", "Login", new { area = "Admin" });
        }
    }
}