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
using SelectPdf;
using System.Text;
using System.IO;
using System.Web.UI;
using MoriiCoffee.Models;

namespace MoriiCoffee.Areas.Admin.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: Admin/HoaDon
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private NguoiDungDao nddao = new NguoiDungDao();
        private DatHangDao dhdao = new DatHangDao();
        private HoaDonDao hddao = new HoaDonDao();
        private ChiTietHoaDonDao cthddao = new ChiTietHoaDonDao();
        private ChiTietSanPhamDao ctspdao = new ChiTietSanPhamDao();
        private KhuyenMaiDao kmdao = new KhuyenMaiDao();

        private const string CartSession = "CartSession";

        public ActionResult DanhSachHoaDon(string searchString)
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
            var model = hddao.ListAllPaging(searchString);
            ViewBag.dathangs = dhdao.ViewAll();
            ViewBag.hoadons = model;
            ViewBag.nds = nddao.ViewAll();
            return View();
        }


        [ValidateInput(false)]
        public ActionResult ChiTietHoaDon(long id)
        {
            var isKM = true;
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
            var hd = hddao.ViewDetail(id);
            var dh = dhdao.ViewDetailByMaHD(id);
            var km = kmdao.ViewDetail(hd.MaKM);
            if (km.TenKM == "Không Khuyến Mãi" && km.ID == 1)
            {
                isKM = false;
            }
            var listCTHD = new List<ChiTietHoaDon>();
            listCTHD = cthddao.ViewAllByID(id);
            var listCTSP = ctspdao.ViewAll();

            ViewBag.km = km;
            ViewBag.isKM = isKM;
            ViewBag.isOnline = hd.IsOnline;
            ViewBag.dh = dh;
            ViewBag.hd = hd;
            ViewBag.listCTHD = listCTHD;
            ViewBag.listCTSP = listCTSP;
            ViewBag.listND = nddao.ViewAll();
            return View();
        }


        public ActionResult ExportPdf(long id)
        {
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 10;
            converter.Options.MarginRight = 10;
            converter.Options.MarginTop = 20;
            converter.Options.MarginBottom = 20;

            var kms = kmdao.ViewAll();

            var isOnline = true;
            long maKM = 1;
            string tenKM = "";
            decimal tienKM = 0;
            var hd = hddao.ViewDetail(id);
            decimal tongTien = 0;
            //Kiểm tra xem mua hàng online hay offline
            if(hd.IsOnline != null)
            {
                isOnline = hd.IsOnline.GetValueOrDefault();
            }
            if(hd.MaKM != null)
            {
                maKM = hd.MaKM;
                foreach(var it in kms)
                {
                    if(it.ID == maKM)
                    {
                        tenKM = it.TenKM;
                    }
                }
            }

            if(hd.TongTien != null)
            {
                tongTien = hd.TongTien.GetValueOrDefault();
            }

            if (hd.TienKM != null)
            {
                tienKM = hd.TienKM.GetValueOrDefault();
            }

            //Truyền Moel cho CTHD
            var modelCTHD = cthddao.ViewAllByID(id);
            

            List<InvoiceExport> ies = new List<InvoiceExport>();
            foreach(var entity in modelCTHD)
            {
                InvoiceExport ie = new InvoiceExport();
                ie.ID = entity.ID;
                var ctsp = ctspdao.ViewDetail(entity.MaSP.GetValueOrDefault());
                ie.TenSP = ctsp.TenSanPham;
                ie.Size = entity.Size;
                ie.Topping = entity.Topping;
                ie.Gia = entity.Gia;
                ie.SoLuong = entity.SoLuong;
                ie.ThanhTien = entity.ThanhTien;
                ie.IDHoaDon = entity.IDHoaDon;
                ie.CreatedDate = entity.CreatedDate;
                ie.TienKM = tienKM;
                ie.TenKM = tenKM;
                ie.isOnline = isOnline;
                ie.TongTien = tongTien;
                ies.Add(ie);
            }

            var modelIes = ies;
            var htmlPdf = RenderPartialToString("~/Areas/Admin/Views/HoaDon/ParticalViewPdfResult.cshtml", modelIes, ControllerContext);
            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString(htmlPdf);

            string fileName = string.Format("{0}.pdf", DateTime.Now.Ticks);
            string pathFile = string.Format("{0}/{1}", Server.MapPath("~/Resource/Pdf"), fileName);


            // save pdf document
            doc.Save(pathFile);

            // close pdf document
            doc.Close();
            return Redirect("~/admin/hoa-don");

        }

        public static string RenderPartialToString(string viewName, object model, ControllerContext ControllerContext)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            ViewDataDictionary ViewData = new ViewDataDictionary();
            TempDataDictionary TempData = new TempDataDictionary();
            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }

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
            
            //Nếu tất cả các case đều hợp lệ, Ta sẽ tiến hành cập nhật dữ liệu và CSDL
            if (isValid == true)
            {
                //Thêm hóa đơn mới
                HoaDon hd = new HoaDon();
                hd.TongTien = 0;
                hd.IsOnline = true;
                hd.MaNV = id;
                var idhd = hddao.Insert(hd);
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

                    //Sau khi thêm tất cả các sản phẩm đặt vào chi tiết hóa đơn, ta tiến hành cập nhật tổng tiền
                    if (hd.TongTien > 0)
                    {
                        //Thêm 30 nghìn phí ship
                        hddao.UpdateTongTien(hd);

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
                errMsg = errMsg,

            });
        }

        

    }
}