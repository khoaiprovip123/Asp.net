using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace DienThoaiDiDong.Controllers
{
    public class KhachhangController : Controller
    {
        UserDAO userDAO = new UserDAO();
        // GET: Khachhang
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection filed)
        {
            String username = filed["username"];
            String password = XString.ToMD5(filed["password"]);
            //
            User row_user = userDAO.getRow(username, "Customer");
            String strError = "";
            if (row_user == null)
            {
                strError = " Tên đăng nhập không tồn tại";
            }
            else
            {
                if (password.Equals(row_user.Password))
                {
                    Session["UserCustomer"] = username;
                    Session["CustomerId"] = row_user.Id;
                    return Redirect("~/");
                }
                else
                {
                    strError = password;
                }
            }
            ViewBag.Error = "<span class='text-danger'>" + strError + "</div>";
            return View();
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(FormCollection form, User user)
        {
            if (ModelState.IsValid)
            {
                string username = form["username"];
                string password = XString.ToMD5(form["password"]);

                user.Username = username;
                user.Password = password;
                user.Roles = "Customer";
                user.Gender = 1;
                user.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                user.CreatedAt = DateTime.Now;
                user.Status = 1;

                userDAO.Insert(user);
                return Redirect("/");
            }
            return View(user);
        }
        public ActionResult DangXuat()
        {
            Session["UserCustomer"] = "";
            Session["CustomerId"] = "";
            return Redirect("/");

        }

    }
}