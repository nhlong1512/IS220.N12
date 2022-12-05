using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class DatHangDao
    {
        MoriiCoffeeDBContext db = null;

        public DatHangDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết DatHang
        public DatHang ViewDetail(long id)
        {
            return db.DatHangs.Find(id);
        }


        public DatHang ViewDetailByMaHD(long id)
        {
            DatHang dhh = new DatHang();
            dhh = db.DatHangs.SingleOrDefault(p => p.MaHoaDon == id);
            return dhh;
        }

        //Xem tất cả DatHang
        public List<DatHang> ViewAll()
        {
            List<DatHang> dathangs = new List<DatHang>();
            if (db.DatHangs.Count() == 0)
            {
                return dathangs;
            }
            var list = db.DatHangs.Where(p => p.ID > 0);
            //Convert từ IqueryTable sang list
            dathangs = new List<DatHang>(list);
            return dathangs;
        }
        //Xem tất cả DatHang có Search
        public IEnumerable<DatHang> ListAllPaging(string searchString)
        {
            IQueryable<DatHang> model = db.DatHangs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.HoTen.Contains(searchString) || ("#" + (x.ID).ToString()).Contains(searchString)
                || (x.CreatedDate.ToString()).Contains(searchString) ||
                (x.TTDH).Contains(searchString) || (x.PTTT).Contains(searchString)); 
                
            }
            return model;
        }

        //Xem tất cả DatHangChoXacNhan có Search
        public IEnumerable<DatHang> ListAllPagingChoXacNhan(string searchString)
        {
            IQueryable<DatHang> model = db.DatHangs;
            model = model.Where(x => x.TTDH == "Chờ Xác Nhận");
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.HoTen.Contains(searchString) || ("#" + (x.ID).ToString()).Contains(searchString)
                || (x.CreatedDate.ToString()).Contains(searchString) ||
                (x.TTDH).Contains(searchString) || (x.PTTT).Contains(searchString));

            }
            return model;
        }


        //Xem tất cả DatHangDangGiao có Search
        public IEnumerable<DatHang> ListAllPagingDangGiao(string searchString)
        {
            IQueryable<DatHang> model = db.DatHangs;
            model = model.Where(x => x.TTDH == "Đang Giao");
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.HoTen.Contains(searchString) || ("#" + (x.ID).ToString()).Contains(searchString)
                || (x.CreatedDate.ToString()).Contains(searchString) ||
                (x.TTDH).Contains(searchString) || (x.PTTT).Contains(searchString));

            }
            return model;
        }

        //Xem tất cả DatHangĐaGiao có Search
        public IEnumerable<DatHang> ListAllPagingDaGiao(string searchString)
        {
            IQueryable<DatHang> model = db.DatHangs;
            model = model.Where(x => x.TTDH == "Đã Giao");
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.HoTen.Contains(searchString) || ("#" + (x.ID).ToString()).Contains(searchString)
                || (x.CreatedDate.ToString()).Contains(searchString) ||
                (x.TTDH).Contains(searchString) || (x.PTTT).Contains(searchString));

            }
            return model;
        }

        //Xem tất cả DatHangDaHuy có Search
        public IEnumerable<DatHang> ListAllPagingDaHuy(string searchString)
        {
            IQueryable<DatHang> model = db.DatHangs;
            model = model.Where(x => x.TTDH == "Đã Hủy");
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.HoTen.Contains(searchString) || ("#" + (x.ID).ToString()).Contains(searchString)
                || (x.CreatedDate.ToString()).Contains(searchString) ||
                (x.TTDH).Contains(searchString) || (x.PTTT).Contains(searchString));

            }
            return model;
        }

        //Xem tất cả DatHang của Người dùng
        public List<DatHang> ViewAllByID(long id)
        {
            List<DatHang> dathangs = new List<DatHang>();
            if (db.DatHangs.Count() == 0)
            {
                return dathangs;
            }
            var list = db.DatHangs.Where(p => p.MaKH == id);
            //Convert từ IqueryTable sang list
            dathangs = new List<DatHang>(list);
            return dathangs;
        }

        //Xem Tất cả đơn đặt chờ xác nhận của người dùng có ID
        public List<DatHang> ViewAllByIDChoXacNhan(long id)
        {
            List<DatHang> dathangs = new List<DatHang>();
            if (db.DatHangs.Count() == 0)
            {
                return dathangs;
            }
            var list = db.DatHangs.Where(p => p.MaKH == id);
            var listt = list.Where(p => p.TTDH == "Chờ Xác Nhận");
            //Convert từ IqueryTable sang list
            dathangs = new List<DatHang>(listt);
            return dathangs;
        }

        //Xem Tất cả đơn đặt Đang Giao của người dùng có ID
        public List<DatHang> ViewAllByIDDangGiao(long id)
        {
            List<DatHang> dathangs = new List<DatHang>();
            if (db.DatHangs.Count() == 0)
            {
                return dathangs;
            }
            var list = db.DatHangs.Where(p => p.MaKH == id);
            var listt = list.Where(p => p.TTDH == "Đang Giao");
            //Convert từ IqueryTable sang list
            dathangs = new List<DatHang>(listt);
            return dathangs;
        }

        //Xem Tất cả đơn đặt Đã Giao của người dùng có ID
        public List<DatHang> ViewAllByIDDaGiao(long id)
        {
            List<DatHang> dathangs = new List<DatHang>();
            if (db.DatHangs.Count() == 0)
            {
                return dathangs;
            }
            var list = db.DatHangs.Where(p => p.MaKH == id);
            var listt = list.Where(p => p.TTDH == "Đã Giao");
            //Convert từ IqueryTable sang list
            dathangs = new List<DatHang>(listt);
            return dathangs;
        }

        //Xem Tất cả đơn đặt Đa Huy của người dùng có ID
        public List<DatHang> ViewAllByIDDaHuy(long id)
        {
            List<DatHang> dathangs = new List<DatHang>();
            if (db.DatHangs.Count() == 0)
            {
                return dathangs;
            }
            var list = db.DatHangs.Where(p => p.MaKH == id);
            var listt = list.Where(p => p.TTDH == "Đã Hủy");
            //Convert từ IqueryTable sang list
            dathangs = new List<DatHang>(listt);
            return dathangs;
        }

        //Thêm DatHang
        public long Insert(DatHang entity)
        {
            entity.CreatedDate = DateTime.Now;
            db.DatHangs.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //Update DatHang
        public bool Update(DatHang entity)
        {
            try
            {
                var dh = db.DatHangs.Find(entity.ID);
                dh.MaHoaDon = entity.MaHoaDon;
                dh.MaKH = entity.MaKH;
                dh.HoTen = entity.HoTen;
                dh.SDT = entity.SDT;
                dh.Email = entity.Email;
                dh.DiaChiNhanHang = entity.DiaChiNhanHang;
                dh.Tinh = entity.Tinh;
                dh.Quan = entity.Quan;
                dh.Phuong = entity.Phuong;
                dh.GhiChu = entity.GhiChu;
                dh.PTTT = entity.PTTT;
                dh.TTDH = entity.TTDH;
                dh.UrlImage = entity.UrlImage;
                dh.ModifiedBy = entity.ModifiedBy;
                dh.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        

        //Xóa DatHang
        public bool Delete(long id)
        {
            try
            {
                var nd = db.DatHangs.Find(id);
                db.DatHangs.Remove(nd);
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
