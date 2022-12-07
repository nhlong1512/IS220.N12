using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;


namespace Model.Dao
{
    public class HoaDonDao
    {
        MoriiCoffeeDBContext db = null;

        public HoaDonDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết HoaDon
        public HoaDon ViewDetail(long id)
        {
            return db.HoaDons.Find(id);
        }

        //Xem tất cả HoaDon
        public List<HoaDon> ViewAll()
        {
            List<HoaDon> hoadons = new List<HoaDon>();
            if (db.HoaDons.Count() == 0)
            {
                return hoadons;
            }
            var list = db.HoaDons.Where(p => p.ID > 0);
            //Convert từ IqueryTable sang list
            hoadons = new List<HoaDon>(list);
            return hoadons;
        }

        //public decimal doanhThuThang(int t)
        //{
        //    decimal doanhThu = 0;
        //    var list = db.HoaDons.Where(p => p.CreatedDate.);
        //    List<HoaDon> hoadons = new List<HoaDon>(list);
        //    foreach (var item in hoadons)
        //    {
        //        if(item.CreatedDate.Value ==  )
        //    }

        //    return doanhThu;

        //}

        //Xem tất cả HoaDon có id 
        public List<HoaDon> ViewAllByID(long id)
        {
            List<HoaDon> hoadons = new List<HoaDon>();
            if (db.HoaDons.Count() == 0)
            {
                return hoadons;
            }
            var list = db.HoaDons.Where(p => p.MaKH == id);
            //Convert từ IqueryTable sang list
            hoadons = new List<HoaDon>(list);
            return hoadons;
        }


        //Thêm HoaDon
        public long Insert(HoaDon entity)
        {
            entity.CreatedDate = DateTime.Now;
            db.HoaDons.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //Update HoaDon
        public bool Update(HoaDon entity)
        {
            try
            {
                var hd = db.HoaDons.Find(entity.ID);
                hd.MaNV = entity.MaNV;
                hd.MaCH = entity.MaCH;
                hd.MaKH = entity.MaKH;
                hd.IsOnline = entity.IsOnline;
                hd.MaKM = entity.MaKM;
                hd.TongTien = entity.TongTien;
                hd.TienKM = entity.TienKM;
                hd.ModifiedBy = entity.ModifiedBy;
                hd.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateTongTien(HoaDon entity)
        {
            try
            {
                var hd = db.HoaDons.Find(entity.ID);
                hd.MaNV = entity.MaNV;
                hd.MaCH = entity.MaCH;
                hd.MaKH = entity.MaKH;
                hd.IsOnline = entity.IsOnline;
                hd.MaKM = entity.MaKM;
                hd.TongTien = entity.TongTien;
                hd.ModifiedBy = entity.ModifiedBy;
                hd.ModifiedDate = entity.ModifiedDate;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa HoaDon
        public bool Delete(long id)
        {
            try
            {
                var nd = db.HoaDons.Find(id);
                db.HoaDons.Remove(nd);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public IEnumerable<HoaDon> ListAllPaging(string searchString)
        {
            IQueryable<HoaDon> model = db.HoaDons;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => ("#" + (x.ID).ToString()).Contains(searchString)
                || (x.CreatedDate.ToString()).Contains(searchString) ||
                ("#" + (x.MaNV).ToString()).Contains(searchString) || ("#" + (x.MaKM).ToString()).Contains(searchString)
                || (x.TongTien).ToString().Contains(searchString));

            }
            return model.OrderBy(x => x.ID);
        }

        public string ConvertIDKHToHoTen(long id)
        {
            NguoiDungDao nddao = new NguoiDungDao();
            var nd = nddao.ViewDetail(id);
            return nd.HoTen;
        }

    }
}
