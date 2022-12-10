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
                if (nd.Role != "Nhân viên" && nd.Role != "ADMIN")
                {
                    return Redirect("/");
                }
                ViewBag.ndd = nd;
            }
            var kms = kmdao.ViewAll();
            ViewBag.kms = kms;
            return View();
        }


        //Handle áp dụng khuyến mãi json
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

            });
        }


        //Handle Xóa khuyến mãi
        [HttpPost]
        public JsonResult XoaKhuyenMaiJson(long selectVal)
        {
            var km = kmdao.ViewDetail(selectVal);
            var isGanLai = false;
            
            var kms = kmdao.ViewAll();
            var isValid = true;
            if(km.Status == true)
            {
                isGanLai = true;
            }

            if (selectVal > 0)
            {
                foreach (var item in kms)
                {
                    if (item.ID == selectVal)
                    {
                        kmdao.Delete(item.ID);
                    }
                }
            }
            else
            {
                isValid = false;
            }

            if(isGanLai == true)
            {
                foreach (var item in kms)
                {
                    item.Status = true;
                    kmdao.Update(item);
                    break;
                }
            }

            return Json(new
            {
                isValid = isValid,

            });
        }


        //Handle thêm khuyến mãi
        [HttpPost]
        public JsonResult ThemKhuyenMaiJson(string tenKhuyenMai, int ptKhuyenMai)
        {
            var isValid = true;
            KhuyenMai km = new KhuyenMai();
            km.TenKM = tenKhuyenMai;
            km.PhanTramKM = ptKhuyenMai;
            km.Status = false;
            if(km.TenKM != null && km.PhanTramKM != null)
            {
                var idkm = kmdao.Insert(km);
                if(idkm > 0)
                {
                    
                }
                else
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }

            return Json(new
            {
                tenKhuyenMai = tenKhuyenMai,
                ptKhuyenMai = ptKhuyenMai,
                isValid = isValid,

            });
        }
    }
}