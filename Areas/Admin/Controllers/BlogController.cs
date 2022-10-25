using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoriiCoffee.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        // GET: Admin/Blog
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                var dao = new BlogDao();
                long id = dao.Insert(blog);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Blog");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm thất bại");
                }
            }
            return View();

        }
    }
}