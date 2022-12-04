using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;


namespace Model.Dao
{
    public class NhanVienDao
    {
        MoriiCoffeeDBContext db = null;

        public NhanVienDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết NhanVien
        public List<NhanVien> ViewAll()
        {
            List<NhanVien> nds = new List<NhanVien>();
            if (db.NguoiDungs.Count() == 0)
            {
                return nds;
            }
            var list = db.NhanViens.Where(p => p.ID > 0);
            //Convert từ IqueryTable sang list
            nds = new List<NhanVien>(list);
            return nds;

        }
        public NhanVien ViewDetail(long id)
        {
            return db.NhanViens.Find(id);
        }


        //Update NhanVien
        public bool Update(NhanVien entity)
        {
            try
            {
                var nv = db.NhanViens.Find(entity.ID);
                nv.Luong = entity.Luong;
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
