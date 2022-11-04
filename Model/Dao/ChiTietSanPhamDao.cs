using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;


namespace Model.Dao
{
    public class ChiTietSanPhamDao
    {
        MoriiCoffeeDBContext db = null;

        public ChiTietSanPhamDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem tất cả ChiTietSanPham
        public List<ChiTietSanPham> ViewAll()
        {
            List<ChiTietSanPham> chitietsps = new List<ChiTietSanPham>();
            if (db.ChiTietSanPhams.Count() == 0)
            {
                return chitietsps;
            }
            var list = db.ChiTietSanPhams.Where(p => p.ID > 0);
            //Convert từ IqueryTable sang list
            chitietsps = new List<ChiTietSanPham>(list);
            return chitietsps;

        }

        //Xem chi tiết SanPham
        public ChiTietSanPham ViewDetail(long id)
        {
            return db.ChiTietSanPhams.Find(id);
        }

        //Thêm Chi tiết sản phẩm
        public long Insert(ChiTietSanPham entity)
        {
            entity.CreatedDate = DateTime.Now;
            db.ChiTietSanPhams.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //Update Chi tiết sản phẩm
        public bool Update(ChiTietSanPham entity)
        {
            try
            {
                var ctsp = db.ChiTietSanPhams.Find(entity.ID);
                ctsp.TenSanPham = entity.TenSanPham;
                ctsp.Gia = entity.Gia;
                ctsp.GiaCu = entity.GiaCu;
                ctsp.Size = entity.Size;
                ctsp.MaKM = entity.MaKM;
                ctsp.UrlImage = entity.UrlImage;
                ctsp.MoTaSanPham = entity.MoTaSanPham;
                ctsp.ChiTietSanPham1 = entity.ChiTietSanPham1;
                ctsp.ModifiedBy = entity.ModifiedBy;
                ctsp.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa ChiTietSanPham
        public bool Delete(long id)
        {
            try
            {
                var nd = db.ChiTietSanPhams.Find(id);
                db.ChiTietSanPhams.Remove(nd);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //
        public IEnumerable<ChiTietSanPham> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<ChiTietSanPham> model = db.ChiTietSanPhams;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenSanPham.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

    }
}
