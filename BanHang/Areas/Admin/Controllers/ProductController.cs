using BanHang.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BanHang.Common;

namespace BanHang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        Banhang_2119110310Entities8 objbanhang_2119110310Entities8 = new Banhang_2119110310Entities8();
        private List<object> lstBrand;

        // GET: Admin/Product
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
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objProduct.Avartar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
                    }
                    objbanhang_2119110310Entities8.Products.Add(objProduct);
                    objbanhang_2119110310Entities8.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(objProduct);
        }
        public ActionResult Details(int id)
        {
            this.LoadData();
            var objProduct = objbanhang_2119110310Entities8.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objbanhang_2119110310Entities8.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objbanhang_2119110310Entities8.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objbanhang_2119110310Entities8.Products.Remove(objProduct);
            objbanhang_2119110310Entities8.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objProduct = objbanhang_2119110310Entities8.Products.Where(n => n.Id == id).FirstOrDefault();
            Session["imgPro"] = objProduct.Avartar;
            return View(objProduct);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Product objProduct)
        {
            try
            {
                this.LoadData();
                if (objProduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName + extension;
                    objProduct.Avartar = fileName;
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
                }
                else
                {
                    objProduct.Avartar = Session["imgPro"].ToString();
                }
                objbanhang_2119110310Entities8.Entry(objProduct).State = EntityState.Modified;
                objbanhang_2119110310Entities8.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                objProduct = objbanhang_2119110310Entities8.Products.Where(n => n.Id == objProduct.Id).FirstOrDefault();
                Session["imgPro"] = objProduct.Avartar;
                return View(objProduct);
            }

        }
        [HttpGet]
        public ActionResult Trash(int id)
        {
            return View();
        }
        void LoadData()
        {
            Common objCommon = new Common();
            // lấy dữ liệu danh mục dưới DB
            var lstCat = objbanhang_2119110310Entities8.Categories.ToList();
            //convert sang select list dang value, text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            //lay du lieu thuong hieu tu DB
            var lstBrand = objbanhang_2119110310Entities8.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //convert sang select list dang value, text          
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            //San pham loai nao
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductTyoe = new ProductType();
            objProductTyoe.Id = 01;
            objProductTyoe.Name = "Giảm giá sốc";
            lstProductType.Add(objProductTyoe);

            objProductTyoe = new ProductType();
            objProductTyoe.Id = 02;
            objProductTyoe.Name = "Đề xuất";
            lstProductType.Add(objProductTyoe);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            //convert sang select list dang value, text
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }
    }
}