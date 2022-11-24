using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;


namespace Model.Dao
{
    public class DatHangDao
    {
        MoriiCoffeeDBContext db = null;

        public DatHangDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết DatHang
        public DatHang ViewDetail(long id)
        {
            return db.DatHangs.Find(id);
        }

        //Thêm DatHang
        public long Insert(DatHang entity)
        {
            entity.CreatedDate = DateTime.Now;
            db.DatHangs.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //Update DatHang
        public bool Update(DatHang entity)
        {
            try
            {
                var dh = db.DatHangs.Find(entity.ID);
                dh.MaHoaDon = entity.MaHoaDon;
                dh.MaKH = entity.MaKH;
                dh.HoTen = entity.HoTen;
                dh.SDT = entity.SDT;
                dh.Email = entity.Email;
                dh.DiaChiNhanHang = entity.DiaChiNhanHang;
                dh.Tinh = entity.Tinh;
                dh.Quan = entity.Quan;
                dh.Phuong = entity.Phuong;
                dh.GhiChu = entity.GhiChu;
                dh.PTTT = entity.PTTT;
                dh.TTDH = entity.TTDH;
                dh.UrlImage = entity.UrlImage;
                dh.ModifiedBy = entity.ModifiedBy;
                dh.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        

        //Xóa DatHang
        public bool Delete(long id)
        {
            try
            {
                var nd = db.DatHangs.Find(id);
                db.DatHangs.Remove(nd);
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
