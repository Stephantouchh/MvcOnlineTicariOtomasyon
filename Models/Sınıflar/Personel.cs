using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Sınıflar
{
    public class Personel
    {
        [Key]
        public int Personelid { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En Fazla 30 Karakter Giriniz!")]
        public string PersonelAd { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En Fazla 30 Karakter Giriniz!")]
        public string PersonelSoyad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public string PersonelMail { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(13, ErrorMessage = "En Fazla 30 Karakter Giriniz!")]
        public string PersonelSehir { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En Fazla 30 Karakter Giriniz!")]
        public string PersonelTelefon { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string PersonelGorsel { get; set; }

        public bool Durum { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; }

        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz!")]
        public int Departmanid { get; set; }

        public virtual Departman Departman { get; set; }
    }
}