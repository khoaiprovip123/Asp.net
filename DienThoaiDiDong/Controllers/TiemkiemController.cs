using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
namespace DienThoaiDiDong.Controllers
{
    public class TiemkiemController : Controller
    {
        // GET: Tiemkiem
        

        public ActionResult Search()
        {

            return View("Search");
        }
        //[HttpGet]
      

    }
}