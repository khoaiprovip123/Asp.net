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
    public class LinkController : BaseController
    {
        LinkDAO linkDAO = new LinkDAO();

        // GET: Admin/Link
        public ActionResult Index()
        {
            return View(linkDAO.getList());
        }

        // GET: Admin/Link/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = linkDAO.getRow(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // GET: Admin/Link/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Link/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Link link)
        {
            if (ModelState.IsValid)
            {
                linkDAO.Insert(link);
                TempData["message"] = new XMessage("success", "Lưu thành công");
                return RedirectToAction("Index");
            }

            return View(link);
        }

        // GET: Admin/Link/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = linkDAO.getRow(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // POST: Admin/Link/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Slug,TypeLink,TableId")] Link link)
        {
            if (ModelState.IsValid)
            {
                linkDAO.Update(link);
                TempData["message"] = new XMessage("success", "Chỉnh sửa thành công");
                return RedirectToAction("Index");
            }
            return View(link);
        }

        // GET: Admin/Link/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = linkDAO.getRow(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // POST: Admin/Link/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Link link = linkDAO.getRow(id);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Index");
        }

    }
}
