using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using PagedList;
using PagedList.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context _context = new Context();

        public ActionResult Index(int sayfa = 1)
        {
            var degerler = _context.Kategoris.Where(x => x.Durum == true).ToList().ToPagedList(sayfa, 5);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            if (!ModelState.IsValid)
            {
                return View("KategoriEkle");
            }
            k.Durum = true;
            _context.Kategoris.Add(k);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var ktg = _context.Kategoris.Find(id);
            ktg.Durum = false;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktg = _context.Kategoris.Find(id);
            return View("KategoriGetir", ktg);
        }
        public ActionResult KategoriGuncelle(Kategori p)
        {
            if (!ModelState.IsValid)
            {
                return View("KategoriGetir");
            }
            var ktg = _context.Kategoris.Find(p.KategoriID);
            ktg.KategoriAd = p.KategoriAd;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Deneme()
        {
            Class2 cs = new Class2();
            cs.Kategoriler = new SelectList(_context.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(_context.Uruns, "UrunID", "UrunAd");
            return View(cs);
        }
        public JsonResult UrunGetir(int p)
        {
            var urunler = (from x in _context.Uruns
                           join y in _context.Kategoris
                           on x.Kategori.KategoriID equals y.KategoriID
                           where x.Kategori.KategoriID == p
                           select new
                           {
                               Text = x.UrunAd,
                               Value = x.UrunID.ToString()
                           }).ToList();
            return Json(urunler, JsonRequestBehavior.AllowGet);
        }
    }
}