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
    public class MenuController : BaseController
    {
        CategoryDAO categoryDAO = new CategoryDAO();
        MenuDAO menuDAO = new MenuDAO();
        TopicDAO topicDAO = new TopicDAO();
        PostDAO postDAO = new PostDAO();

        // GET: Admin/Category
        public ActionResult Index()
        {
            ViewBag.ListCategory = categoryDAO.getList("Index");
            ViewBag.ListTopic = topicDAO.getList("Index");
            ViewBag.ListPage = postDAO.getList("Index","Page");
            List<Menu> menu = menuDAO.getList("Index" );

            return View("Index", menu);
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if(!string.IsNullOrEmpty(form["ThemCategory"]))
            {
                if(!string.IsNullOrEmpty(form["nameCat"]))
                {
                    var listitem = form["nameCat"];
                    var listarr = listitem.Split(',');
                    foreach(var row in listarr)
                    {
                        int id = int.Parse(row);
                        Category category = categoryDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = category.Name;
                        menu.Link = category.Slug;
                        menu.Table = category.Id;
                        menu.Type = "category";
                        menu.Position = form["Position"];
                        menu.Parentid = 0;
                        menu.Orders = 0;
                        menu.CreatedAt = DateTime.Now;
                        menu.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.Status = 2;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm menu thành công");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn danh mục sản phẩm");
                }

            }
//-------------------------------------------------------Thêm Topic------------------------------------------------
            if (!string.IsNullOrEmpty(form["ThemTopic"]))
            {
                if (!string.IsNullOrEmpty(form["nameTop"]))
                {
                    var listitem = form["nameTop"];
                    var listarr = listitem.Split(',');
                    foreach (var row in listarr)
                    {
                        int id = int.Parse(row);
                        Topic topic = topicDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = topic.Name;
                        menu.Link = topic.Slug;
                        menu.Table = topic.Id;
                        menu.Type = "topic";
                        menu.Position = form["Position"];
                        menu.Parentid = 0;
                        menu.Orders = 0;
                        menu.CreatedAt = DateTime.Now;
                        menu.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.Status = 2;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm menu thành công");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn danh mục sản phẩm");
                }
            }
      //===================================page===========================================================
            if (!string.IsNullOrEmpty(form["ThemPage"]))
            {
                if (!string.IsNullOrEmpty(form["namePage"]))
                {
                    var listitem = form["namePage"];
                    var listarr = listitem.Split(',');
                    foreach (var row in listarr)
                    {
                        int id = int.Parse(row);
                        Post post = postDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = post.Title;
                        menu.Link = post.Slug;
                        menu.Table = post.Id;
                        menu.Type = "page";
                        menu.Position = form["Position"];
                        menu.Parentid = 0;
                        menu.Orders = 0;
                        menu.CreatedAt = DateTime.Now;
                        menu.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.Status = 2;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm menu thành công");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn danh mục sản phẩm");
                }
            }
            //===================================page===========================================================
            //ThemCustom
            if (!string.IsNullOrEmpty(form["ThemCustom"]))
            {
                if (!string.IsNullOrEmpty(form["name"])&& !string.IsNullOrEmpty(form["link"]))
                {
                    Menu menu = new Menu();
                    menu.Name = form["name"];
                    menu.Link = form["link"];
                    menu.Type = "custom";
                    menu.Position = form["Position"];
                    menu.Parentid = 0;
                    menu.Orders = 0;
                    menu.CreatedAt = DateTime.Now;
                    menu.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                    menu.Status = 2;
                    menuDAO.Insert(menu);
                    TempData["message"] = new XMessage("success", "Thêm menu thành công");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa nhập đủ thông tin ");
                }
            }    

                return RedirectToAction("Index", "Menu");
        }









        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Admin/Category/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ListMe = new SelectList(menuDAO.getList("Index"), "Id", "Name");
        //    ViewBag.ListOrders = new SelectList(menuDAO.getList("Index"), "Orders", "Name");
        //    return View();
        //}

        //// POST: Admin/Category/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Menu menu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Xử lý thêm thông tin
        //        menu.Link = XString.Str_Slug(menu.Name);
        //        if (menu.Parentid == null)
        //        {
        //            menu.Parentid = 0;
        //        }
        //        if (menu.Orders == null)
        //        {
        //            menu.Orders = 1;
        //        }
        //        else
        //        {
        //            menu.Orders += 1;
        //        }
        //        menu.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
        //        menu.CreatedAt = DateTime.Now;
        //        menuDAO.Insert(menu);
        //        TempData["message"] = new XMessage("success", "Cập nhật thành công");
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ListMe = new SelectList(menuDAO.getList("Index"), "Id", "Name");
        //    ViewBag.ListOrders = new SelectList(menuDAO.getList("Index"), "Orders", "Name");
        //    return View(menu);
        //}

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Orders", "Name", 0);
            Menu menu = menuDAO.getRow(id);
            return View("Edit", menu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                // xử lý cập nhật
                if (menu.Parentid == null)
                {
                    menu.Parentid = 0;
                }
                if (menu.Orders == null)
                {
                    menu.Orders = 0;
                }
                else
                {
                    menu.Orders += 1;
                }
                menu.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                menu.UpdatedAt = DateTime.Now;
                menuDAO.Update(menu);
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Orders", "Name", 0);
            return View(menu);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = menuDAO.getRow(id);
            menuDAO.Delete(menu);
            TempData["message"] = new XMessage("success", "Xóa thành thành công");
            return RedirectToAction("Trash", "Menu");
        }
        //thùng rác :Admin/Trash
        public ActionResult Trash()
        {
            return View(menuDAO.getList("Trash"));
        }


        //thay đổi trạng thái
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Menu");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Menu");
            }
            menu.Status = (menu.Status == 1) ? 2 : 1;
            menu.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            menu.UpdatedAt = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Menu");
        }

        // Xóa Vào thùng rác
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Menu");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Menu");
            }
            menu.Status = 0;
            menu.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            menu.UpdatedAt = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Menu");
        }

        //khôi phục
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Menu");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Menu");
            }
            menu.Status = 2;
            menu.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            menu.UpdatedAt = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Menu");
        }
    }
}
