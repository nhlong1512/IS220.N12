using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoriiCoffee.Common
{
    [Serializable]
    public class CartItem
    {
        public ChiTietSanPham ChiTietSanPham { set; get; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string Topping { get; set; }
        public long Gia { get; set; }

    }
}