using System.Web.Mvc;

namespace MoriiCoffee.Areas.Admin
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
               name: "Dashboard",
               url: "admin/dashboard",
               defaults: new { controller = "Admin", action = "Dashboard", id = UrlParameter.Optional }
           );



            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {Controller="Admin", action = "Dashboard", id = UrlParameter.Optional }
            );
        }
    }
}