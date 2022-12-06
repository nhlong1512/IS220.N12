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

namespace MoriiCoffee.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private NguoiDungDao nddao = new NguoiDungDao();
        private const string CartSession = "CartSession";
        private KhuyenMaiDao kmdao = new KhuyenMaiDao();

        public ActionResult GioHang()
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
            if(cart != null)
            {
                list = (List<CartItem>)cart;

                var cartQtySession = list.Count();
                ViewBag.cartQtySession = cartQtySession;
            }

            //Khuyến mãi
            var km = kmdao.ViewDetailKhuyenMaiTrue();
            //Nếu là không khuyến mãi thì không thay đồi gì trong giao diện giỏ hàng ngược lại thì sẽ thêm tên khuyến mãi và handle trừ tiền khuyến mãi
            if(km.TenKM == "Không Khuyến Mãi" && km.ID == 1)
            {
                ViewBag.isKM = false;
                ViewBag.km = km;
            }else
            {
                ViewBag.isKM = true;
                ViewBag.km = km;
            }

            return View(list);
        }

        public ActionResult ThemGioHang(long productID, int quantity)
        {
            var sanpham = new ChiTietSanPhamDao().ViewDetail(productID);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.ChiTietSanPham.ID == productID))
                {
                    foreach (var item in list)
                    {
                        if (item.ChiTietSanPham.ID == productID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.ChiTietSanPham = sanpham;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                //Tạo mới đối tượng CartItem
                var item = new CartItem();
                item.ChiTietSanPham = sanpham;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào Session
                Session[CartSession] = list;
            }

            return Redirect("/gio-hang");
        }


        //Handle phương thức thêm giỏ hàng trong mục deltail sản phẩm
        [HttpPost]
        public JsonResult ThemGioHangJson(long id, string size, string topping, long gia)
        {

            var sanpham = new ChiTietSanPhamDao().ViewDetail(id);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.ChiTietSanPham.ID == id))
                {
                    var flag = false;
                    foreach (var item in list)
                    {
                        if (item.ChiTietSanPham.ID == id && item.Size == size && item.Topping == topping && item.Gia == gia)
                        {
                            item.Quantity += 1;
                            flag = true;
                        }
                    }
                    if(!flag)
                    {
                        var item = new CartItem();
                        item.ChiTietSanPham = sanpham;
                        item.Quantity = 1;
                        item.Gia = gia;
                        item.Topping = topping;
                        item.Size = size;
                        list.Add(item);
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.ChiTietSanPham = sanpham;
                    item.Quantity = 1;
                    item.Gia = gia;
                    item.Topping = topping;
                    item.Size = size;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                //Tạo mới đối tượng CartItem
                var item = new CartItem();
                item.ChiTietSanPham = sanpham;
                item.Quantity = 1;
                item.Gia = gia;
                item.Topping = topping;
                item.Size = size;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào Session
                Session[CartSession] = list;
            }
            return Json(new
            {
                status = true,
                id = id, 
                size = size,
                topping = topping,
                gia = gia
            });
        }

        //Handle tăng quantity trong giỏ hàng
        [HttpPost]
        public JsonResult IncreaseQtyJson(long id, string size, string topping, long gia)
        {
            //Update Qty cho Session
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.ChiTietSanPham.ID == id))
                {
                    var flag = false;
                    foreach (var item in list)
                    {
                        if (item.ChiTietSanPham.ID == id && item.Size == size && item.Topping == topping && item.Gia == gia)
                        {
                            item.Quantity += 1;
                            flag = true;
                        }
                    }
                    
                }
                
                Session[CartSession] = list;
            }


            return Json(new
            {
                status = true,
                id = id,
                size = size,
                topping = topping,
                gia = gia
            });
        }

        //Handle giảm quantity trong giỏ hàng
        [HttpPost]
        public JsonResult DecreaseQtyJson(long id, string size, string topping, long gia)
        {
            //Update Qty cho Session
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.ChiTietSanPham.ID == id))
                {
                    var flag = false;
                    foreach (var item in list)
                    {
                        if (item.ChiTietSanPham.ID == id && item.Size == size && item.Topping == topping && item.Gia == gia)
                        {
                            item.Quantity -= 1;
                            flag = true;
                        }
                    }

                }
                Session[CartSession] = list;
            }


            return Json(new
            {
                status = true,
                id = id,
                size = size,
                topping = topping,
                gia = gia
            });
        }


        //Handle Xóa CartItem trong giỏ hàng
        [HttpPost]
        public JsonResult DeleteItemJson(long id, string size, string topping, long gia)
        {
            //Duyệt mảng, delete CartItem
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.ChiTietSanPham.ID == id))
                {
                    var flag = false;
                    foreach (var item in list)
                    {
                        if (item.ChiTietSanPham.ID == id && item.Size == size && item.Topping == topping && item.Gia == gia)
                        {
                            list.Remove(item);
                            flag = true;
                        }
                        if(flag==true)
                        {
                            break;
                        }
                    }

                }
                Session[CartSession] = list;
            }


            return Json(new
            {
                status = true,
            });
        }

        
        


        
    }
}