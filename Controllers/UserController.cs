using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoriiCoffee.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult DangKy()
        {
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        public ActionResult QuenMatKhau()
        {
            return View();
        }
    }
}