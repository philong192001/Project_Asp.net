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
    public class HomeController : Controller
    {
        public DataContext db = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //ajax
        [HttpGet]
        public ActionResult AjaxSearch()
        {
            int _category_id = Request.QueryString["category_id"] != null && !String.IsNullOrEmpty(Request.QueryString["category_id"]) ? Convert.ToInt32(Request.QueryString["category_id"]) : 1;
            int _fromProduct = Request.QueryString["fromProduct"] != null && !String.IsNullOrEmpty(Request.QueryString["fromProduct"]) ? Convert.ToInt32(Request.QueryString["fromProduct"]) : 1;
            int _recordPerPage = Request.QueryString["recordPerPage"] != null && !String.IsNullOrEmpty(Request.QueryString["recordPerPage"]) ? Convert.ToInt32(Request.QueryString["recordPerPage"]) : 1;
            //DataTable dt = Database.ListDataTable("select * from Products where category_id = " + _category_id + " OFFSET " + _fromProduct + " ROWS FETCH NEXT " + _recordPerPage + " ROWS ONLY ");
            DataTable dt = Database.ListDataTable("select * from Products where category_id = " + _category_id + "  order by id desc OFFSET " + _fromProduct + " ROWS FETCH NEXT " + _recordPerPage + " ROWS ONLY ");
            //khi da co du lieu tra ve DataTable -> convert sang kieu du lieu giong voi linq tra ve
            List<Products> listRecord = new List<Products>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    listRecord.Add(new Products() { id = Convert.ToInt32(dr["id"]), name = dr["name"].ToString(), category_id = Convert.ToInt32(dr["category_id"]), discount = Convert.ToInt32(dr["discount"]), price = Convert.ToDouble(dr["price"]), hot = Convert.ToInt32(dr["hot"]), description = dr["description"].ToString(), content = dr["content"].ToString(), photo = dr["photo"].ToString() });
                }
            }
            return View(listRecord);
        }
    }
}