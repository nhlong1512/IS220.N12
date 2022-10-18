using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;


namespace Model.Dao
{
    class BlogDao
    {
        MoriiCoffeeDBContext db = null;

        public BlogDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết người dùng
        public Blog ViewDetail(int id)
        {
            return db.Blogs.Find(id);
        }

        //Thêm Người dùng
        public long Insert(Blog entity)
        {
            db.Blogs.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //Update người dùng
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

        //Xóa người dùng
        public bool Delete(int id)
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
    }
}
