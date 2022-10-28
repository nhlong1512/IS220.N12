
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;


namespace MoriiCoffee.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private Blog bl = new Blog();
        private BlogDao bldao = new BlogDao();
        // GET: Admin/Admin
        public ActionResult Dashboard()
        {
            try
            {
                foreach(Blog bl in bldao.ViewAll())
                {
                    if (bl.ID == 3) ViewBag.Long = bl.TieuDe;
                }
                
            }catch(Exception ex)
            {
                ViewBag.Long = "Falsseeeee";
            }
                
            return View();
        }
        public ActionResult Blog()
        {
            
            return View();
        }


    }
}