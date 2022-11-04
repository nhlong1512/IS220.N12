using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;


namespace Model.Dao
{
    public class SanPhamDao
    {
        MoriiCoffeeDBContext db = null;

        public SanPhamDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem tất cả SanPham
        public List<SanPham> ViewAll()
        {
            List<SanPham> sanphams = new List<SanPham>();
            if(db.SanPhams.Count() == 0)
            {
                return sanphams;
            }
            var list = db.SanPhams.Where(p => p.ID > 0);
            //Convert từ IqueryTable sang list
            sanphams = new List<SanPham>(list);
            return sanphams;

        }

        //Xem chi tiết SanPham
        public SanPham ViewDetail(long id)
        {
            return db.SanPhams.Find(id);
        }

        //Thêm SanPham
        public long Insert(SanPham entity)
        {
            entity.CreatedDate = DateTime.Now;
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
        public bool Delete(long id)
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

        //
        public IEnumerable<SanPham> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<SanPham> model = db.SanPhams;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.PhanLoai.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
}
