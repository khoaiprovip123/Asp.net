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
    public class UserController : BaseController
    {
        UserDAO userDAO = new UserDAO();

        // GET: Admin/User
        public ActionResult Index()
        {
            return View(userDAO.getList("Index"));
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( User user)
        {
            if (ModelState.IsValid)
            {
                user.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                user.UpdatedAt = DateTime.Now;
                user.CreatedAt = DateTime.Now;
                userDAO.Insert(user);
                TempData["message"] = new XMessage("success", "Thêm người dùng thành công");
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                user.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                user.UpdatedAt = DateTime.Now;
                userDAO.Update(user);
                TempData["message"] = new XMessage("success", "Chỉnh sửa thành công");
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = userDAO.getRow(id);
            userDAO.Delete(user);
            TempData["message"] = new XMessage("danger", "Xóa người dùng thành công");
            return RedirectToAction("Index");
        }
        //--------------------------------------------------------
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã liên hệ không tồn tại");
                return RedirectToAction("Index", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "User");
            }
            user.Status = (user.Status == 1) ? 2 : 1;
            user.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            user.UpdatedAt = DateTime.Now;
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "User");
        }
        // Xóa Vào thùng rác
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "User");
            }
            user.Status = 0;
            user.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            user.UpdatedAt = DateTime.Now;
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "User");
        }
        public ActionResult Trash()
        {
            return View(userDAO.getList("Trash"));
        }
        //khôi phục
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "User");
            }
            user.Status = 2;
            user.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            user.UpdatedAt = DateTime.Now;
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "khôi phục thành công");
            return RedirectToAction("Trash", "User");
        }
        //NameCusstomer
        public String NameCustomer(int userid)
        {


            User user = userDAO.getRow(userid);
            if (user == null)
            {
                return "";
            }
            else
            {
                return user.Fullname;
            }
        }
        

    }
}
