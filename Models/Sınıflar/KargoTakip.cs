using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Sınıflar
{
    public class KargoTakip
    {
        [Key]
        public int KargoTakipid { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(10, ErrorMessage = "En Fazla 10 Karakter Giriniz!")]
        public string TakipKodu { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(100, ErrorMessage = "En Fazla 100 Karakter Giriniz!")]
        public string Aciklama { get; set; }

        public DateTime TarihZaman { get; set; }
    }
}