using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoriiCoffee.Models;

namespace MoriiCoffee.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        // GET: Admin/Admin
        public ActionResult Dashboard()
        {

            ViewBag.LongNguyen = db.Blogs.Count();
            return View();
        }
    }
}