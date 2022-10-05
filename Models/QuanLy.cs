using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoriiCoffee.Models
{
    [Table("QuanLys")]
    public class QuanLy
    {
        [Key]
        public int ID { get; set; }
    }
}