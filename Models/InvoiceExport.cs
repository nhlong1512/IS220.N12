using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoriiCoffee.Models
{
    public partial class InvoiceExport
    {
        public long ID { get; set; }

        public string TenSP { get; set; }

        public string Size { get; set; }

        public string Topping { get; set; }

        public decimal? Gia { get; set; }

        public int? SoLuong { get; set; }

        public decimal? ThanhTien { get; set; }

        public long? IDHoaDon { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string TenKM { get; set; }
        public decimal TienKM { get; set; }
        public bool isOnline { get; set; }
        public decimal TongTien { get; set; }

    }
}