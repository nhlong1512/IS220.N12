using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.EF;
using System.Threading.Tasks;

namespace Model.Dao
{
    class ChiTietHoaDonDao
    {
        MoriiCoffeeDBContext db = null;

        public ChiTietHoaDonDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết hóa đơn
        public ChiTietHoaDon ViewDetail(int id)
        {
            return db.ChiTietHoaDons.Find(id);
        }

        //Thêm chi tiết hóa đơn
        public long Insert(ChiTietHoaDon entity)
        {
            db.ChiTietHoaDons.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //Update chi tiết hóa đơn
        public bool Update(ChiTietHoaDon entity)
        {
            try
            {
                var cthd = db.ChiTietHoaDons.Find(entity.ID);
                cthd.MaSP = entity.MaSP;
                cthd.SoLuong = entity.SoLuong;
                cthd.ThanhTien = entity.ThanhTien;
                cthd.ModifiedBy = entity.ModifiedBy;
                cthd.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa chi tiết hóa đơn
        public bool Delete(int id)
        {
            try
            {
                var nd = db.ChiTietHoaDons.Find(id);
                db.ChiTietHoaDons.Remove(nd);
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
