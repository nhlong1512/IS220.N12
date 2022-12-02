using Model.Dao;
using Model.EF;
using MoriiCoffee.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MoriiCoffee.Areas.Admin.Controllers
{
    public class AdminProfileController : Controller
    {
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private NguoiDungDao nddao = new NguoiDungDao();
        private DatHangDao dhdao = new DatHangDao();
        private HoaDonDao hddao = new HoaDonDao();
        private ChiTietHoaDonDao cthddao = new ChiTietHoaDonDao();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();
        // GET: Admin/Profile
        public ActionResult Index()
        {
            return View();
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
                ViewBag.ndd = nd;
            }
            var ndd = nddao.ViewDetail(id);
            if (ndd.GioiTinh == null)
            {
                ndd.GioiTinh = true;
            }
            return View(ndd);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(NguoiDung nd)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                ViewBag.session = session;
                var nddd = nddao.ViewDetailEmail(session.UserName);
                ViewBag.ndd = nddd;
            }

            var isTrue = nddao.Update(nd);
            if (isTrue)
            {
                var msg = "Cập nhật thông tin thành công. ";
                ViewBag.msg = msg;
                return RedirectToAction("Update", "Profile");
            }
            else
            {
                ModelState.AddModelError("", "Không lưu được vào CSDL");
                return RedirectToAction("Update", "Profile");
            }

        }
    }
}