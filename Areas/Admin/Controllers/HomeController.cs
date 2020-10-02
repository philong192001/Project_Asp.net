using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Add_TemplateAdmin.Areas.Admin.Attribute;
namespace Add_TemplateAdmin.Areas.Admin.Controllers
{
    //goi Attribute CheckLogin de kiem tra dang nhap
    [CheckLogin]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}