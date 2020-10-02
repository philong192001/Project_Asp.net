using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Add_TemplateAdmin.Areas.Admin.Attribute
{
    public class CheckLogin : ActionFilterAttribute
    {
        // GET: Admin/CheckLogin
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //kiem tra dang nhap
            if (HttpContext.Current.Session["email"] == null)
                HttpContext.Current.Response.Redirect("/Admin/Login");
            base.OnActionExecuting(filterContext);
        }      
    }
}