using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Add_TemplateAdmin.Models;
namespace Add_TemplateAdmin.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        public DataContext db = new DataContext();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        //khi an nut submit thi trang thai cua trang la POST phai co cac thuoc tinh sau :
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DoLogin()
        {
            string _email = Request.Form["email"];
            string _password = Request.Form["password"];
            //ma hoa password
            if (!string.IsNullOrEmpty(_password))
            {
                //ma hoa password
                _password = ComputeSha256Hash(_password);
                //lay mot ban ghi
                var record = (from tbl in db.Users where tbl.email == _email && tbl.password == _password select tbl).FirstOrDefault();
                if (record != null)
                {
                    this.Session.Add("email", _email);
                    //di chuyen den Url /Admin/Home
                    //return RedirectToAction("Index", "Home");                    
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Index");
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
        //dang xuat
        public ActionResult Logout()
        {
            //huy tat ca cac session
            Session.Abandon();
            //di chuyen den view login
            //return View("Index");
            return RedirectToAction("Index", "Login");
        }
    }
}