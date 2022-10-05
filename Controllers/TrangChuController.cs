﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoriiCoffee.Models;

namespace MoriiCoffee.Controllers
{
    public class TrangChuController : Controller
    {
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        // GET: TrangChu
        public ActionResult Index()
        {
            ViewBag.SoMauTin = db.NguoiDungs.Count();
            return View();
        }
    }
}