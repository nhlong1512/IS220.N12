
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;
using MoriiCoffee.Common;
using MoriiCoffee.Controllers;

namespace MoriiCoffee.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private Blog bl = new Blog();
        private BlogDao bldao = new BlogDao();
        private NguoiDungDao nddao = new NguoiDungDao();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();
        private HoaDonDao hddao = new HoaDonDao();

        // GET: Admin/Admin
        public ActionResult Dashboard()
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
            var nds = nddao.ViewAll();
            ViewBag.nds = nds;
            ViewBag.ndsQty = nds.Count();
            ViewBag.ctspsQty = ctspdao.ViewAll().Count();
            var hds = hddao.ViewAll();
            ViewBag.tongTienHD = hds.Sum(p => p.TongTien);
            ViewBag.hdsQty = hds.Count();

            decimal doanhThuOnline = 0;
            decimal doanhThuTrucTiep = 0;
            foreach (var item in hds)
            {
                if (item.IsOnline == true)
                {
                    doanhThuOnline += item.TongTien.GetValueOrDefault();
                }
                else
                {
                    doanhThuTrucTiep += item.TongTien.GetValueOrDefault();
                }
            }

            ViewBag.doanhThuOnline = doanhThuOnline;
            ViewBag.doanhThuTrucTiep = doanhThuTrucTiep;

            ViewBag.doanhThuThang7 = doanhThuThang(7);
            ViewBag.doanhThuThang8 = doanhThuThang(8);
            ViewBag.doanhThuThang9 = doanhThuThang(9);
            ViewBag.doanhThuThang10 = doanhThuThang(10);
            ViewBag.doanhThuThang11 = doanhThuThang(11);
            ViewBag.doanhThuThang12 = doanhThuThang(12);

            return View();
        }


        public int getThang(HoaDon hd)
        {
            var ngayThang = hd.CreatedDate;
            var thang = ngayThang.Value.ToString();

            var thangStr = "";
            foreach (var item in thang)
            {
                if (item == '/')
                {
                    break;
                }
                thangStr = thangStr + item;
            }
            return int.Parse(thangStr);
        }

        public decimal doanhThuThang(int t)
        {
            decimal doanhThu = 0;
            var thang = t;
            var hds = hddao.ViewAll();
            foreach (var item in hds)
            {
                if (getThang(item) == thang)
                {
                    doanhThu += item.TongTien.GetValueOrDefault();
                }
            }
            return doanhThu;

        }

    }
}