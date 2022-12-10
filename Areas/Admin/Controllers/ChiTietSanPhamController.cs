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
    public class ChiTietSanPhamController : Controller
    {
        // GET: Admin/ChiTietSanPham
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private SanPham sp = new SanPham();
        private SanPhamDao spdao = new SanPhamDao();
        private NguoiDungDao nddao = new NguoiDungDao();
        private ChiTietSanPham ctsp = new ChiTietSanPham();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();


        public ActionResult DanhSachSanPham(string searchString, int page = 1, int pageSize = 5)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nd = nddao.ViewDetailEmail(session.UserName);
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }
            var model = ctspdao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;

            var ctsps = ctspdao.ViewAll();
            var sps = spdao.ViewAll();
            ViewBag.ctsps = ctsps;
            ViewBag.sps = sps;

            return View(model);
        }

        public ActionResult DanhSachCaPhe(string searchString, int page = 1, int pageSize = 5)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nd = nddao.ViewDetailEmail(session.UserName);
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }
            var model = ctspdao.ListAllPagingCaPhe(searchString, page, pageSize);
            ViewBag.SearchString = searchString;

            var ctsps = ctspdao.ViewAll();
            var sps = spdao.ViewAll();
            ViewBag.ctsps = ctsps;
            ViewBag.sps = sps;

            return View(model);
        }

        public ActionResult DanhSachTraSua(string searchString, int page = 1, int pageSize = 5)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nd = nddao.ViewDetailEmail(session.UserName);
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }
            var model = ctspdao.ListAllPagingTraSua(searchString, page, pageSize);
            ViewBag.SearchString = searchString;

            var ctsps = ctspdao.ViewAll();
            var sps = spdao.ViewAll();
            ViewBag.ctsps = ctsps;
            ViewBag.sps = sps;

            return View(model);
        }

        public ActionResult DanhSachTopping(string searchString, int page = 1, int pageSize = 5)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nd = nddao.ViewDetailEmail(session.UserName);
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }
            var model = ctspdao.ListAllPagingTopping(searchString, page, pageSize);
            ViewBag.SearchString = searchString;

            var ctsps = ctspdao.ViewAll();
            var sps = spdao.ViewAll();
            ViewBag.ctsps = ctsps;
            ViewBag.sps = sps;

            return View(model);
        }

        public ActionResult DanhSachKhac(string searchString, int page = 1, int pageSize = 5)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nd = nddao.ViewDetailEmail(session.UserName);
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }
            var model = ctspdao.ListAllPagingKhac(searchString, page, pageSize);
            ViewBag.SearchString = searchString;

            var ctsps = ctspdao.ViewAll();
            var sps = spdao.ViewAll();
            ViewBag.ctsps = ctsps;
            ViewBag.sps = sps;

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult Create(ChiTietSanPham ctsp)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nd = nddao.ViewDetailEmail(session.UserName);
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }

            ViewBag.sanpham = spdao.ViewAll();
            if (ModelState.IsValid)
            {
                ctsp.Status = true;
                var id = ctspdao.Insert(ctsp);

                if (id > 0)
                {
                    return Redirect("/admin/san-pham");
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
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nd = nddao.ViewDetailEmail(session.UserName);
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }

            var ctsp = ctspdao.ViewDetail(id);
            ViewBag.ctsps = ctspdao.ViewAll();
            return View(ctsp);
        }

        public ActionResult Delete(long id)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nd = nddao.ViewDetailEmail(session.UserName);
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }

            ctspdao.Delete(id);
            return Redirect("~/admin/san-pham");
        }


        [ValidateInput(false)]
        public ActionResult Update(long id)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nd = nddao.ViewDetailEmail(session.UserName);
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }

            ViewBag.sanpham = spdao.ViewAll();
            var ctsp = ctspdao.ViewDetail(id);
            return View(ctsp);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(ChiTietSanPham ctsp)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nd = nddao.ViewDetailEmail(session.UserName);
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }

            var isTrue = ctspdao.Update(ctsp);
            if (isTrue)
            {
                return RedirectToAction("DanhSachSanPham");
            }
            else
            {
                ModelState.AddModelError("", "Không lưu được vào CSDL");
                return View(ctsp);
            }
        }



    }
}