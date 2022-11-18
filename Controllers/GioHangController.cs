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
            return View();
        }
    }
}