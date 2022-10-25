using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;


namespace Model.Dao
{
    public class SanPhamDao
    {
        MoriiCoffeeDBContext db = null;

        public SanPhamDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết SanPham
        public SanPham ViewDetail(int id)
        {
            return db.SanPhams.Find(id);
        }

        //Thêm SanPham
        public long Insert(SanPham entity)
        {
            db.SanPhams.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //Update SanPham
        public bool Update(SanPham entity)
        {
            try
            {
                var sp = db.SanPhams.Find(entity.ID);
                sp.PhanLoai = entity.PhanLoai;
                sp.ModifiedBy = entity.ModifiedBy;
                sp.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa SanPham
        public bool Delete(int id)
        {
            try
            {
                var nd = db.SanPhams.Find(id);
                db.SanPhams.Remove(nd);
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
