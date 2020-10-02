using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

using Add_TemplateAdmin.Models;
using PagedList;
//su dung DataTable va DataRow phai using doi tuong sau
using System.Data;
using Add_TemplateAdmin.Models.General;

namespace Add_TemplateAdmin.Controllers
{
    public class ProductController : Controller
    {
        //doi tuong thao tac csdl
        public DataContext db = new DataContext();
        // GET: Product
        //danh muc san pham
        public ActionResult Categories()
        {
            int _category_id = Request.QueryString["category_id"] != null && !String.IsNullOrEmpty(Request.QueryString["category_id"]) ? Convert.ToInt32(Request.QueryString["category_id"]) : 1;
            ViewBag.category_id = _category_id;
            //---
            int page = Request.QueryString["page"] != null && !String.IsNullOrEmpty(Request.QueryString["page"]) ? Convert.ToInt32(Request.QueryString["page"]) : 1;
            //int? page -> neu page co gia tri thi gan gia tri do vao bien page
            //khai bao so ban ghi tren mot trang
            int pageSize = 9;
            //page ?? 1 -> neu bien page = null thi gan gia tri 1, con khong thi lay gia tri
            int pageNumber = page;
            //---
            string strWhere = "";
            string groupPrice = Request.QueryString["groupPrice"] != null ? Request.QueryString["groupPrice"].Trim() : "";
            switch (groupPrice)
            {
                case "1tr-5tr":
                    strWhere += " and price >= 1000000 and price <= 5000000 ";
                    break;
                case "5tr-10tr":
                    strWhere += " and price >= 5000000 and price <= 10000000 ";
                    break;
                case "10tr-15tr":
                    strWhere += " and price >= 10000000 and price <= 15000000 ";
                    break;
                case "15tr-20tr":
                    strWhere += " and price >= 15000000 and price <= 20000000 ";
                    break;
                case "20tr-25tr":
                    strWhere += " and price >= 20000000 and price <= 25000000 ";
                    break;
                case "25tr-30tr":
                    strWhere += " and price >= 25000000 and price <= 30000000 ";
                    break;
                case "30tr-40tr":
                    strWhere += " and price >= 30000000 and price <= 40000000 ";
                    break;
                case "40tr-50tr":
                    strWhere += " and price >= 40000000 and price <= 50000000 ";
                    break;
                case "50tr-vv":
                    strWhere += " and price >= 50000000 ";
                    break;
            }
            //---
            string order = Request.QueryString["order"] != null ? Request.QueryString["order"].Trim() : "";
            string strOrder = "";
            switch (order)
            {
                case "priceAsc":
                    strOrder = " order by price asc ";
                    break;
                case "priceDesc":
                    strOrder = " order by price desc ";
                    break;
                case "nameAsc":
                    strOrder = " order by name asc ";
                    break;
                case "nameDesc":
                    strOrder = " order by name desc ";
                    break;
            }
            //---
            //loc bao nhieu ban ghi tren mot trang
            int pageSizeFromUrl = Request.QueryString["pageSize"] != null ? Convert.ToInt32(Request.QueryString["pageSize"].Trim()) : 0;
            if (Convert.ToInt32(pageSizeFromUrl) > 0)
            {
                //ghi den bien pageSize quy dinh de phan trang o ben tren
                pageSize = Convert.ToInt32(pageSizeFromUrl);
            }
            //---
            //var listRecord = (from tbl in db.Products orderby tbl.id descending select tbl);
            //var listRecord = db.Products.Where(tbl=>tbl.category_id == _category_id).OrderByDescending(tbl => tbl.id).ToList();
            DataTable dt = Database.ListDataTable("select * from Products where category_id = " + _category_id + strWhere + strOrder);
            //khi da co du lieu tra ve DataTable -> convert sang kieu du lieu giong voi linq tra ve
            List<Products> listRecord = new List<Products>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    listRecord.Add(new Products() { id = Convert.ToInt32(dr["id"]), name = dr["name"].ToString(), category_id = Convert.ToInt32(dr["category_id"]), discount = Convert.ToInt32(dr["discount"]), price = Convert.ToDouble(dr["price"]), hot = Convert.ToInt32(dr["hot"]), description = dr["description"].ToString(), content = dr["content"].ToString(), photo = dr["photo"].ToString() });
                }
            }
            //goi view
            return View("ProductCategories", listRecord.ToPagedList(pageNumber, pageSize));
            //---
        }
        public ActionResult Search()
        {
            //---
            int page = Request.QueryString["page"] != null && !String.IsNullOrEmpty(Request.QueryString["page"]) ? Convert.ToInt32(Request.QueryString["page"]) : 1;
            //int? page -> neu page co gia tri thi gan gia tri do vao bien page
            //khai bao so ban ghi tren mot trang
            int pageSize = 8;
            //page ?? 1 -> neu bien page = null thi gan gia tri 1, con khong thi lay gia tri
            int pageNumber = page;
            //---
            string strWhere = " ";
            string groupPrice = Request.QueryString["groupPrice"] != null ? Request.QueryString["groupPrice"].Trim() : "";
            
                switch (groupPrice)
                {

                    case "1tr-5tr":
                        strWhere += " and price >= 1000000 and price <= 5000000 ";
                        break;
                    case "5tr-10tr":
                        strWhere += " and price >= 5000000 and price <= 10000000 ";
                        break;
                    case "10tr-15tr":
                        strWhere += " and price >= 10000000 and price <= 15000000 ";
                        break;
                    case "15tr-20tr":
                        strWhere += " and price >= 15000000 and price <= 20000000 ";
                        break;
                    case "20tr-25tr":
                        strWhere += " and price >= 20000000 and price <= 25000000 ";
                        break;
                    case "25tr-30tr":
                        strWhere += " and price >= 25000000 and price <= 30000000 ";
                        break;
                    case "30tr-40tr":
                        strWhere += " and price >= 30000000 and price <= 40000000 ";
                        break;
                    case "40tr-50tr":
                        strWhere += " and price >= 40000000 and price <= 50000000 ";
                        break;
                    case "50tr-vv":
                        strWhere += " and price >= 50000000 ";
                        break;
                default:
                    strWhere += " ";
                    break;

            }         
            
            //---
            int fromPrice = Request.QueryString["fromPrice"] != null ? Convert.ToInt32(Request.QueryString["fromPrice"].Trim()) : 0;
            int toPrice = Request.QueryString["toPrice"] != null ? Convert.ToInt32(Request.QueryString["toPrice"].Trim()) : 0;
            if (fromPrice > 0 && toPrice > 0)
            {
                strWhere += " and price between " + fromPrice + " and " + toPrice + " ";
            }
            //---
            string _key = Request.QueryString["key"] != null ? Request.QueryString["key"].Trim() : "";
            if (!String.IsNullOrEmpty(_key))
            {
                strWhere += " and name like N'%" + _key + "%' ";
            } 
            //---
            //var listRecord = (from tbl in db.Products orderby tbl.id descending select tbl);
            //var listRecord = db.Products.Where(tbl=>tbl.category_id == _category_id).OrderByDescending(tbl => tbl.id).ToList();
            DataTable dt = Database.ListDataTable("select * from Products where 1=1 " + strWhere);
            //khi da co du lieu tra ve DataTable -> convert sang kieu du lieu giong voi linq tra ve
            List<Products> listRecord = new List<Products>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    listRecord.Add(new Products() { id = Convert.ToInt32(dr["id"]), name = dr["name"].ToString(), category_id = Convert.ToInt32(dr["category_id"]), discount = Convert.ToInt32(dr["discount"]), price = Convert.ToDouble(dr["price"]), hot = Convert.ToInt32(dr["hot"]), description = dr["description"].ToString(), content = dr["content"].ToString(), photo = dr["photo"].ToString() });
                }
            }
            //goi view
            return View("Search", listRecord.ToPagedList(pageNumber, pageSize));
            //---
        }
        //chi tiet san pham
        public ActionResult Detail(int id)
        {
            //--
            //neu truyen bien star thi Update CSDL
            int _star = Request.QueryString["star"] != null ? Convert.ToInt32(Request.QueryString["star"]) : 0;
            if (Request.QueryString["star"] != null)
            {
                //tao ban ghi,chuan bi them vao table Rating
                Rating newRecord = new Rating();
                newRecord.product_id = id;
                newRecord.star = _star;
                //luu vao csdl
                db.Rating.Add(newRecord);
                db.SaveChanges();
            }

            //---
            var recordStar1 = db.Rating.Where(tbl => tbl.product_id == id && tbl.star == 1).Count();
            var recordStar2 = db.Rating.Where(tbl => tbl.product_id == id && tbl.star == 2).Count();
            var recordStar3 = db.Rating.Where(tbl => tbl.product_id == id && tbl.star == 3).Count();
            var recordStar4 = db.Rating.Where(tbl => tbl.product_id == id && tbl.star == 4).Count();
            var recordStar5 = db.Rating.Where(tbl => tbl.product_id == id && tbl.star == 5).Count();
            ViewBag.star1 = recordStar1;//vi da truyen model roi nen phai dung viewbag de loi n ra dung
            ViewBag.star2 = recordStar2;
            ViewBag.star3 = recordStar3;
            ViewBag.star4 = recordStar4;
            ViewBag.star5 = recordStar5;

            var record = db.Products.Where(tbl => tbl.id == id).FirstOrDefault();
            return View("ProductDetail", record);
        }
        public void Rating(int id)
        {
            //--
            //neu truyen bien star thi Update CSDL
            int _star = Request.QueryString["star"] != null ? Convert.ToInt32(Request.QueryString["star"]) : 0;
            //chi cho phep rating 1 lan o mot phien lam viec
            if (Session["idRating"] == null || Convert.ToInt32(Session["idRating"].ToString()) != id)
            {
                //dat bien session de the hien la phien lam viec nay da rating
                Session["idRating"] = id;
                //tao ban ghi,chuan bi them vao table Rating
                Rating newRecord = new Rating();
                newRecord.product_id = id;
                newRecord.star = _star;
                //luu vao csdl
                db.Rating.Add(newRecord);
                db.SaveChanges();
            }
            //---
            Response.Redirect("/Product/Detail/" + id);
        }
        public ActionResult AjaxSearch()
        {
            string _key = Request.QueryString["key"] != null ? Request.QueryString["key"] : "";
            List<Products> listRecord = db.Products.Where(tbl => tbl.name.Contains(_key)).OrderByDescending(tbl => tbl.id).ToList();
            string strResult = "";
            foreach (var item in listRecord)
            {
                strResult += "<li><a href='/Product/Detail/" + item.id + "'><img src='/Assets/Upload/Products/" + item.photo + "' /> " + item.name + "</a></li>";
            }
            return Content(strResult);
        }
    }
}