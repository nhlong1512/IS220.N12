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

        public List<ChiTietSanPham> ViewAllTatCaClient()
        {
            List<ChiTietSanPham> chitietsps = new List<ChiTietSanPham>();
            if (db.ChiTietSanPhams.Count() == 0)
            {
                return chitietsps;
            }
            var list = db.ChiTietSanPhams.Where(p => (p.MaPhanLoai == 1) || (p.MaPhanLoai == 2) || (p.MaPhanLoai == 4));
            //Convert từ IqueryTable sang list
            chitietsps = new List<ChiTietSanPham>(list);
            return chitietsps;
        }

        //Xem tất cả ChiTietSanPham Cà Phê
        public List<ChiTietSanPham> ViewAllCaPhe()
        {
            List<ChiTietSanPham> chitietsps = new List<ChiTietSanPham>();
            if (db.ChiTietSanPhams.Count() == 0)
            {
                return chitietsps;
            }
            var list = db.ChiTietSanPhams.Where(p => p.MaPhanLoai == 1);
            //Convert từ IqueryTable sang list
            chitietsps = new List<ChiTietSanPham>(list);
            return chitietsps;
        }

        //Xem tất cả ChiTietSanPham Trà Sữa
        public List<ChiTietSanPham> ViewAllTraSua()
        {
            List<ChiTietSanPham> chitietsps = new List<ChiTietSanPham>();
            if (db.ChiTietSanPhams.Count() == 0)
            {
                return chitietsps;
            }
            var list = db.ChiTietSanPhams.Where(p => p.MaPhanLoai == 2);
            //Convert từ IqueryTable sang list
            chitietsps = new List<ChiTietSanPham>(list);
            return chitietsps;
        }

        //Xem tất cả ChiTietSanPham Trà Sữa
        public List<ChiTietSanPham> ViewAllKhac()
        {
            List<ChiTietSanPham> chitietsps = new List<ChiTietSanPham>();
            if (db.ChiTietSanPhams.Count() == 0)
            {
                return chitietsps;
            }
            var list = db.ChiTietSanPhams.Where(p => p.MaPhanLoai == 4);
            //Convert từ IqueryTable sang list
            chitietsps = new List<ChiTietSanPham>(list);
            return chitietsps;
        }

        //Xem danh sách các món Topping
        public List<ChiTietSanPham> ViewListTopping()
        {
            List<ChiTietSanPham> chitietsps = new List<ChiTietSanPham>();
            if (db.ChiTietSanPhams.Count() == 0)
            {
                return chitietsps;
            }
            var list = db.ChiTietSanPhams.Where(p => p.MaPhanLoai == 3);
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
            entity.Size = true;
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
                ctsp.UrlImage = entity.UrlImage;
                ctsp.MoTaSanPham = entity.MoTaSanPham;
                ctsp.ChiTietSanPham1 = entity.ChiTietSanPham1;
                ctsp.ModifiedBy = entity.ModifiedBy;
                ctsp.MaPhanLoai = entity.MaPhanLoai;
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
                model = model.Where(x => x.TenSanPham.Contains(searchString) || ("#"+(x.ID).ToString()).Contains(searchString)
                || (x.Gia).ToString().Contains(searchString) || (x.CreatedDate.ToString()).Contains(searchString) ||
                (x.Status == true ? "Đang Mở Bán" : "Chưa Mở Bán").Contains(searchString) ||
                (x.MaPhanLoai == 1 ? "Cà Phê" : (x.MaPhanLoai == 2 ? "Trà Sữa" : (x.MaPhanLoai == 3 ? "Topping" : "Khác"))).Contains(searchString));
            }
            return model.OrderBy(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<ChiTietSanPham> ListAllPagingCaPhe(string searchString, int page, int pageSize)
        {
            IQueryable<ChiTietSanPham> model = db.ChiTietSanPhams;
            model = model.Where(x => x.MaPhanLoai == 1);

            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenSanPham.Contains(searchString) || ("#" + (x.ID).ToString()).Contains(searchString)
                || (x.Gia).ToString().Contains(searchString) || (x.CreatedDate.ToString()).Contains(searchString) ||
                (x.Status == true ? "Đang Mở Bán" : "Chưa Mở Bán").Contains(searchString) || ("Cà Phê").Contains(searchString));
            }
            return model.OrderBy(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<ChiTietSanPham> ListAllPagingTraSua(string searchString, int page, int pageSize)
        {
            IQueryable<ChiTietSanPham> model = db.ChiTietSanPhams;
            model = model.Where(x => x.MaPhanLoai == 2);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenSanPham.Contains(searchString) || ("#" + (x.ID).ToString()).Contains(searchString)
                || (x.Gia).ToString().Contains(searchString) || (x.CreatedDate.ToString()).Contains(searchString) ||
                (x.Status == true ? "Đang Mở Bán" : "Chưa Mở Bán").Contains(searchString) || ("Trà Sữa").Contains(searchString));
            }
            return model.OrderBy(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<ChiTietSanPham> ListAllPagingTopping(string searchString, int page, int pageSize)
        {
            IQueryable<ChiTietSanPham> model = db.ChiTietSanPhams;
            model = model.Where(x => x.MaPhanLoai == 3);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenSanPham.Contains(searchString) || ("#" + (x.ID).ToString()).Contains(searchString)
                || (x.Gia).ToString().Contains(searchString) || (x.CreatedDate.ToString()).Contains(searchString) ||
                (x.Status == true ? "Đang Mở Bán" : "Chưa Mở Bán").Contains(searchString) ||("Topping").Contains(searchString));
            }
            return model.OrderBy(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<ChiTietSanPham> ListAllPagingKhac(string searchString, int page, int pageSize)
        {
            IQueryable<ChiTietSanPham> model = db.ChiTietSanPhams;
            model = model.Where(x => x.MaPhanLoai == 4);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenSanPham.Contains(searchString) || ("#" + (x.ID).ToString()).Contains(searchString)
                || (x.Gia).ToString().Contains(searchString) || (x.CreatedDate.ToString()).Contains(searchString) ||
                (x.Status == true ? "Đang Mở Bán" : "Chưa Mở Bán").Contains(searchString) || ("Khác").Contains(searchString));
            }
            return model.OrderBy(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

    }
}
