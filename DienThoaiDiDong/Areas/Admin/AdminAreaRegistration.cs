using System.Web.Mvc;

namespace DienThoaiDiDong.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_login",
                "Admin/login",
                new { Controller = "Login", action = "Auth", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {Controller="Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}