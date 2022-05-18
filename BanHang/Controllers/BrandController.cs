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
    public class BrandController : Controller
    {
        Banhang_2119110310Entities8 objbanhang_2119110310Entities8 = new Banhang_2119110310Entities8();
        // GET: Brand
        public ActionResult Brand()
        {
            var lstBrand = objbanhang_2119110310Entities8.Brands.ToList();
            return View(lstBrand);
        }
        public ActionResult ProductBrand(string currentFiler, int? page, int Id)
        {
            var lstProduct = new List<Product>();          
                ///lay all san pham trong product
                lstProduct = objbanhang_2119110310Entities8.Products.Where(n => n.BrandId == Id).ToList();
            //so luong item cua 1 trang = 6
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            //sap xep theo id san pham,sp moi dua len dau
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            //dem san pham
            Session["countProbrand"] = lstProduct.Count;
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }

    }
}