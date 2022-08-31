using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DienThoaiDiDong.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        public BaseController ()
        {
            if (System.Web.HttpContext.Current.Session["UserAdmin"].Equals(""))
            {
                //chuyển hướng web
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/login");
            }
        }
    }
}