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
        private HoaDonDao hddao = new HoaDonDao();
        //private HoaDon hd = new HoaDon();
        private ChiTietHoaDonDao cthddao = new ChiTietHoaDonDao();
        //private ChiTietHoaDon cthd = new ChiTietHoaDon();
        private DatHangDao dhdao = new DatHangDao();
        //private DatHang dh = new DatHang();

        private const string CartSession = "CartSession";
        private KhuyenMaiDao kmdao = new KhuyenMaiDao();
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
            //Khuyến mãi
            var km = kmdao.ViewDetailKhuyenMaiTrue();
            //Nếu là không khuyến mãi thì không thay đồi gì trong giao diện giỏ hàng ngược lại thì sẽ thêm tên khuyến mãi và handle trừ tiền khuyến mãi
            if (km.TenKM == "Không Khuyến Mãi" && km.ID == 1)
            {
                ViewBag.isKM = false;
                ViewBag.km = km;
            }
            else
            {
                ViewBag.isKM = true;
                ViewBag.km = km;
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
            
            //Nếu tất cả các case đều hợp lệ, Ta sẽ tiến hành cập nhật dữ liệu và CSDL
            if(isValid == true)
            {
                //Thêm hóa đơn mới
                HoaDon hd = new HoaDon();
                hd.TongTien = 0;
                hd.IsOnline = true;
                hd.MaKH = id;
                var idhd = hddao.Insert(hd);
                //Kiểm tra nếu thêm được thì tiếp tục thêm dữ liệu cho bảng CTHD ngược lại thì không làm gì cả
                if (idhd > 0)
                {
                    foreach(var item in list)
                    {
                        //Thêm các sản phẩm đặt vào chi tiết hóa đơn
                        ChiTietHoaDon cthd = new ChiTietHoaDon();
                        cthd.MaSP = item.ChiTietSanPham.ID;
                        cthd.Size = item.Size;
                        cthd.Topping = item.Topping;
                        cthd.Gia = item.Gia;
                        cthd.SoLuong = item.Quantity;
                        cthd.ThanhTien = item.Quantity * item.Gia;
                        cthd.IDHoaDon = idhd;
                        var idcthd = cthddao.Insert(cthd);
                        if(idcthd <= 0)
                        {
                            isValid = false;
                        }
                        hd.TongTien += cthd.ThanhTien;
                    }

                    //Sau khi thêm tất cả các sản phẩm đặt vào chi tiết hóa đơn, ta tiến hành cập nhật tổng tiền
                    var km = kmdao.ViewDetailKhuyenMaiTrue();
                    if (km != null)
                    {
                        hd.MaKM = km.ID;
                    }

                    if (km.TenKM == "Không Khuyến Mãi" && km.ID == 1)
                    {
                        if (hd.TongTien > 0)
                        {
                            hd.TienKM = 0;
                            //Thêm 30 nghìn phí ship
                            hd.TongTien = hd.TongTien + 30000;
                            hddao.UpdateTongTien(hd);

                        }
                    }else
                    {
                        if (hd.TongTien > 0)
                        {
                            //Thêm 30 nghìn phí ship
                            var ttkm = hd.TongTien / 1000;
                            ttkm = ttkm * km.PhanTramKM / 100;
                            ttkm = Math.Floor((decimal)ttkm);
                            hd.TienKM = ttkm * 1000;


                            //Thêm 30 nghìn phí ship
                            hd.TongTien = hd.TongTien - ttkm * 1000 + 30000;
                            hddao.UpdateTongTien(hd);
                        }
                    }
                    
                    //Sau khi cập nhật tổng tiền cho phần Hóa đơn, ta tiến hành thêm thông tin cho Đặt hàng
                    DatHang dh = new DatHang();
                    dh.MaKH = id;
                    dh.HoTen = hoTen;
                    dh.SDT = sdt;
                    dh.Email = email;
                    dh.DiaChiNhanHang = diaChi;
                    dh.GhiChu = ghiChu;
                    dh.PTTT = pttt;
                    dh.TTDH = "Chờ Xác Nhận";
                    dh.MaHoaDon = idhd;
                    var iddh = dhdao.Insert(dh);
                    //Nếu không thêm được isValid sẽ là False
                    if(iddh <= 0)
                    {
                        isValid = false;
                    }
                }
                else
                {
                    isValid = false;
                }
            }

            //Kiểm tra nếu tất cả đã hoàn thành và hợp lệ rồi thì sẽ đưa session về null
            if(isValid == true)
            {
                Session["CartSession"] = null;
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


        //Handle phương thức đặt giao trong phần giao hàng, khi ấn đặt giao và không xảy ra ngoại lệ nào
        //Các dữ liệu sẽ được đổ hết vào 3 bảng ChiTietHoaDon, HoaDon và Bảng DatHang

        [HttpPost]
        public JsonResult ThanhToanHoaDonJson(long id)
        {

            var cart = Session[CartSession];
            var list = (List<CartItem>)cart;
            var isValid = true;
            var errMsg = "";
            long idhd = 0;

            //Nếu tất cả các case đều hợp lệ, Ta sẽ tiến hành cập nhật dữ liệu và CSDL
            if (isValid == true)
            {
                //Thêm hóa đơn mới
                HoaDon hd = new HoaDon();
                hd.TongTien = 0;
                hd.IsOnline = false;
                hd.MaNV = id;

                idhd = hddao.Insert(hd);
                //Kiểm tra nếu thêm được thì tiếp tục thêm dữ liệu cho bảng CTHD ngược lại thì không làm gì cả
                if (idhd > 0)
                {
                    foreach (var item in list)
                    {
                        //Thêm các sản phẩm đặt vào chi tiết hóa đơn
                        ChiTietHoaDon cthd = new ChiTietHoaDon();
                        cthd.MaSP = item.ChiTietSanPham.ID;
                        cthd.Size = item.Size;
                        cthd.Topping = item.Topping;
                        cthd.Gia = item.Gia;
                        cthd.SoLuong = item.Quantity;
                        cthd.ThanhTien = item.Quantity * item.Gia;
                        cthd.IDHoaDon = idhd;
                        var idcthd = cthddao.Insert(cthd);
                        if (idcthd <= 0)
                        {
                            isValid = false;
                        }
                        hd.TongTien += cthd.ThanhTien;
                    }
                    var km = kmdao.ViewDetailKhuyenMaiTrue();
                    if(km != null)
                    {
                        hd.MaKM = km.ID;
                    }

                    if (km.TenKM == "Không Khuyến Mãi" && km.ID == 1)
                    {
                        if (hd.TongTien > 0)
                        {
                            hd.TienKM = 0;
                            hddao.UpdateTongTien(hd);

                        }
                    }
                    else
                    {
                        if (hd.TongTien > 0)
                        {
                            var ttkm = hd.TongTien / 1000;
                            ttkm = ttkm * km.PhanTramKM / 100;
                            ttkm = Math.Floor((decimal)ttkm);
                            hd.TienKM = ttkm * 1000;


                            //Thêm 30 nghìn phí ship
                            hd.TongTien = hd.TongTien - ttkm * 1000;
                            hddao.UpdateTongTien(hd);
                        }
                    }

                }
                else
                {
                    isValid = false;
                }
            }

            //Kiểm tra nếu tất cả đã hoàn thành và hợp lệ rồi thì sẽ đưa session về null
            if (isValid == true)
            {
                Session["CartSession"] = null;
            }


            return Json(new
            {
                isValid = isValid,
                listCart = list,
                id = id,
                idhd = idhd,
                errMsg = errMsg,

            });
        }

    }
}