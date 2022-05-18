using BanHang.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Controllers
{
    public class CategoryController : Controller
    {
        Banhang_2119110310Entities8 objbanhang_2119110310Entities8 = new Banhang_2119110310Entities8();
        // GET: Category
        public ActionResult Category()
        {
            var lstCategory = objbanhang_2119110310Entities8.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(string currentFiler, int? page, int Id)
        {
            var lstProduct = new List<Product>();
            ///lay all san pham trong product
            lstProduct = objbanhang_2119110310Entities8.Products.Where(n => n.CategoryId == Id).ToList();
            //so luong item cua 1 trang = 6
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            //sap xep theo id san pham,sp moi dua len dau
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            // dem san pham
            Session["countPro"] = lstProduct.Count;
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
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
            //dem san pham
            //sap xep theo id san pham,sp moi dua len dau
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }

    }
}