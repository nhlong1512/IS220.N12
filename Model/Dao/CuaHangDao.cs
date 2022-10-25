using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;


namespace Model.Dao
{
    public class CuaHangDao
    {
        MoriiCoffeeDBContext db = null;

        public CuaHangDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết CuaHang
        public CuaHang ViewDetail(int id)
        {
            return db.CuaHangs.Find(id);
        }

        //Thêm CuaHang
        public long Insert(CuaHang entity)
        {
            db.CuaHangs.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //Update CuaHang
        public bool Update(CuaHang entity)
        {
            try
            {
                var ch = db.CuaHangs.Find(entity.ID);
                ch.TenCuaHang = entity.TenCuaHang;
                ch.SDT = entity.SDT;
                ch.MaQuanLy = entity.MaQuanLy;
                ch.DiaChi = entity.DiaChi;
                ch.UrlImage = entity.UrlImage;
                ch.ModifiedBy = entity.ModifiedBy;
                ch.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa CuaHang
        public bool Delete(int id)
        {
            try
            {
                var nd = db.CuaHangs.Find(id);
                db.CuaHangs.Remove(nd);
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
