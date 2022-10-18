using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;


namespace Model.Dao
{
    class NhanVienDao
    {
        MoriiCoffeeDBContext db = null;

        public NhanVienDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết NhanVien
        public NhanVien ViewDetail(int id)
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
