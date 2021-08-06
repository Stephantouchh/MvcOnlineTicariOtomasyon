using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik

        Context _context = new Context();
      
        public ActionResult Index()
        {
            var deger1 = _context.Carilers.Where(x => x.Durum == true).Count().ToString();
            ViewBag.d1 = deger1;
            var deger2 = _context.Uruns.Where(x => x.Durum == true).Count().ToString();
            ViewBag.d2 = deger2;
            var deger3 = _context.Personels.Where(x => x.Durum == true).Count().ToString();
            ViewBag.d3 = deger3;
            var deger4 = _context.Kategoris.Where(x => x.Durum == true).Count().ToString();
            ViewBag.d4 = deger4;
            var deger5 = _context.Uruns.Sum(x => x.Stok).ToString();
            ViewBag.d5 = deger5;
            var deger6 = (from x in _context.Uruns select x.Marka).Distinct().Count().ToString();
            ViewBag.d6 = deger6;
            var deger7 = _context.Uruns.Count(x => x.Stok <= 20).ToString();
            ViewBag.d7 = deger7;
            var deger8 = (from x in _context.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
            ViewBag.d8 = deger8;
            var deger9 = (from x in _context.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();
            ViewBag.d9 = deger9;
            var deger10 = _context.Uruns.Count(x => x.UrunAd == "Buzdolabı").ToString();
            ViewBag.d10 = deger10;
            var deger11 = _context.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.d11 = deger11;
            var deger12 = _context.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.d12 = deger12;
            var deger13 = _context.Uruns.Where(u => u.UrunID == (_context.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(y => y.Count()).Select(z => z.Key).FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault();
            ViewBag.d13 = deger13;
            var deger14 = _context.SatisHarekets.Sum(x => x.ToplamTutar).ToString();
            ViewBag.d14 = deger14;
            DateTime bugün = DateTime.Today;
            var deger15 = _context.SatisHarekets.Count(x => x.Tarih == bugün).ToString();
            ViewBag.d15 = deger15;
            var deger16 = _context.SatisHarekets.Where(x => x.Tarih == bugün).Sum(y => (decimal?)y.ToplamTutar).ToString();
            ViewBag.d16 = deger16;

            return View();
        }
        public ActionResult KolayTablolar()
        {
            var sorgu = from x in _context.Carilers
                        group x by x.CariSehir into g
                        select new SinifGrup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };
            return View(sorgu.ToList());
        }
        public PartialViewResult Partial1()
        {
            var sorgu2 = from x in _context.Personels
                         group x by x.Departman.DepartmanAd into g
                         select new SinifGrup2
                         {
                             Departman = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu2.ToList());
        }
        public PartialViewResult Partial2()
        {
            var sorgu2 = _context.Carilers.ToList();
            return PartialView(sorgu2);
        }
        public PartialViewResult Partial3()
        {
            var sorgu3 = _context.Uruns.ToList();
            return PartialView(sorgu3);
        }
        public PartialViewResult Partial4()
        {
            var sorgu4 = from x in _context.Uruns
                         group x by x.Marka into g
                         select new SinifGrup3
                         {
                             marka = g.Key,
                             sayi = g.Count()
                         };
            return PartialView(sorgu4.ToList());
        }
    }
}