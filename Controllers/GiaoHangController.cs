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
        public JsonResult DatGiaoJson(long id, string hoTen, string sdt, string diaChi, string ghiChu, string pttt)
        {

            var cart = Session[CartSession];
                var list = (List<CartItem>)cart;
            //if (cart != null)
            //{
                //if (list.Exists(x => x.ChiTietSanPham.ID == id))
                //{
                //    var flag = false;
                //    foreach (var item in list)
                //    {
                //        if (item.ChiTietSanPham.ID == id && item.Size == size && item.Topping == topping && item.Gia == gia)
                //        {
                //            item.Quantity += 1;
                //            flag = true;
                //        }
                //    }
                //    if (!flag)
                //    {
                //        var item = new CartItem();
                //        item.ChiTietSanPham = sanpham;
                //        item.Quantity = 1;
                //        item.Gia = gia;
                //        item.Topping = topping;
                //        item.Size = size;
                //        list.Add(item);
                //    }
                //}
                //else
                //{
                //    var item = new CartItem();
                //    item.ChiTietSanPham = sanpham;
                //    item.Quantity = 1;
                //    item.Gia = gia;
                //    item.Topping = topping;
                //    item.Size = size;
                //    list.Add(item);
                //}
                //Session[CartSession] = list;
            //}
            //else
            //{
            //    //Tạo mới đối tượng CartItem
            //    var item = new CartItem();
            //    item.ChiTietSanPham = sanpham;
            //    item.Quantity = 1;
            //    item.Gia = gia;
            //    item.Topping = topping;
            //    item.Size = size;
            //    var list = new List<CartItem>();
            //    list.Add(item);
            //    //Gán vào Session
            //    Session[CartSession] = list;
            //}
            return Json(new
            {
                status = true,
                listCart = list,
                id = id,
                hoTen = hoTen,
                sdt = sdt,
                diaChi = diaChi,
                ghiChu = ghiChu,
                pttt = pttt,
                
            }); 
        }

    }
}