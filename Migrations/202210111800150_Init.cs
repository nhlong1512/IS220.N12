namespace MoriiCoffee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        MaBL = c.Int(nullable: false, identity: true),
                        TieuDe = c.String(nullable: false),
                        MoTa = c.String(nullable: false),
                        NoiDung = c.String(nullable: false),
                        UrlImage = c.String(nullable: false),
                        MaND = c.Int(),
                        NgayBlog = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MaBL);
            
            CreateTable(
                "dbo.ChiTietHDs",
                c => new
                    {
                        MaHD = c.Int(nullable: false, identity: true),
                        MaSP = c.Int(nullable: false),
                        SoLuong = c.Int(nullable: false),
                        ThanhTien = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaHD);
            
            CreateTable(
                "dbo.ChiTietSPs",
                c => new
                    {
                        MaSP = c.Int(nullable: false, identity: true),
                        TenSP = c.String(nullable: false),
                        Gia = c.Int(nullable: false),
                        Size = c.String(nullable: false),
                        MaKM = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaSP);
            
            CreateTable(
                "dbo.CuaHangs",
                c => new
                    {
                        MaCH = c.Int(nullable: false, identity: true),
                        TenCH = c.String(nullable: false),
                        SDT = c.String(nullable: false),
                        MaQuanLy = c.Int(nullable: false),
                        DiaChi = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MaCH);
            
            CreateTable(
                "dbo.DatHangs",
                c => new
                    {
                        MaDH = c.Int(nullable: false, identity: true),
                        MaHD = c.Int(nullable: false),
                        TrangThai = c.String(nullable: false),
                        DiaChiNH = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MaDH);
            
            CreateTable(
                "dbo.HoaDons",
                c => new
                    {
                        MaHD = c.Int(nullable: false, identity: true),
                        MaNV = c.Int(nullable: false),
                        MaKH = c.Int(nullable: false),
                        MaCH = c.Int(nullable: false),
                        NgayHD = c.DateTime(nullable: false),
                        TongTien = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaHD);
            
            CreateTable(
                "dbo.KhachHangs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.KhuyenMais",
                c => new
                    {
                        MaKM = c.Int(nullable: false, identity: true),
                        PhanTramKM = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaKM);
            
            CreateTable(
                "dbo.NguoiDungs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HoTen = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        SDT = c.String(),
                        Role = c.String(),
                        NgDK = c.DateTime(),
                        NgSinh = c.DateTime(),
                        GioiTinh = c.String(),
                        UrlAvt = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NhanViens",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Luong = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuanLys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        MaSP = c.Int(nullable: false, identity: true),
                        PhanLoai = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MaSP);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SanPhams");
            DropTable("dbo.QuanLys");
            DropTable("dbo.NhanViens");
            DropTable("dbo.NguoiDungs");
            DropTable("dbo.KhuyenMais");
            DropTable("dbo.KhachHangs");
            DropTable("dbo.HoaDons");
            DropTable("dbo.DatHangs");
            DropTable("dbo.CuaHangs");
            DropTable("dbo.ChiTietSPs");
            DropTable("dbo.ChiTietHDs");
            DropTable("dbo.Blogs");
        }
    }
}
