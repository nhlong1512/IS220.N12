using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MoriiCoffee.Models
{
    public class MoriiCoffeeDBContext:DbContext
    {
        public MoriiCoffeeDBContext() : base("name=ChuoiKN") { }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<QuanLy> QuanLys { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<ChiTietSP> ChiTietSPs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHD> ChiTietHDs { get; set; }
        public DbSet<CuaHang> CuaHangs { get; set; }
        public DbSet<DatHang> DatHangs { get; set; }
        public DbSet<Blog> Blogs { get; set; }

    }
}