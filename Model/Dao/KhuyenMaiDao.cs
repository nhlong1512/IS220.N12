using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;


namespace Model.Dao
{
    class KhuyenMaiDao
    {
        MoriiCoffeeDBContext db = null;

        public KhuyenMaiDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết KhuyenMai
        public KhuyenMai ViewDetail(int id)
        {
            return db.KhuyenMais.Find(id);
        }

        //Thêm KhuyenMai
        public long Insert(KhuyenMai entity)
        {
            db.KhuyenMais.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //Update KhuyenMai
        public bool Update(KhuyenMai entity)
        {
            try
            {
                var km = db.KhuyenMais.Find(entity.ID);
                km.PhanTramKM = entity.PhanTramKM; 
                km.ModifiedBy = entity.ModifiedBy;
                km.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa KhuyenMai
        public bool Delete(int id)
        {
            try
            {
                var nd = db.KhuyenMais.Find(id);
                db.KhuyenMais.Remove(nd);
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
