using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public public class NguoiDungDao
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
                var nd = db.NguoiDungs.Find(entity.ID);
                nd.HoTen = entity.HoTen;
                nd.SDT = entity.SDT;
                nd.NgSinh = entity.NgSinh;
                nd.GioiTinh = entity.GioiTinh;
                nd.Urlmage = entity.Urlmage;
                nd.ModifiedBy = entity.ModifiedBy;
                nd.ModifiedDate = DateTime.Now;
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
                var nd = db.NguoiDungs.Find(id);
                db.NguoiDungs.Remove(nd);
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
