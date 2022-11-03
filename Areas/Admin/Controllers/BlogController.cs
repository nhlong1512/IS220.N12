using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MoriiCoffee.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        // GET: Admin/Blog
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private Blog bl = new Blog();
        private BlogDao bldao = new BlogDao();
        private NguoiDungDao nguoidungdao = new NguoiDungDao();
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            //var blogs = bldao.ViewAll();
            //ViewBag.blogs = blogs;
            var nguoidung = nguoidungdao.ViewDetail(1);
            ViewBag.nguoidung = nguoidung;
            var model = bldao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }



        [ValidateInput(false)]
        public ActionResult Create(Blog blog)
        {

            if (ModelState.IsValid)
            {
                var id = bldao.Insert(blog);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Blog");
                }
                return View("Create");

            }
            else
            {

            }
            return View();

        }

        public ActionResult Details(long id)
        {
            var blog = bldao.ViewDetail(id);

            return View(blog);
        }

        public ActionResult Delete(long id)
        {
            bldao.Delete(id);
            return RedirectToAction("Index");
        }


        [ValidateInput(false)]
        public ActionResult Update(long id)
        {
            var blog = bldao.ViewDetail(id);
            return View(blog);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Blog blog)
        {
            var isTrue =  bldao.Update(blog);
            if(isTrue)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Không lưu được vào CSDL");
                return View(blog);
            }
        }
    }
}