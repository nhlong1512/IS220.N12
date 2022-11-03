
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
            var blogs = bldao.ViewAll();
            ViewBag.blogs = blogs;
            
                
            return View();
        }
        public ActionResult Blog()
        {
            
            return View();
        }


    }
}