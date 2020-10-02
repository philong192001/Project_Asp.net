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
//thao tac voi file
using System.IO;

namespace Add_TemplateAdmin.Areas.Admin.Controllers
{
    //kiem tra dang nhap
    [CheckLogin]
    public class ProductsController : Controller
    {
        //khai bao doi tuong thao csdl
        private readonly DataContext db = new DataContext();
        // GET: Admin/Products
        public ActionResult Index()
        {
            //di chuyen den url Admin/Products/Read
            return RedirectToAction("Read", "Products");
        }
        public ActionResult Read(int? page)
        {
            //int? page -> neu page co gia tri thi gan gia tri do vao bien page
            //khai bao so ban ghi tren mot trang
            int pageSize = 10;
            //page ?? 1 -> neu bien page = null thi gan gia tri 1, con khong thi lay gia tri
            int pageNumber = page ?? 1;
            //var listRecord = (from tbl in db.Products orderby tbl.id descending select tbl);
            var listRecord = db.Products.OrderByDescending(tbl => tbl.id).ToList();
            //goi view
            return View("Read", listRecord.ToPagedList(pageNumber, pageSize));
        }
        //update - GET
        public ActionResult Update(int id)
        {
            //tao action de khi an sumit trang se submit se den
            ViewBag.action = "/Admin/Products/UpdatePost/" + id;
            //lay 1 ban ghi tuong ung voi id truyen vao
            var record = db.Products.Where(tbl => tbl.id == id).FirstOrDefault();
            return View("Form", record);
        }
        //update - khi an nut submit
        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult UpdatePost(FormCollection fc, int id)
        {
            //lay name
            string _name = fc["name"].Trim();
            string _price = fc["price"];
            string _category_id = fc["category_id"];
            string _discount = fc["discount"];
            string _content = fc["content"];
            string _description = fc["description"];
            int _hot = fc["hot"] != null ? 1 : 0;
            //lay ban ghi tuong ung id truyen vao
            var record = db.Products.Where(tbl => tbl.id == id).FirstOrDefault();
            if (record != null)
            {
                record.name = _name;
                record.price = Convert.ToDouble(_price);
                record.category_id = Convert.ToInt32(_category_id);
                record.discount = Convert.ToInt32(_discount);
                record.content = _content;
                record.hot = _hot;
                record.description = _description;
                //---
                //neu user chon anh de upload
                if (Request.Files["photo"].ContentLength > 0)
                {
                    //lay anh cu de xoa truoc khi update anh moi
                    //.Select(tbl=>new { tbl.photo }) chi lay field photo
                    var _oldRecord = db.Products.Where(tbl => tbl.id == id).FirstOrDefault();
                    if (_oldRecord != null && !String.IsNullOrEmpty(_oldRecord.photo))
                    {
                        string _oldPath = Path.Combine(Server.MapPath("~/Assets/Upload/Products"), _oldRecord.photo);
                        //thuc hien xoa file cu
                        System.IO.File.Delete(_oldPath);
                    }
                    //thuc hien upload file
                    string path = Path.Combine(Server.MapPath("~/Assets/Upload/Products"), Path.GetFileName(Request.Files["photo"].FileName));
                    Request.Files["photo"].SaveAs(path);
                    record.photo = Request.Files["photo"].FileName;
                }
                //---
                //cap nhat ban ghi
                db.SaveChanges();
            }
            return RedirectToAction("Read", "Products");
        }
        //Create - GET
        public ActionResult Create()
        {
            //tao action de khi an sumit trang se submit se den
            ViewBag.action = "/Admin/Products/CreatePost";
            return View("Form");
        }
        //Create - POST
        //ValidateInput(false) -> do dung ckeditor truyen du lieu nen phai
        //co tag nay
        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult CreatePost(FormCollection fc)
        {
            //lay name
            string _name = fc["name"].Trim();
            string _price = fc["price"];
            string _discount = fc["discount"];
            int _category_id = Convert.ToInt32(fc["category_id"]);
            string _description = fc["description"];
            string _content = fc["content"];
            int _hot = fc["hot"] != null ? 1 : 0;
            //tao mot object <=> mot ban ghi
            var record = new Products();
            record.name = _name;
            record.price = Convert.ToDouble(_price);
            record.category_id = Convert.ToInt32(_category_id);
            record.description = _description;
            record.content = _content;
            record.hot = _hot;
            record.discount = Convert.ToInt32(_discount);
            //---
            //neu user chon anh de upload
            if (Request.Files["photo"].ContentLength > 0)
            {
                //thuc hien upload file
                string path = Path.Combine(Server.MapPath("~/Assets/Upload/Products"), Path.GetFileName(Request.Files["photo"].FileName));
                Request.Files["photo"].SaveAs(path);
                record.photo = Request.Files["photo"].FileName;
            }
            //---
            //them ban ghi
            db.Products.Add(record);
            db.SaveChanges();
            //---
            return RedirectToAction("Read", "Products");
        }
        //xoa ban ghi
        public ActionResult Delete(int id)
        {
            //lay anh cu de xoa truoc khi update anh moi
            //.Select(tbl=>new { tbl.photo }) chi lay field photo
            var _oldRecord = db.Products.Where(tbl => tbl.id == id).Select(tbl => new { tbl.photo }).FirstOrDefault();
            if (_oldRecord != null && !String.IsNullOrEmpty(_oldRecord.photo))
            {
                string _oldPath = Path.Combine(Server.MapPath("~/Assets/Upload/Products"), _oldRecord.photo);
                //thuc hien xoa file cu
                System.IO.File.Delete(_oldPath);
            }
            //lay 1 ban ghi tuong ung voi id truyen vao
            var record = db.Products.Where(tbl => tbl.id == id).FirstOrDefault();
            //xoa ban ghi
            db.Products.Remove(record);
            db.SaveChanges();
            //RedirectToAction("Read", "Products") <=> Admin/Products/Read
            return RedirectToAction("Read", "Products");
        }
    }
}