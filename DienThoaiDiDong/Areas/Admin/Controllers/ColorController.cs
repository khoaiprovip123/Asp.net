using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;


namespace DienThoaiDiDong.Areas.Admin.Controllers
{
    public class ColorController : BaseController
    {

        ColorDAO colorDAO = new ColorDAO();
        // GET: Admin/color
        public ActionResult Index()
        {
            return View(colorDAO.getList());
        }

        // GET: Admin/color/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color color = colorDAO.getRow(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        // GET: Admin/color/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/color/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Color color)
        {
            if (ModelState.IsValid)
            {
               
                color.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                color.CreatedAt = DateTime.Now;
                colorDAO.Insert(color);
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            return View(color);
        }

        // GET: Admin/color/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color color = colorDAO.getRow(id);
            if (color == null)
            {
                return HttpNotFound();
            }
           
            return View(color);
        }

        // POST: Admin/color/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Color color)
        {
            if (ModelState.IsValid)
            {
                color.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                color.UpdatedAt = DateTime.Now;
                TempData["message"] = new XMessage("success", "Chỉnh sửa thành công");
                colorDAO.Update(color);
                return RedirectToAction("Index");
            }
            return View(color);
        }


        // GET: Admin/color/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color color = colorDAO.getRow(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        // POST: Admin/color/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Color color = colorDAO.getRow(id);
            colorDAO.Delete(color);
            TempData["message"] = new XMessage("success", "Xóa thành thành công");
            return RedirectToAction("Trash", "color");
        }
        
        //thay đổi trạng thái
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Color");
            }
            Color color = colorDAO.getRow(id);
            if (color == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Color");
            }
            color.Status = (color.Status == 1) ? 2 : 1;
            color.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            color.UpdatedAt = DateTime.Now;
            colorDAO.Update(color);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Color");
        }

    }
}
