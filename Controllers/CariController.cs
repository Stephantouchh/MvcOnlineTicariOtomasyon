using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class CariController : Controller
    {
        // GET: Cari
        Context _context = new Context();
      
        public ActionResult Index()
        {
            var cari = _context.Carilers.Where(x => x.Durum == true).ToList();
            return View(cari);
        }
        [HttpGet]
        public ActionResult YeniCari()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniCari(Cariler cari)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniCari");
            }
            cari.Durum = true;
            _context.Carilers.Add(cari);
            _context.SaveChanges();
            return RedirectToAction("CariListe");
        }
        public ActionResult CariSil(int id)
        {

            var cari = _context.Carilers.Find(id);
            cari.Durum = false;
            _context.SaveChanges();
            return RedirectToAction("CariListe");
        }
        public ActionResult CariGetir(int id)
        {
            var cari = _context.Carilers.Find(id);
            return View("CariGetir", cari);
        }
        public ActionResult CariGuncelle(Cariler cariler)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }
            var cr = _context.Carilers.Find(cariler.Cariid);
            cr.CariAd = cariler.CariAd;
            cr.CariSoyad = cariler.CariSoyad;
            cr.CariSehir = cariler.CariSehir;
            cr.CariMail = cariler.CariMail;
            cr.CariTelefon = cariler.CariTelefon;
            cr.CariGorsel = cariler.CariGorsel;
            cr.Durum = true;
            _context.SaveChanges();
            return RedirectToAction("CariListe");
        }
        public ActionResult MusteriSatis(int id)
        {
            var cr = _context.Carilers.Where(x => x.Cariid == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.ad = cr;
            var degerler = _context.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }
        public ActionResult CariListe()
        {
            var sorgu = _context.Carilers.Where(x => x.Durum == true).ToList();
            return View(sorgu);
        }
    }
}