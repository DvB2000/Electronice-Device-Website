using BanHang.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        Banhang_2119110310Entities8 objbanhang_2119110310Entities8 = new Banhang_2119110310Entities8();
        // GET: Admin/User
        public ActionResult Index(string SearchString, string currentFiler, int? page)
        {
            var lstUser = new List<User>();
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
                lstUser = objbanhang_2119110310Entities8.Users.Where(n => n.Email.Contains(SearchString)).ToList();
            }
            else
            {
                ///lay all san pham trong categỏy
                lstUser = objbanhang_2119110310Entities8.Users.ToList();
            }
            ViewBag.CurrentFiler = SearchString;
            //so luong item cua 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sap xep theo id san pham,sp moi dua len dau
            lstUser = lstUser.OrderByDescending(n => n.Id).ToList();
            return View(lstUser.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User objUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objUser.Password = GetMD5(objUser.Password);
                    objbanhang_2119110310Entities8.Configuration.ValidateOnSaveEnabled = false;
                    objbanhang_2119110310Entities8.Users.Add(objUser);
                    objbanhang_2119110310Entities8.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(objUser);
        }
       
        public ActionResult Details(int id)
        {
            var objUser = objbanhang_2119110310Entities8.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objUser = objbanhang_2119110310Entities8.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Delete(User objU)
        {
            var objUser = objbanhang_2119110310Entities8.Users.Where(n => n.Id == objU.Id).FirstOrDefault();
            objbanhang_2119110310Entities8.Users.Remove(objUser);
            objbanhang_2119110310Entities8.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objUser = objbanhang_2119110310Entities8.Users.Where(n => n.Id == id).FirstOrDefault();
            Session["mk"] = objUser.Password;
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Edit(int id, User objUser)
        {
            if (objUser.Password != null)
            {
                objUser.Password = GetMD5(objUser.Password);
                objbanhang_2119110310Entities8.Configuration.ValidateOnSaveEnabled = false;
            }
            else
            {
                objUser.Password = Session["mk"].ToString();
                objbanhang_2119110310Entities8.Configuration.ValidateOnSaveEnabled = false;
            }
            objbanhang_2119110310Entities8.Entry(objUser).State = EntityState.Modified;
            objbanhang_2119110310Entities8.SaveChanges();
            return RedirectToAction("Index");               
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        
    }
}