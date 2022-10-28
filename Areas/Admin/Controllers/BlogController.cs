﻿using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoriiCoffee.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        // GET: Admin/Blog
        private MoriiCoffeeDBContext db = new MoriiCoffeeDBContext();
        private Blog bl = new Blog();
        private BlogDao bldao = new BlogDao();
        private NguoiDungDao nguoidungdao = new NguoiDungDao();
        public ActionResult Index()
        {
            var blogs = bldao.ViewAll();
            ViewBag.blogs = blogs;
            var nguoidung = nguoidungdao.ViewDetail(1);
            ViewBag.nguoidung = nguoidung;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Blog blog)
        {
            
            return View();

        }
    }
}