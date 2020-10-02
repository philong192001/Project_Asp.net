using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Add_TemplateAdmin.Models;
using System.Security.Cryptography;
using System.Text;
using Add_TemplateAdmin.Models.General;

namespace Add_TemplateAdmin.Controllers
{
    public class AccountController : Controller
    {
        //tao doi tuong thao tac csdl (-> khoi tao object cua class)
        public DataContext db = new DataContext();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        //dang ky
        public ActionResult Register()
        {
            return View("Register");
        }
        //dangky - khi an nut submit thi vao day
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult RegisterPost()
        {
            string _name = Request.Form["name"].Trim();
            string _email = Request.Form["email"].Trim();
            string _address = Request.Form["address"].Trim();
            string _phone = Request.Form["phone"].Trim();
            string _password = Request.Form["password"].Trim();
            //password khong duoc rong
            if (!string.IsNullOrEmpty(_password))
            {
                //ma hoa password
                _password = ComputeSha256Hash(_password);
                //---
                //khoi tao ban ghi
                Customers record = new Customers();
                record.name = _name;
                record.email = _email;
                record.address = _address;
                record.phone = _phone;
                record.password = _password;
                //---
                db.Customers.Add(record);
                db.SaveChanges();
                //---
            }
            return RedirectToAction("Register", "Account", new { registered = true });
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
        //dang nhap
        public ActionResult Login()
        {
            return View("Login");
        }
        //khi an nut submit login
        public ActionResult LoginPost()
        {
            string _name = Request.Form["name"];
            string _password = Request.Form["password"];
            //ma hoa password
            if (!String.IsNullOrEmpty(_password))
            {
                _password = ComputeSha256Hash(_password);
                //kiem tra trong csdl
                Customers record = db.Customers.Where(tbl => tbl.name == _name && tbl.password == _password).FirstOrDefault();
                if (record != null)
                {
                    //dang nhap thanh cong
                    //khoi tao session email
                    this.Session["name"] = _name;
                    this.Session["customer_id"] = record.id;
                    return RedirectToAction("Index", "Home");
                }
                else
                    return RedirectToAction("Login", "Account", new { notify = "invalid" });
            }
            //return RedirectToAction("Login", "Account", new { notify = "success" });
            return RedirectToAction("Login", "Account", new { notify = "invalid" });
        }
        //dang xuat
        public ActionResult Logout()
        {
            this.Session["name"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}