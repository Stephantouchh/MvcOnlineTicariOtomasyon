using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Sınıflar
{
    public class KargoDetay
    {
        [Key]
        public int KargoDetayid { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(300, ErrorMessage = "En Fazla 300 Karakter Giriniz!")]
        public string Aciklama { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(10, ErrorMessage = "En Fazla 10 Karakter Giriniz!")]
        public string TakipKodu { get; set; } //1234123AB

        [Column(TypeName = "VarChar")]
        [StringLength(30, ErrorMessage = "En Fazla 30 Karakter Giriniz!")]
        public string Personel { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(30, ErrorMessage = "En Fazla 30 Karakter Giriniz!")]
        public string Alici { get; set; }

        public DateTime Tarih { get; set; }
    }
}