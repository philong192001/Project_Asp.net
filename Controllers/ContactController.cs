using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Add_TemplateAdmin.Models;
using Add_TemplateAdmin.Models.General;
using System.Data;
namespace Add_TemplateAdmin.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}