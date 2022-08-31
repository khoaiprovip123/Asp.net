using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace DienThoaiDiDong.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        OrderDAO orderDAO = new OrderDAO(); 
        OrderdetailDAO orderdetailDAO = new OrderdetailDAO();

        // GET: Admin/Order
        public ActionResult Index()
        {
            return View(orderDAO.getList("Index"));
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListChiTiet = orderdetailDAO.getList(id);
            return View(order);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                order.CreatedAt = DateTime.Now;
                orderDAO.Insert(order);
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                order.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                order.UpdatedAt = DateTime.Now;
                TempData["message"] = new XMessage("success", "Chỉnh sửa thành công");
                orderDAO.Update(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = orderDAO.getRow(id);
            orderDAO.Delete(order);
            return RedirectToAction("Index");
        }
        //thùng rác :Admin/Trash
        public ActionResult Trash()
        {
            return View(orderDAO.getList("Trash"));
        }


        //thay đổi trạng thái
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            order.Status = (order.Status == 1) ? 2 : 1;
            order.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            order.UpdatedAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Order");
        }

        // Xóa Vào thùng rác
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            order.Status = 0;
            order.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            order.UpdatedAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Order");
        }

        //khôi phục
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Order");
            }
            order.Status = 2;
            order.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            order.UpdatedAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "khôi phục thành công");
            return RedirectToAction("Trash", "Order");
        }

        //Destroy
        public ActionResult Destroy(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            if(order.Status==1 || order.Status==2)
            {
                order.Status = 0;
                order.UpdatedBy = 1;
                order.UpdatedAt = DateTime.Now;
            }
            else
            {
                if(order.Status==3)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng trong trạng thái vận chuyển không thể hủy");
                }
                if (order.Status == 4)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đã giao không thể hủy");
                }
                return RedirectToAction("Index", "Order");
            }
           
            
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Đã hủy đơn hàng thành công");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult XacThuc(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            if (order.Status == 1 || order.Status == 2)
            {
                order.Status = 2;
                order.UpdatedBy = 1;
                order.UpdatedAt = DateTime.Now;
            }
            else
            {
                if (order.Status == 4)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đã giao không thể xác thực");
                }

                if (order.Status == 0)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đã xác thực không thể hủy");
                }
                return RedirectToAction("Index", "Order");
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Đơn hàng đã xác thực thành công");
            return RedirectToAction("Index", "Order");
        }
        //DangVanChuyen
        public ActionResult DangVanChuyen(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            if (order.Status == 1 || order.Status == 2)
            {
                order.Status = 3;
                order.UpdatedBy = 1;
                order.UpdatedAt = DateTime.Now;
            }
            else
            {
               
                if (order.Status == 2)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đã được xác thực.");
                }
                if (order.Status == 4)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đã giao không thể hủy");
                }
                return RedirectToAction("Index", "Order");
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Đơn hàng đang chuẩn bị vận chuyển");
            return RedirectToAction("Index", "Order");
        }
        //DangVanChuyen
        public ActionResult ThanhCong(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            if (order.Status == 1 || order.Status == 3)
            {
                order.Status = 4;
                order.UpdatedBy = 1;
                order.UpdatedAt = DateTime.Now;
            }
            else
            {

                if (order.Status == 3)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng trong trạng thái vận chuyển không thể hủy");
                }
                return RedirectToAction("Index", "Order");
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Đơn hàng giao thành công");
            return RedirectToAction("Index", "Order");
        }
    }

}
