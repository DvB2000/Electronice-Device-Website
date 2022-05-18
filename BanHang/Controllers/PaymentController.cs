using BanHang.Context;
using BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Controllers
{
    public class PaymentController : Controller
    {
        Banhang_2119110310Entities8 objbanhang_2119110310Entities8 = new Banhang_2119110310Entities8();
        // GET: Payment
        public ActionResult Payment()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                //lay thong tin tu gio hang
                var lstCart = (List<CartModel>)Session["cart"];
                //gán dữ liệu
                Order objOrder = new Order();
                objOrder.Name = "DonHang" + DateTime.Now.ToString("yyyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objbanhang_2119110310Entities8.Orders.Add(objOrder);
                //Luu thong tin vao SQL,Order
                objbanhang_2119110310Entities8.SaveChanges();

                //lay OrderId vua tao de luu vao Orderdetail
                int intOrderId = objOrder.Id;
                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
                foreach (var item in lstCart)
                {
                    OrderDetail objDetail = new OrderDetail();
                    objDetail.Quantity = item.Quantity;
                    objDetail.OrderId = intOrderId;
                    objDetail.ProductId = item.Product.Id;
                    objDetail.Price = ((float)Convert.ToDecimal(item.Product.PriceDiscount * item.Quantity));
                    lstOrderDetail.Add(objDetail);
                }
                objbanhang_2119110310Entities8.OrderDetails.AddRange(lstOrderDetail);
                objbanhang_2119110310Entities8.SaveChanges();

            }
            return View();
        }
    }
}