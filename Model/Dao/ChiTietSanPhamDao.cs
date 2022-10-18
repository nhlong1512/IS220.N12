using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;


namespace Model.Dao
{
    class ChiTietSanPhamDao
    {
        MoriiCoffeeDBContext db = null;

        public ChiTietSanPhamDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết chi tiết sản phẩm
        public ChiTietSanPham ViewDetail(int id)
        {
            return db.ChiTietSanPhams.Find(id);
        }

        //Thêm Chi tiết sản phẩm
        public long Insert(ChiTietSanPham entity)
        {
            db.ChiTietSanPhams.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //Update Chi tiết sản phẩm
        public bool Update(ChiTietSanPham entity)
        {
            try
            {
                var ctsp = db.ChiTietSanPhams.Find(entity.ID);
                ctsp.TenSanPham = entity.TenSanPham;
                ctsp.Gia = entity.Gia;
                ctsp.GiaCu = entity.GiaCu;
                ctsp.Size = entity.Size;
                ctsp.MaKM = entity.MaKM;
                ctsp.UrlImage = entity.UrlImage;
                ctsp.MoTaSanPham = entity.MoTaSanPham;
                ctsp.ChiTietSanPham1 = entity.ChiTietSanPham1;
                ctsp.ModifiedBy = entity.ModifiedBy;
                ctsp.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa ChiTietSanPham
        public bool Delete(int id)
        {
            try
            {
                var nd = db.ChiTietSanPhams.Find(id);
                db.ChiTietSanPhams.Remove(nd);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
