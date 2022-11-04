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
    public class SanPhamController : Controller
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
    }
}