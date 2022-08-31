using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace DienThoaiDiDong.Controllers
{
    public class ModuleController : Controller
    {
        private MenuDAO menuDAO = new MenuDAO();
        private SliderDAO sliderDAO = new SliderDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private UserDAO userDAO = new UserDAO();
        // GET: Module
        public ActionResult MainMenu()
        {
            List<Menu> list = menuDAO.getListByParentId("mainmenu", 0);
            return View("MainMenu", list);
        }
        //ManiMenuSub
        public ActionResult MainMenuSub(int id)
        {
            Menu menu = menuDAO.getRow(id);
            List<Menu> list = menuDAO.getListByParentId("mainmenu", id);
            if (list.Count == 0)
            {
                return View("MainMenuSub1", menu);//không co cấp con
            }
            else
            {
                ViewBag.Menu = menu;
                return View("MainMenuSub2", list);
            }

        }
        public ActionResult Slideshow()
        {
            List<Slider> list = sliderDAO.getListByPosition("Slideshow");
            return View("Slideshow", list);
        }
        public ActionResult ListCategory()
        {
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("ListCategory", list);
        }
        public ActionResult FooterMenu()
        {
            List<Menu> list = menuDAO.getListByParentId("footermenu", 0);
            return View("FooterMenu", list);
        }
        public ActionResult Userbar()
        {
            if (!Session["CustomerId"].Equals(""))
            {
                int userid = int.Parse(Session["CustomerId"].ToString());
                User user = userDAO.getRow(userid);
                ViewBag.User = user;
                return View("Userbar");
            }
            else
            {
                return View("Userbar");
            }
        }
        public ActionResult UserContact()
        {
            return View("UserContact");
        }
    }
}