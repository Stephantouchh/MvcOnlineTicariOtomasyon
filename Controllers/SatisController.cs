using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class SatisController : Controller
    {
        Context _context = new Context();
     
        // GET: Satis
        public ActionResult Index()
        {
            var satis = _context.SatisHarekets.ToList();
            return View(satis);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> deger1 = (from x in _context.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.UrunID.ToString()
                                           }).ToList();


            List<SelectListItem> deger2 = (from x in _context.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.Cariid.ToString()
                                           }).ToList();


            List<SelectListItem> deger3 = (from x in _context.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();

            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket satishareket)
        {
            satishareket.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            _context.SatisHarekets.Add(satishareket);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in _context.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.UrunID.ToString()
                                           }).ToList();


            List<SelectListItem> deger2 = (from x in _context.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.Cariid.ToString()
                                           }).ToList();


            List<SelectListItem> deger3 = (from x in _context.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();

            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;


            var sts = _context.SatisHarekets.Find(id);
            return View("SatisGetir", sts);
        }
        public ActionResult SatisGuncelle(SatisHareket s)
        {
            var sts = _context.SatisHarekets.Find(s.Satisid);
            sts.Cariid = s.Cariid;
            sts.Adet = s.Adet;
            sts.Fiyat = s.Fiyat;
            sts.Personelid = s.Personelid;
            sts.Tarih = s.Tarih;
            sts.ToplamTutar = s.ToplamTutar;
            sts.Urunid = s.Urunid;
            sts.Tarih = s.Tarih;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var degerler = _context.SatisHarekets.Where(x => x.Satisid == id).ToList();
            return View(degerler);
        }

        public ActionResult SatisListesi(int id)
        {
            var degerler = _context.SatisHarekets.Where(x => x.Satisid == id).ToList();
            return View(degerler);
        }
    }
}