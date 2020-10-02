using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//load folder Models -> de nhin thay cac class bent trong folder nay
using Add_TemplateAdmin.Models;
//load folder Attributes
using Add_TemplateAdmin.Areas.Admin.Attribute;
//load thu vien phan trang
using PagedList;
using System.Data.Entity;

namespace Add_TemplateAdmin.Areas.Admin.Controllers
{
    //kiem tra dang nhap
    [CheckLogin]
    public class OrdersController : Controller
    {
        //khai bao doi tuong thao csdl
        private readonly DataContext db = new DataContext();
        // GET: Admin/Orders
        public ActionResult Index()
        {
            //di chuyen den url Admin/Users/Read
            return RedirectToAction("Read", "Orders");
        }
        public ActionResult Read(int? page)
        {
            //int? page -> neu page co gia tri thi gan gia tri do vao bien page
            //khai bao so ban ghi tren mot trang
            int pageSize = 3;
            //page ?? 1 -> neu bien page = null thi gan gia tri 1, con khong thi lay gia tri
            int pageNumber = page ?? 1;
            //var listRecord = (from tbl in db.Users orderby tbl.id descending select tbl);
            var listRecord = db.Orders.OrderByDescending(tbl => tbl.id).ToList();
            //goi view
            return View("Read", listRecord.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Detail(int? id)
        {
            ViewBag.order_id = id;
            var listRecord = db.OrderDetail.Where(tbl => tbl.order_id == id).ToList();
            //goi view
            return View("Detail", listRecord);
        }
        //Delivery
        public ActionResult Delivery(int? id)
        {
            var record = db.Orders.Where(tbl => tbl.id == id).FirstOrDefault();
            record.status = 1;
            db.SaveChanges();
            return RedirectToAction("Read", "Orders");
        }
    }
}