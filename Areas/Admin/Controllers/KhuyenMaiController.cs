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
    public class KhuyenMaiController : Controller
    {
        // GET: Admin/KhuyenMai
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private SanPham sp = new SanPham();
        private SanPhamDao spdao = new SanPhamDao();
        private NguoiDungDao nddao = new NguoiDungDao();
        private KhuyenMaiDao kmdao = new KhuyenMaiDao();

        public ActionResult DanhSachKhuyenMai()
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
            var kms = kmdao.ViewAll();
            ViewBag.kms = kms;
            return View();
        }

        [HttpPost]
        public JsonResult ApDungKhuyenMaiJson(long selectVal)
        {
            var km = kmdao.ViewDetail(selectVal);
            var kms = kmdao.ViewAll();
            var isValid = true;
            
            
            if(selectVal > 0)
            {
                foreach (var item in kms)
                {
                    if (item.ID == selectVal)
                    {
                        item.Status = true;
                        kmdao.Update(item);
                    }
                    else
                    {
                        item.Status = false;
                        kmdao.Update(item);
                    }
                }
            }
            else
            {
                isValid = false;
            }


            return Json(new
            {
                isValid = isValid,
                errMsg = "Áp Dụng Thất Bại. ",

            });
        }
    }
}