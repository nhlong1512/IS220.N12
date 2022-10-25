using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;


namespace Model.Dao
{
    public class HoaDonDao
    {
        MoriiCoffeeDBContext db = null;

        public HoaDonDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết HoaDon
        public HoaDon ViewDetail(int id)
        {
            return db.HoaDons.Find(id);
        }

        //Thêm HoaDon
        public long Insert(HoaDon entity)
        {
            db.HoaDons.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //Update HoaDon
        public bool Update(HoaDon entity)
        {
            try
            {
                var hd = db.HoaDons.Find(entity.ID);
                hd.MaNV = entity.MaNV;
                hd.MaCH = entity.MaCH;
                hd.MaKH = entity.MaKH;
                hd.TongTien = entity.TongTien;
                hd.ModifiedBy = entity.ModifiedBy;
                hd.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa HoaDon
        public bool Delete(int id)
        {
            try
            {
                var nd = db.HoaDons.Find(id);
                db.HoaDons.Remove(nd);
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
