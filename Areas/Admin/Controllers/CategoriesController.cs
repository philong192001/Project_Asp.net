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
using System.Security.Cryptography;
using System.Text;

namespace Add_TemplateAdmin.Areas.Admin.Controllers
{
    //kiem tra dang nhap
    [CheckLogin]
    public class CategoriesController : Controller
    {
        //khai bao doi tuong thao csdl
        private readonly DataContext db = new DataContext();
        // GET: Admin/Categories
        public ActionResult Index()
        {
            //di chuyen den url Admin/Categories/Read
            return RedirectToAction("Read", "Categories");
        }
        public ActionResult Read(int? page)
        {
            //int? page -> neu page co gia tri thi gan gia tri do vao bien page
            //khai bao so ban ghi tren mot trang
            int pageSize = 5;
            //page ?? 1 -> neu bien page = null thi gan gia tri 1, con khong thi lay gia tri
            int pageNumber = page ?? 1;
            //var listRecord = (from tbl in db.Categories orderby tbl.id descending select tbl);
            var listRecord = db.Categories.Where(tbl => tbl.parent_id == 0).OrderByDescending(tbl => tbl.id).ToList();
            //goi view
            return View("Read", listRecord.ToPagedList(pageNumber, pageSize));
        }
        //update - GET
        public ActionResult Update(int id)
        {
            //tao action de khi an sumit trang se submit se den
            ViewBag.action = "/Admin/Categories/UpdatePost/" + id;
            //lay 1 ban ghi tuong ung voi id truyen vao
            var record = db.Categories.Where(tbl => tbl.id == id).FirstOrDefault();
            return View("Form", record);
        }
        //update - khi an nut submit
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UpdatePost(FormCollection fc, int id)
        {
            //lay name
            string _name = fc["name"].Trim();
            string _parent_id = fc["parent_id"].Trim();
            int _displayhome = fc["displayhome"] != null ? 1 : 0;
            //lay ban ghi tuong ung id truyen vao
            var record = db.Categories.Where(tbl => tbl.id == id).FirstOrDefault();
            if (record != null)
            {
                record.name = _name;
                record.parent_id = Convert.ToInt32(_parent_id);
                record.displayhome = _displayhome;
                //cap nhat ban ghi
                db.SaveChanges();
            }
            return RedirectToAction("Read", "Categories");
        }
        //Create - GET
        public ActionResult Create()
        {
            //tao action de khi an sumit trang se submit se den
            ViewBag.action = "/Admin/Categories/CreatePost";
            return View("Form");
        }
        //Create - POST
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreatePost(FormCollection fc)
        {
            //lay name
            string _name = fc["name"].Trim();
            string _parent_id = fc["parent_id"].Trim();
            int _displayhome = fc["displayhome"] != null ? 1 : 0;
            //tao mot object <=> mot ban ghi
            var record = new Categories();
            record.name = _name;
            record.parent_id = Convert.ToInt32(_parent_id);
            record.displayhome = _displayhome;
            //them ban ghi
            db.Categories.Add(record);
            db.SaveChanges();
            //---
            return RedirectToAction("Read", "Categories");
        }
        //xoa ban ghi
        public ActionResult Delete(int id)
        {
            //lay cac ban ghi cap con de xoa
            var subRecord = db.Categories.Where(tbl => tbl.parent_id == id).ToList();
            foreach (var itemSub in subRecord)
            {
                //xoa ban ghi
                db.Categories.Remove(itemSub);
            }
            //---
            //lay 1 ban ghi tuong ung voi id truyen vao
            var record = db.Categories.Where(tbl => tbl.id == id).FirstOrDefault();
            //xoa ban ghi
            db.Categories.Remove(record);
            db.SaveChanges();
            //RedirectToAction("Read", "Categories") <=> Admin/Categories/Read
            return RedirectToAction("Read", "Categories");
        }
    }
}