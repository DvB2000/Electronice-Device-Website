using BanHang.Context;
using BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Controllers
{
    public class UserController : Controller
    {
        Banhang_2119110310Entities8 objbanhang_2119110310Entities8 = new Banhang_2119110310Entities8();
        // GET: user
        public ActionResult Main(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            HomeModel objModel = new HomeModel();
            objModel.ListUser = objbanhang_2119110310Entities8.Users.Where(u => u.Id == id).ToList();
            objModel.ListOrder = objbanhang_2119110310Entities8.Orders.ToList();
            objModel.ListOrDerDetail = objbanhang_2119110310Entities8.OrderDetails.ToList();
            objModel.ListProduct = objbanhang_2119110310Entities8.Products.ToList();
            return View(objModel);
        }
        // GET: Order
        public ActionResult Order(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            HomeModel objModel = new HomeModel();
            objModel.ListUser = objbanhang_2119110310Entities8.Users.Where(u => u.Id == id).ToList();
            objModel.ListOrder = objbanhang_2119110310Entities8.Orders.ToList();
            objModel.ListOrDerDetail = objbanhang_2119110310Entities8.OrderDetails.ToList();
            objModel.ListProduct = objbanhang_2119110310Entities8.Products.ToList();
            return View(objModel);
        }

    }
}