using BanHang.Context;
using BanHang.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Controllers
{
    public class ProductDetailController : Controller
    {
        Banhang_2119110310Entities8 objbanhang_2119110310Entities8 = new Banhang_2119110310Entities8();
        // GET: ProductDetail

        public ActionResult Detail(int Id)
        {
            var objProduct = objbanhang_2119110310Entities8.Products.Where(n => n.Id == Id).FirstOrDefault();            
            return View(objProduct);
        }        
        public ActionResult Sale(string currentFiler, int? page)
        {
            var objSale = new List<Product>();
            ///lay all san pham trong product
            objSale = objbanhang_2119110310Entities8.Products.Where(n => n.PriceDiscount != null).ToList();
            //so luong item cua 1 trang = 6
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            //sap xep theo id san pham,sp moi dua len dau
            Session["countPro"] = objSale.Count;
            return View(objSale.ToPagedList(pageNumber, pageSize));        
        }
    }
}