using BanHang.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        Banhang_2119110310Entities8 objbanhang_2119110310Entities8 = new Banhang_2119110310Entities8();
        // GET: Admin/Home
        public ActionResult Index(string SearchString, string currentFiler, int? page)
        {
            var lstProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFiler;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                // lay san pham theo tu khoa tim kiem
                lstProduct = objbanhang_2119110310Entities8.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                ///lay all san pham trong product
                lstProduct = objbanhang_2119110310Entities8.Products.ToList();
            }
            ViewBag.CurrentFiler = SearchString;
            //so luong item cua 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sap xep theo id san pham,sp moi dua len dau
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
    }
}