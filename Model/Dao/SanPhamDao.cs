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

        //Xem tất cả Blog
        public List<Blog> ViewAll()
        {
            List<Blog> blogs = new List<Blog>();
            if(db.Blogs.Count() == 0)
            {
                return blogs;
            }
            var list = db.Blogs.Where(p => p.ID > 0);
            //Convert từ IqueryTable sang list
            blogs = new List<Blog>(list);
            return blogs;

        }

        //Xem chi tiết SanPham
        public SanPham ViewDetail(int id)
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
