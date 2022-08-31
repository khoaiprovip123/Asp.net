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
    public class TypeProductController : BaseController
    {
        TypeProductDAO typeProductDAO = new TypeProductDAO();

        // GET: Admin/TypeProduct
        public ActionResult Index()
        {
            return View(typeProductDAO.getList());
        }

        // GET: Admin/TypeProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeProduct typeProduct = typeProductDAO.getRow(id);
            if (typeProduct == null)
            {
                return HttpNotFound();
            }
            return View(typeProduct);
        }

        // GET: Admin/TypeProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TypeProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedBy,UpdatedBy,Status")] TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {
                typeProductDAO.Insert(typeProduct);
                typeProduct.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index");
            }

            return View(typeProduct);
        }

        // GET: Admin/TypeProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeProduct typeProduct = typeProductDAO.getRow(id);
            if (typeProduct == null)
            {
                return HttpNotFound();
            }
            return View(typeProduct);
        }

        // POST: Admin/TypeProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedBy,UpdatedBy,Status")] TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {
                typeProductDAO.Update(typeProduct);
                typeProduct.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            return View(typeProduct);
        }

        // GET: Admin/TypeProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeProduct typeProduct = typeProductDAO.getRow(id);
            if (typeProduct == null)
            {
                return HttpNotFound();
            }
            return View(typeProduct);
        }

        // POST: Admin/TypeProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeProduct typeProduct = typeProductDAO.getRow(id);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Index");
        }
        //thay đổi trạng thái
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "TypeProduct");
            }
            TypeProduct typeProduct = typeProductDAO.getRow(id);
            if (typeProduct == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "TypeProduct");
            }
            typeProduct.Status = (typeProduct.Status == 1) ? 2 : 1;
            typeProduct.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            typeProductDAO.Update(typeProduct);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "TypeProduct");
        }

    }
}
