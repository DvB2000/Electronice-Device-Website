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
    public class CategoryController : Controller
    {
        Banhang_2119110310Entities8 objbanhang_2119110310Entities8 = new Banhang_2119110310Entities8();
        // GET: Admin/Category
        public ActionResult Index(string SearchString, string currentFiler, int? page)
        {
            var lstCategory = new List<Category>();
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
                lstCategory = objbanhang_2119110310Entities8.Categories.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                ///lay all san pham trong categỏy
                lstCategory = objbanhang_2119110310Entities8.Categories.ToList();
            }
            ViewBag.CurrentFiler = SearchString;
            //so luong item cua 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sap xep theo id san pham,sp moi dua len dau
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objCategory.Avartar = fileName;
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
                    }
                    objbanhang_2119110310Entities8.Categories.Add(objCategory);
                    objbanhang_2119110310Entities8.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(objCategory);
        }
        public ActionResult Details(int id)
        {
            var objCategory = objbanhang_2119110310Entities8.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objbanhang_2119110310Entities8.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Delete(Category objCate)
        {
            var objCategory = objbanhang_2119110310Entities8.Categories.Where(n => n.Id == objCate.Id).FirstOrDefault();
            objbanhang_2119110310Entities8.Categories.Remove(objCategory);
            objbanhang_2119110310Entities8.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objCategory = objbanhang_2119110310Entities8.Categories.Where(n => n.Id == id).FirstOrDefault();
            Session["imgCate"] = objCategory.Avartar;
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Edit(int id, Category objCategory)
        {
            this.LoadData();
            if (objCategory.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                fileName = fileName + extension;
                objCategory.Avartar = fileName;
                objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
            }
            else
            {
                objCategory.Avartar = Session["imgCate"].ToString();
            }
            objbanhang_2119110310Entities8.Entry(objCategory).State = EntityState.Modified;
            objbanhang_2119110310Entities8.SaveChanges();
            return RedirectToAction("Index");
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
        }
    }
}