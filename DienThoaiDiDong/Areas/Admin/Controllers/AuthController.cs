using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
namespace DienThoaiDiDong.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        private MyDBcontext db = new MyDBcontext();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            ViewBag.Error = "";
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["UserId"] = "";
            Session["FullName"] = "";
            return View("Login");
        }
        [HttpPost]
        public ActionResult DoLogin(FormCollection field  )
        {
            ViewBag.Error = "";
            string username = field["username"];
            string password = XString.ToMD5(field["password"]);
            User user = db.Users
                .Where(m => m.Roles == "Admin" && m.Status == 1 && (m.Username == username || m.Email == username))
                .FirstOrDefault();
            if(user!=null)
            {
                if(user.Password.Equals(password))
                {
                    //khớp
                    Session["UserAdmin"] = username;
                    Session["UserId"] = user.Id.ToString();
                    Session["FullName"] = user.Fullname;
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.Error = "<p class='login-box-msg text-danger'>Mật khẩu Không chính xác !</p>";
                }

            }   else
            {
                ViewBag.Error = "<p class='login-box-msg text-danger'>Tài khoản: " + username + " Không tồn tại !</p>";
            }    
            return View("Login");
        }
    }
}