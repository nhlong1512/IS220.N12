namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KhuyenMai
    {
        [Key]
        public int MaKM { get; set; }

        public int PhanTramKM { get; set; }

    }
}
