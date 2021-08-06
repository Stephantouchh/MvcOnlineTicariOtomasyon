using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context _context = new Context();

        public ActionResult Index()
        {
            var liste = _context.Faturalars.ToList();
            return View(liste);
        }
        [HttpGet]
        public ActionResult YeniFatura()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniFatura(Faturalar fatura)
        {
            _context.Faturalars.Add(fatura);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaGetir(int id)
        {
            var ftr = _context.Faturalars.Find(id);
            return View("FaturaGetir", ftr);
        }
        public ActionResult FaturaGuncelle(Faturalar fatura)
        {
            var ftr = _context.Faturalars.Find(fatura.Faturaid);
            ftr.FaturaSeriNo = fatura.FaturaSeriNo;
            ftr.FaturaSıraNo = fatura.FaturaSıraNo;
            ftr.VergiDairesi = fatura.VergiDairesi;
            ftr.Tarih = fatura.Tarih;
            ftr.Saat = fatura.Saat;
            ftr.TeslimEden = fatura.TeslimEden;
            ftr.TeslimAlan = fatura.TeslimAlan;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var degerler = _context.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem fatura)
        {
            _context.FaturaKalems.Add(fatura);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Dinamik()
        {
            Class4 cs = new Class4();
            cs.deger1 = _context.Faturalars.ToList();
            cs.deger2 = _context.FaturaKalems.ToList();
            return View(cs);
        }
        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSıraNo, DateTime Tarih, string VergiDairesi, string Saat, string TeslimEden, string TeslimAlan, string Toplam, FaturaKalem[] kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSıraNo = FaturaSıraNo;
            f.Tarih = Tarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimEden = TeslimEden;
            f.TeslimAlan = TeslimAlan;
         //   f.Toplam = decimal.Parse(Toplam);
            _context.Faturalars.Add(f);
            foreach (var x in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = x.Aciklama;               
                fk.BirimFiyat = x.BirimFiyat;
                fk.Faturaid = x.FaturaKalemid;
                fk.Miktar = x.Miktar;
                fk.Tutar = x.Tutar;
                _context.FaturaKalems.Add(fk);
            }
            _context.SaveChanges();
            return Json("İşlem Başarılı Bir Şekilde Gerçekleşti", JsonRequestBehavior.AllowGet);
        }
    }
}