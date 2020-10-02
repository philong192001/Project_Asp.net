using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using PagedList;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
//load folder Models -> de nhin thay cac class ben trong folder nay
using Add_TemplateAdmin.Models;
//load folder Attribute 
using Add_TemplateAdmin.Areas.Admin.Attribute;
namespace Add_TemplateAdmin.Areas.Admin.Controllers
{
    //kiem tra dang nhap
    [CheckLogin]
    public class UsersController : Controller
    {
        //khai bao doi tuong thao tac csdl
        private readonly DataContext db = new DataContext();
        // GET: Admin/Users
        public ActionResult Index()
        {
            //di chuyen den url Admin/ Users / Read
            return RedirectToAction("Read", "Users");
        }
        public ActionResult Read(int? page)
        {
            //int ? page -> neu page co gia tri thi gan gia tri do vao bien page
            //khai bao so ban ghi tren 1 trang
            int pageSize = 5;
            //page ?? 1 -> neu bien page = null thi gan gia tri 1, con khong thi lay gia tri
            int pageNumber = page ?? 1;
            //var listRecord = (from tbl in db.Users orderby tbl.id descending select tbl);
            var listRecord = db.Users.OrderByDescending(tbl => tbl.id).ToList();
            //goi view
            return View("Read", listRecord.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Update(int id)
        {
            //lay ban ghi tuong ung voi id truyen vao
            var record = db.Users.Where(tbl => tbl.id == id).FirstOrDefault();
            return View("Form", record);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(FormCollection fc, int id)
        {
            //lay name
            string _name = fc["name"];
            //lay email
            string _email = Request.Form["email"];
            //lay password
            string _password = Request.Form["password"];
            //lay ban ghi tuong ung id truyen vao
            var record = db.Users.Where(tbl => tbl.id == id).FirstOrDefault();
            if (record != null)
            {
                record.name = _name;
                record.email = _email;
                //neu password khong rong thi ma hoa va update password
                // if (!String.IsNullOrEmpty(_password)) <=> 
                if (!String.IsNullOrEmpty(_password))
                {
                    //ma hoa password
                    _password = ComputeSha256Hash(_password);
                    record.password = _password;
                }
                //cap nhat ban ghi
                db.SaveChanges();
            }
            return RedirectToAction("Read", "Users");
        }
        //Add - GET
        public ActionResult Create()
        {

            return View("Form");
        }
        //Create - POST
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection fc)
        {
            //lay name
            string _name = fc["name"];
            //lay email
            string _email = Request.Form["email"];
            //lay password
            string _password = Request.Form["password"];
            //ma hoa password
            _password = ComputeSha256Hash(_password);
            //tao mot obj -> mot ban ghi
            var record = new Users();
            record.name = _name;
            record.email = _email;
            record.password = _password;
            //them ban ghi
            db.Users.Add(record);
            db.SaveChanges();
            //---
            return RedirectToAction("Read", "Users");
        }
        public ActionResult Delete(int id)
        {
            //lay 1 ban ghi tuong ung voi id truyen vao
            var record = db.Users.Where(tbl => tbl.id == id).FirstOrDefault();
            //xoa ban ghi
            db.Users.Remove(record);
            db.SaveChanges();
            //Admin/Users/Read
            return RedirectToAction("Read", "Users");
        }
        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}