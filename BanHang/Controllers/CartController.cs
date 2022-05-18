using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHang.Context;
using BanHang.Models;
namespace BanHang.Controllers
{
    public class CartController : Controller
    {
        Banhang_2119110310Entities8 objbanhang_2119110310Entities8 = new Banhang_2119110310Entities8();
        // GET: Cart
        public ActionResult Index()
        {
            return View((List<CartModel>)Session["cart"]);
        }
        public ActionResult AddToCart(int id, double price, int quantity)
        {
            if (quantity < 1)
            {
                return View();
            }
            if (Session["cart"] == null)
            {
                List<CartModel> cart = new List<CartModel>();
                cart.Add(new CartModel { Product = objbanhang_2119110310Entities8.Products.Find(id), Price = price, Quantity = quantity });
                Session["cart"] = cart;
                double total = quantity * price;
                Session["total"] = total;
                Session["count"] = quantity;
            }
            else
            {
                List<CartModel> cart = (List<CartModel>)Session["cart"];
                //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa???
                int index = isExist(id);
                if (index != -1)
                {
                    //nếu sp tồn tại trong giỏ hàng thì cộng thêm số lượng
                    cart[index].Quantity += quantity;
                    Session["total"] = Convert.ToInt32(Session["total"]) + quantity * price;
                }
                else
                {
                    //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    cart.Add(new CartModel { Product = objbanhang_2119110310Entities8.Products.Find(id), Price = price, Quantity = quantity });
                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                    Session["total"] = Convert.ToInt32(Session["total"]) + quantity * price;
                }
                Session["cart"] = cart;
            }
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
        private int isExist(int id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.Id.Equals(id))
                    return i;
            return -1;
        }

        public ActionResult Remove(int Id,  int quantity,double price)
        {
            List<CartModel> li = (List<CartModel>)Session["cart"];
            li.RemoveAll(x => x.Product.Id == Id);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            Session["total"] = Convert.ToInt32(Session["total"]) - quantity * price;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
    }
}