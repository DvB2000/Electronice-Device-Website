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
    public class BrandController : Controller
    {
        Banhang_2119110310Entities8 objbanhang_2119110310Entities8 = new Banhang_2119110310Entities8();
        // GET: Admin/Brand
        public ActionResult Index(string SearchString, string currentFiler, int? page)
        {
            var lstBrand = new List<Brand>();
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
                lstBrand = objbanhang_2119110310Entities8.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                ///lay all san pham trong categỏy
                lstBrand = objbanhang_2119110310Entities8.Brands.ToList();
            }
            ViewBag.CurrentFiler = SearchString;
            //so luong item cua 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sap xep theo id san pham,sp moi dua len dau
            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (objBrand.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                        string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objBrand.Avatar = fileName;
                        objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/brand"), fileName));
                    }
                    objbanhang_2119110310Entities8.Brands.Add(objBrand);
                    objbanhang_2119110310Entities8.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(objBrand);
        }
        public ActionResult Details(int id)
        {
            var objBrand = objbanhang_2119110310Entities8.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = objbanhang_2119110310Entities8.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand objBra)
        {
            var objBrand = objbanhang_2119110310Entities8.Brands.Where(n => n.Id == objBra.Id).FirstOrDefault();
            objbanhang_2119110310Entities8.Brands.Remove(objBrand);
            objbanhang_2119110310Entities8.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objBrand = objbanhang_2119110310Entities8.Brands.Where(n => n.Id == id).FirstOrDefault();
            Session["imgBrand"] = objBrand.Avatar;
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Edit(int id,Brand objBrand)
        {
            this.LoadData();
            if (objBrand.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                fileName = fileName + extension;
                objBrand.Avatar = fileName;
                objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/brand"), fileName));
            }
            else
            {
                objBrand.Avatar = Session["imgBrand"].ToString();
            }
            objbanhang_2119110310Entities8.Entry(objBrand).State = EntityState.Modified;
            objbanhang_2119110310Entities8.SaveChanges();
            return RedirectToAction("Index");
        }
        void LoadData()
        {
            Common objCommon = new Common();
            // lấy dữ liệu danh mục dưới DB
            //convert sang select list dang value, text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            //lay du lieu thuong hieu tu DB
            var lstBrand = objbanhang_2119110310Entities8.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //convert sang select list dang value, text          
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");
        }
    }
}