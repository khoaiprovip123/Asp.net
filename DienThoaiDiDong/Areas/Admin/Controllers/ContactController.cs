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
    public class ContactController : BaseController
    {

        ContactDAO contactDAO = new ContactDAO();
        // GET: Admin/Contact
        public ActionResult Index()
        {
            return View(contactDAO.getList("Index"));
        }
        // GET: Admin/Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Admin/Contact/Create
        public ActionResult Create()
        {
          
            return View();
        }

        // POST: Admin/Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                //Xử lý thêm thông tin
              
                contact.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                contact.UpdatedAt = DateTime.Now;
                contact.CreatedAt = DateTime.Now;
                contact.Status = 1;
                contactDAO.Insert(contact);
                TempData["message"] = new XMessage("success", "Gửi thanh công thành công");
                return RedirectToAction("Index");
            }
           
            return View(contact);
        }

        // GET: Admin/Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                contact.UpdatedAt = DateTime.Now;
                contactDAO.Update(contact);
                TempData["message"] = new XMessage("success", "Chỉnh sửa thành công");
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Admin/Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }


        // POST: Admin/Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = contactDAO.getRow(id);
            contactDAO.Delete(contact);
            TempData["message"] = new XMessage("success", "Xóa thành thành công");
            return RedirectToAction("Index");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã liên hệ không tồn tại");
                return RedirectToAction("Index", "Contact");
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Contact");
            }
            contact.Status = (contact.Status == 1) ? 2 : 1;
            contact.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            contact.UpdatedAt = DateTime.Now;
            contactDAO.Update(contact);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Contact");
        }
        // Xóa Vào thùng rác
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Contact");
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Contact");
            }
            contact.Status = 0;
            contact.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            contact.UpdatedAt = DateTime.Now;
            contactDAO.Update(contact);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Contact");
        }
        public ActionResult Trash()
        {
            return View(contactDAO.getList("Trash"));
        }
        //khôi phục
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Contact");
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Contact");
            }
            contact.Status = 2;
            contact.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            contact.UpdatedAt = DateTime.Now;
            contactDAO.Update(contact);
            TempData["message"] = new XMessage("success", "khôi phục thành công");
            return RedirectToAction("Trash", "Contact");
        }

    }
}
