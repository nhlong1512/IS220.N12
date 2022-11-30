using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoriiCoffee.Areas.Admin.Controllers
{
    public class DonDatController : Controller
    {
        // GET: Admin/DonDat
        public ActionResult DanhSachDonDat()
        {
            return View();
        }
    }
}