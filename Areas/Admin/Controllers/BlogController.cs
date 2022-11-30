using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using MoriiCoffee.Controllers;
using MoriiCoffee.Common;

namespace MoriiCoffee.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        // GET: Admin/Blog
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private Blog bl = new Blog();
        private BlogDao bldao = new BlogDao();
        private NguoiDungDao nguoidungdao = new NguoiDungDao();
        public ActionResult DanhSachBlog(string searchString, int page = 1, int pageSize = 5)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
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

            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }

            if (ModelState.IsValid)
            {
                var id = bldao.Insert(blog);
                if (id > 0)
                {
                    return Redirect("/admin/blog");
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
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }

            var blog = bldao.ViewDetail(id);

            return View(blog);
        }

        public ActionResult Delete(long id)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }

            bldao.Delete(id);
            return Redirect("/admin/blog");
        }


        [ValidateInput(false)]
        public ActionResult Update(long id)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }

            var blog = bldao.ViewDetail(id);
            return View(blog);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Blog blog)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }

            var isTrue =  bldao.Update(blog);
            if(isTrue)
            {
                return Redirect("/admin/blog");
            }
            else
            {
                ModelState.AddModelError("", "Không lưu được vào CSDL");
                return View(blog);
            }
        }
    }
}