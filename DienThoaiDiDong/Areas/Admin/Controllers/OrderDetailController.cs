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
    public class OrderDetailController : BaseController
    {
        OrderdetailDAO orderdetailDAO = new OrderdetailDAO();

        // GET: Admin/OrderDetail
        public ActionResult Index(int? orderid)
        {
            return View(orderdetailDAO.getList(orderid));
        }

        // GET: Admin/OrderDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = orderdetailDAO.getRow(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: Admin/OrderDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/OrderDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                orderdetailDAO.Insert(orderDetail);
                return RedirectToAction("Index");
            }

            return View(orderDetail);
        }

        // GET: Admin/OrderDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = orderdetailDAO.getRow(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Admin/OrderDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                orderdetailDAO.Update(orderDetail);
                return RedirectToAction("Index");
            }
            return View(orderDetail);
        }

        // GET: Admin/OrderDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = orderdetailDAO.getRow(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Admin/OrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = orderdetailDAO.getRow(id);
            orderdetailDAO.Delete(orderDetail);
           
            return RedirectToAction("Index");
        }

    }
}
