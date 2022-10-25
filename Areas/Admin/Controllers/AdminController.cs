
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
        // GET: Admin/Admin
        public ActionResult Dashboard()
        {

            ViewBag.LongNguyen = db.Blogs.Count();
            return View();
        }
        public ActionResult Blog()
        {
            
            return View();
        }


    }
}