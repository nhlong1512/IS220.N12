using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.EF;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ChiTietHoaDonDao
    {
        MoriiCoffeeDBContext db = null;

        public ChiTietHoaDonDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết hóa đơn
        public ChiTietHoaDon ViewDetail(long id)
        {
            return db.ChiTietHoaDons.Find(id);
        }

        //Xem tất cả ChiTietHoaDon
        public List<ChiTietHoaDon> ViewAll()
        {
            List<ChiTietHoaDon> hoadons = new List<ChiTietHoaDon>();
            if (db.ChiTietHoaDons.Count() == 0)
            {
                return hoadons;
            }
            var list = db.ChiTietHoaDons.Where(p => p.ID > 0);
            //Convert từ IqueryTable sang list
            hoadons = new List<ChiTietHoaDon>(list);
            return hoadons;
        }

        //Xem tất cả ChiTietHoaDon có id 
        public List<ChiTietHoaDon> ViewAllByID(long id)
        {
            List<ChiTietHoaDon> hoadons = new List<ChiTietHoaDon>();
            if (db.HoaDons.Count() == 0)
            {
                return hoadons;
            }
            var list = db.ChiTietHoaDons.Where(p => p.IDHoaDon == id);
            //Convert từ IqueryTable sang list
            hoadons = new List<ChiTietHoaDon>(list);
            return hoadons;
        }

        //Thêm chi tiết hóa đơn
        public long Insert(ChiTietHoaDon entity)
        {
            entity.CreatedDate = DateTime.Now;
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
                cthd.IDHoaDon = entity.IDHoaDon;
                cthd.MaSP = entity.MaSP;
                cthd.Size = entity.Size;
                cthd.Topping = entity.Topping;
                cthd.Gia = entity.Gia;
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
