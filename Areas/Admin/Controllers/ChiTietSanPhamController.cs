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
    public class ChiTietSanPhamController : Controller
    {
        // GET: Admin/ChiTietSanPham
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private SanPham sp = new SanPham();
        private SanPhamDao spdao = new SanPhamDao();
        private NguoiDungDao nguoidungdao = new NguoiDungDao();
        private ChiTietSanPham ctsp = new ChiTietSanPham();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            //var blogs = bldao.ViewAll();
            //ViewBag.blogs = blogs;
            ViewBag.sanphams = spdao.ViewAll();
            var model = ctspdao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult Create(ChiTietSanPham ctsp)
        {
            ViewBag.sanpham = spdao.ViewAll();
            if (ModelState.IsValid)
            {
                ctsp.Status = true;
                var id = ctspdao.Insert(ctsp);

                if (id > 0)
                {
                    return RedirectToAction("Index", "ChiTietSanPham");
                }
                return View("Create");

            }
            else
            {

            }
            return View("Create");
        }

        public ActionResult Details(long id)
        {
            var ctsp = ctspdao.ViewDetail(id);

            return View(ctsp);
        }

        public ActionResult Delete(long id)
        {
            ctspdao.Delete(id);
            return RedirectToAction("Index");
        }


        [ValidateInput(false)]
        public ActionResult Update(long id)
        {
            var ctsp = ctspdao.ViewDetail(id);
            return View(ctsp);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(ChiTietSanPham ctsp)
        {
            var isTrue = ctspdao.Update(ctsp);
            if (isTrue)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Không lưu được vào CSDL");
                return View(ctsp);
            }
        }
    }
}