using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Model.EF;


namespace Model.Dao
{
    public class KhuyenMaiDao
    {
        MoriiCoffeeDBContext db = null;

        public KhuyenMaiDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem tất cả KhuyenMai
        public List<KhuyenMai> ViewAll()
        {
            List<KhuyenMai> nds = new List<KhuyenMai>();
            if (db.KhuyenMais.Count() == 0)
            {
                return nds;
            }
            var list = db.KhuyenMais.Where(p => p.ID > 0);
            //Convert từ IqueryTable sang list
            nds = new List<KhuyenMai>(list);
            return nds;

        }

        //Xem chi tiết KhuyenMai
        public KhuyenMai ViewDetail(long id)
        {
            return db.KhuyenMais.Find(id);
        }

        //Thêm KhuyenMai
        public long Insert(KhuyenMai entity)
        {
            entity.CreatedDate = DateTime.Now;
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
                km.Status = entity.Status;
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
        public bool Delete(long id)
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


        //Get khuyến mãi status True
        public KhuyenMai ViewDetailKhuyenMaiTrue()
        {
            KhuyenMai km = new KhuyenMai();
            km = db.KhuyenMais.SingleOrDefault(p => p.Status == true);
            return km;
        }


        //
    }
}
