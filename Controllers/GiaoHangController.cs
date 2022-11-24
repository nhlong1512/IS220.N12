using Model.Dao;
using Model.EF;
using MoriiCoffee.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoriiCoffee.Controllers
{
    public class GiaoHangController : BaseController
    {
        // GET: GiaoHang

        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private NguoiDungDao nddao = new NguoiDungDao();
        private const string CartSession = "CartSession";
        //ViewAction Giao diện Giao Hàng
        public ActionResult GiaoHang()
        {
            if (ModelState.IsValid)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION];

                if (!(session is null))
                {
                    ViewBag.session = session;
                    var ndd = nddao.ViewDetailEmail(session.UserName);
                    ViewBag.ndd = ndd;
                }
            }
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;

                var cartQtySession = list.Count();
                ViewBag.cartQtySession = cartQtySession;
            }
            return View(list);
        }


        //Handle phương thức đặt giao trong phần giao hàng, khi ấn đặt giao và không xảy ra ngoại lệ nào
        //Các dữ liệu sẽ được đổ hết vào 3 bảng ChiTietHoaDon, HoaDon và Bảng DatHang

        [HttpPost]
        public JsonResult DatGiaoJson(long id, string hoTen, string sdt, string email, string diaChi, string ghiChu, string pttt)
        {

            var cart = Session[CartSession];
            var list = (List<CartItem>)cart;
            var isValid = true;
            var errMsg = "";
            if (string.IsNullOrEmpty(hoTen))
            {
                isValid = false;
                errMsg += "Vui lòng nhập họ tên. ";
            }
            if (string.IsNullOrEmpty(sdt))
            {
                isValid = false;
                errMsg += "Vui lòng nhập số điện thoại. ";
            }
            if (string.IsNullOrEmpty(email))
            {
                isValid = false;
                errMsg += "Vui lòng nhập Email. ";
            }
            if (string.IsNullOrEmpty(diaChi))
            {
                isValid = false;
                errMsg += "Vui lòng nhập địa chỉ. ";
            }
            if (string.IsNullOrEmpty(pttt))
            {
                isValid = false;
                errMsg += "Vui lòng chọn phương thức thanh toán. ";
            }


            return Json(new
            {
                isValid = isValid,
                listCart = list,
                id = id,
                hoTen = hoTen,
                sdt = sdt,
                email = email,
                diaChi = diaChi,
                ghiChu = ghiChu,
                pttt = pttt,
                errMsg = errMsg,

            }) ; 
        }

    }
}