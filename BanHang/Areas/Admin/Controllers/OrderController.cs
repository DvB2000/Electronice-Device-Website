using BanHang.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BanHang.Common;

namespace BanHang.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {

        Banhang_2119110310Entities8 objbanhang_2119110310Entities8 = new Banhang_2119110310Entities8();
        // GET: Admin/Order
        public ActionResult Index(string SearchString, string currentFiler, int? page)
        {
            var lstOrder = new List<Order>();
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
                lstOrder = objbanhang_2119110310Entities8.Orders.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                ///lay all san pham trong categỏy
                lstOrder = objbanhang_2119110310Entities8.Orders.ToList();
            }
            ViewBag.CurrentFiler = SearchString;
            //so luong item cua 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sap xep theo id san pham,sp moi dua len dau
            lstOrder = lstOrder.OrderByDescending(n => n.Id).ToList();
            return View(lstOrder.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.loadData();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Order objOrder)
        {
            this.loadData();
            if (ModelState.IsValid)
            {
                try
                {
                    objbanhang_2119110310Entities8.Configuration.ValidateOnSaveEnabled = false;
                    objbanhang_2119110310Entities8.Orders.Add(objOrder);
                    objbanhang_2119110310Entities8.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(objOrder);
        }

        public ActionResult Details(int id)
        {
            var objOrder = objbanhang_2119110310Entities8.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objOrder = objbanhang_2119110310Entities8.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }
        [HttpPost]
        public ActionResult Delete(Order objOr)
        {
            var objOrder = objbanhang_2119110310Entities8.Orders.Where(n => n.Id == objOr.Id).FirstOrDefault();
            objbanhang_2119110310Entities8.Orders.Remove(objOrder);
            objbanhang_2119110310Entities8.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.loadData();
            var objOrder = objbanhang_2119110310Entities8.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }
        [HttpPost]
        public ActionResult Edit(int id, Order objOrder)
        {
            this.loadData();
            objbanhang_2119110310Entities8.Entry(objOrder).State = EntityState.Modified;
            objbanhang_2119110310Entities8.SaveChanges();
            return RedirectToAction("Index");
        }
        void loadData()
        {
            Common objCommon = new Common();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            List<OrderStatus> lstOrder = new List<OrderStatus>();
            OrderStatus objOrder = new OrderStatus();
            objOrder.Status = 01;
            objOrder.Name = "Đang giao";
            lstOrder.Add(objOrder);

            objOrder = new OrderStatus();
            objOrder.Status = 02;
            objOrder.Name = "Đã giao";
            lstOrder.Add(objOrder);

            objOrder = new OrderStatus();
            objOrder.Status = 03;
            objOrder.Name = "Hủy hàng";
            lstOrder.Add(objOrder);

            objOrder = new OrderStatus();
            objOrder.Status = 04;
            objOrder.Name = "Trả hàng";
            lstOrder.Add(objOrder);

            DataTable dtOrder = converter.ToDataTable(lstOrder);
            //convert sang select list dang value, texts
            ViewBag.Order = objCommon.ToSelectList(dtOrder, "Status", "Name");
        }
    }
}