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
    public class CategoryController : BaseController
    {

        CategoryDAO categoryDAO = new CategoryDAO();
        LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(categoryDAO.getList("Index"));
        }
       
        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrders = new SelectList(categoryDAO.getList("Index"), "Orders", "Name");
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                //Xử lý thêm thông tin
                category.Slug = XString.Str_Slug(category.Name);
                if (category.ParentId == null)
                {
                    category.ParentId = 0;
                }
                if (category.Orders == null)
                {
                    category.Orders = 1;
                }
                else
                {
                    category.Orders += 1;
                }
                category.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                category.CreatedAt = DateTime.Now;
                if (categoryDAO.Insert(category) == 1)
                {
                    Link link = new Link();
                    link.Slug = category.Slug;
                    link.TableId = category.Id;
                    link.TypeLink = "category";
                    linkDAO.Insert(link);
                }
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrders = new SelectList(categoryDAO.getList("Index"), "Orders", "Name");
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrders = new SelectList(categoryDAO.getList("Index"), "Orders", "Name", 0);
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = XString.Str_Slug(category.Name);
                if (category.ParentId == null)
                {
                    category.ParentId = 0;
                }
                if (category.Orders == null)
                {
                    category.Orders = 1;
                }
                else
                {
                    category.Orders += 1;
                }
                category.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                category.UpdatedAt = DateTime.Now;
                TempData["message"] = new XMessage("success", "Chỉnh sửa thành công");
                if (categoryDAO.Update(category) == 1)
                {
                    Link link = linkDAO.getRow(category.Id, "category");
                    link.Slug = category.Slug;
                    linkDAO.Update(link);
                }
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrders = new SelectList(categoryDAO.getList("Index"), "Orders", "Name", 0);
            return View(category);
        }


        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = categoryDAO.getRow(id);
            Link link = linkDAO.getRow(category.Id, "category");
            if (categoryDAO.Delete(category) == 1)
            {
                linkDAO.Delete(link);
            }

            TempData["message"] = new XMessage("success", "Xóa thành thành công");
            return RedirectToAction("Trash", "Category");
        }
        //thùng rác :Admin/Trash
        public ActionResult Trash()
        {
            return View(categoryDAO.getList("Trash"));
        }


        //thay đổi trạng thái
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            category.Status = (category.Status == 1) ? 2 : 1;
            category.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            category.UpdatedAt = DateTime.Now;
            categoryDAO.Update(category);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Category");
        }

        // Xóa Vào thùng rác
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            category.Status = 0;
            category.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            category.UpdatedAt = DateTime.Now;
            categoryDAO.Update(category);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Category");
        }

        //khôi phục
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Category");
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Category");
            }
            category.Status = 2;
            category.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            category.UpdatedAt = DateTime.Now;
            categoryDAO.Update(category);
            TempData["message"] = new XMessage("success", "khôi phục thành công");
            return RedirectToAction("Trash", "Category");
        }
    }
}
