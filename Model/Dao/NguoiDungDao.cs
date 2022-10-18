using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class NguoiDungDao
    {
        MoriiCoffeeDBContext db = null;

        public NguoiDungDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết người dùng
        public NguoiDung ViewDetail(int id)
        {
            return db.NguoiDungs.Find(id);
        }

        //Thêm Người dùng
        public long Insert (NguoiDung entity)
        {
            db.NguoiDungs.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //Update người dùng
        public bool Update(NguoiDung entity)
        {
            try
            {
                var nguoidung = db.NguoiDungs.Find(entity.ID);
                nguoidung.HoTen = entity.HoTen;
                nguoidung.SDT = entity.SDT;
                nguoidung.NgSinh = entity.NgSinh;
                nguoidung.GioiTinh = entity.GioiTinh;
                nguoidung.Urlmage = entity.Urlmage;
                nguoidung.ModifiedBy = entity.ModifiedBy;
                nguoidung.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;


            }
            catch(Exception ex)
            {
                return false;
            }
        }

        //Xóa người dùng
        public bool Delete(int id)
        {
            try
            {
                var nguoidung = db.NguoiDungs.Find(id);
                db.NguoiDungs.Remove(nguoidung);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
