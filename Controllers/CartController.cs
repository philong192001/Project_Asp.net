using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Add_TemplateAdmin.Models;

namespace Add_TemplateAdmin.Controllers
{
    public class CartController : Controller
    {
        public DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            //khai bao danh sach Cart
            List<CartItem> Cart = new List<CartItem>();
            if (Session["Cart"] == null)
                Session["Cart"] = Cart;
            else
                Cart = Session["Cart"] as List<CartItem>;
            return View("Index", Cart);
        }
        public ActionResult Buy(int id)
        {
            //lay ban ghi tuong ung voi id
            var record = db.Products.Where(tbl => tbl.id == id).FirstOrDefault();
            if (record != null)
            {
                //them moi sp vao gio hang
                ShoppingCart objCart = new ShoppingCart();
                objCart.CartAdd(new CartItem() { id = record.id, photo = record.photo, name = record.name, number = 1, price = Convert.ToInt32(record.price) });
            }
            return RedirectToAction("Index","Home");
        }
        public ActionResult Delete(int id)
        {
            ShoppingCart objCart = new ShoppingCart();
            objCart.CartDelete(id);
            return RedirectToAction("Index");
        }
        public ActionResult Update()
        {
            //duyet cac san pham tu gio hang
            if (Session["Cart"] != null)
            {
                List<CartItem> Cart = new List<CartItem>();
                Cart = Session["Cart"] as List<CartItem>;
                foreach (var item in Cart)
                {
                    var cartQuantity = "product_" + item.id;
                    var number = Request.Form[cartQuantity];
                    //Response.Write(number + "<br>");
                    ShoppingCart objCart = new ShoppingCart();
                    objCart.CartUpdate(item.id, Convert.ToInt32(number));
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Destroy()
        {
            ShoppingCart objCart = new ShoppingCart();
            objCart.CartDestroy();
            return RedirectToAction("Index");
        }
        //thanh toan gio hang
        public ActionResult CheckOut()
        {
            //khai bao danh sach Cart
            List<CartItem> Cart = new List<CartItem>();
            Cart = Session["Cart"] as List<CartItem>;
            //duyet cac phan tu trong List Cart, update vao talbe Orders, OrderDetail
            //khoi tao record cua table Orders
            Orders recordOrders = new Orders();
            recordOrders.customer_id = Convert.ToInt32(this.Session["customer_id"]);
            //---
            var objCart = new ShoppingCart();
            recordOrders.price = objCart.CartTotal();
            //---
            recordOrders.status = 0;
            recordOrders.date = DateTime.Now;
            //add ban ghi vao csdl
            db.Orders.Add(recordOrders);
            db.SaveChanges();
            //lay id cua ban ghi vua moi insert
            int _orderId = recordOrders.id;
            //---
            //duyet cac san pham de insert vao table OrderDetail
            foreach (CartItem item in Cart)
            {
                OrderDetail record = new OrderDetail();
                record.order_id = _orderId;
                record.price = item.price;
                record.product_id = item.id;
                record.quantity = item.number;
                //---
                //add ban ghi
                db.OrderDetail.Add(record);
                //---
            }
            db.SaveChanges();
            //huy session Cart
            this.Session["Cart"] = new List<CartItem>();
            return RedirectToAction("Index", "Cart");
        }
    }
}