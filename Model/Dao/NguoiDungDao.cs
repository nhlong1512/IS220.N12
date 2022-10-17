using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class NguoiDungDao
    {
        MoriiCoffeeDBContext db = null;

        public NguoiDungDao()
        {
            db = new MoriiCoffeeDBContext();
        }

        //Thêm Người dùng
        public long Insert (NguoiDung entity)
        {
            db.NguoiDungs.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

    }
}
