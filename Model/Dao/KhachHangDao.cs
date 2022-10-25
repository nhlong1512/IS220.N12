using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;


namespace Model.Dao
{
    public class KhachHangDao
    {
        MoriiCoffeeDBContext db = null;
        public KhachHangDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Xem chi tiết Khách hàng
        public KhachHang ViewDetail(int id)
        {
            return db.KhachHangs.Find(id);
        }


    }
}
