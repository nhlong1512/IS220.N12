using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;


namespace Model.Dao
{
    public class BlogDao
    {
        MoriiCoffeeDBContext db = null;

        public BlogDao()
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
            var list = db.Blogs.Where(p => p.ID > 0).OrderByDescending(x => x.CreatedDate);
            //Convert từ IqueryTable sang list
            blogs = new List<Blog>(list);
            return blogs;
        }

        //Xem chi tiết Blog
        public Blog ViewDetail(long id)
        {
            return db.Blogs.Find(id);
        }

        //Thêm Blog
        public long Insert(Blog entity)
        {

            entity.CreatedDate = DateTime.Now;
            db.Blogs.Add(entity);
                db.SaveChanges();
                return entity.ID;
            
        }

        //Update Blog
        public bool Update(Blog entity)
        {
            try
            {
                var bl = db.Blogs.Find(entity.ID);
                bl.TieuDe = entity.TieuDe;
                bl.MoTa = entity.MoTa;
                bl.NoiDung = entity.NoiDung;
                bl.UrlImage = entity.UrlImage;
                bl.ModifiedBy = entity.ModifiedBy;
                bl.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa Blog
        public bool Delete(long id)
        {
            try
            {
                var nd = db.Blogs.Find(id);
                db.Blogs.Remove(nd);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }   


        //
        public IEnumerable<Blog> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Blog> model = db.Blogs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TieuDe.Contains(searchString) || ("#" + (x.ID).ToString()).Contains(searchString)
                || ((x.CreatedDate).ToString()).Contains(searchString) || ("Nguyễn Hữu Long").Contains(searchString));
            }
            return model.OrderByDescending(x=>x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
}
