using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;


namespace Model.Dao
{
    public class NguoiDungDao
    {
        MoriiCoffeeDBContext db = null;

        public NguoiDungDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem tất cả NguoiDung
        public List<NguoiDung> ViewAll()
        {
            List<NguoiDung> nds = new List<NguoiDung>();
            if (db.NguoiDungs.Count() == 0)
            {
                return nds;
            }
            var list = db.NguoiDungs.Where(p => p.ID > 0);
            //Convert từ IqueryTable sang list
            nds = new List<NguoiDung>(list);
            return nds;

        }

        //Xem chi tiết người dùng
        public NguoiDung ViewDetail(int id)
        {
            return db.NguoiDungs.Find(id);
        }

        //Thêm Người dùng
        public long Insert (NguoiDung entity)
        {
            entity.CreatedDate = DateTime.Now;
            db.NguoiDungs.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //Update người dùng
        public bool Update(NguoiDung entity)
        {
            try
            {
                var nd = db.NguoiDungs.Find(entity.ID);
                nd.HoTen = entity.HoTen;
                nd.SDT = entity.SDT;
                nd.NgSinh = entity.NgSinh;
                nd.GioiTinh = entity.GioiTinh;
                nd.Urlmage = entity.Urlmage;
                nd.ModifiedBy = entity.ModifiedBy;
                nd.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        //Xóa người dùng
        public bool Delete(int id)
        {
            try
            {
                var nd = db.NguoiDungs.Find(id);
                db.NguoiDungs.Remove(nd);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        //
        public IEnumerable<NguoiDung> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<NguoiDung> model = db.NguoiDungs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.HoTen.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

    }
}
