using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Models.Sınıflar
{
    public class Mesajlar
    {
        [Key]
        public int MesajID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50, ErrorMessage = "En Fazla 50 Karakter Giriniz!")]
        public string Gönderici { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50, ErrorMessage = "En Fazla 50 Karakter Giriniz!")]
        public string Alıcı { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50, ErrorMessage = "En Fazla 50 Karakter Giriniz!")]
        public string Konu { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(2000, ErrorMessage = "En Fazla 2000 Karakter Giriniz!")]
        [AllowHtml]
        public string İcerik { get; set; }

        public DateTime Tarih { get; set; }

        public bool Durum { get; set; }
    }
}