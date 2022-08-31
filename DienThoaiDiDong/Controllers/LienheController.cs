using MyClass.DAO;
using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DienThoaiDiDong.Controllers
{
    public class LienheController : Controller
    {
        public ContactDAO contactDAO = new ContactDAO();
        public UserDAO userDAO = new UserDAO();
        MyDBcontext db = new MyDBcontext();

        // GET: Lienhe
        public ActionResult Index()
        {
            ViewBag.InfoAdmin = userDAO.getRow("Admin");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                contact.CreatedAt = DateTime.Now;

                contact.Status = 1;
                contactDAO.Insert(contact);
                TempData["message"] = new XMessage("success", "Gửi thanh công thành công");
                contactDAO.Insert(contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }
    }
}