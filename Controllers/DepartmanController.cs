using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context _context = new Context();
        
        public ActionResult Index()
        {
            var departman = _context.Departmans.Where(x => x.Durum == true).ToList();
            return View(departman);
        }
        [HttpGet]
        public ActionResult YeniDepartman()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniDepartman(Departman departman)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniDepartman");
            }
            departman.Durum = true;
            _context.Departmans.Add(departman);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanSil(int id)
        {
            var dpr = _context.Departmans.Find(id);
            dpr.Durum = false;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanGetir(int id)
        {
            var dpr = _context.Departmans.Find(id);
            return View("DepartmanGetir", dpr);
        }
        public ActionResult DepartmanGuncelle(Departman departman)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanGetir");
            }
            var dpr = _context.Departmans.Find(departman.Departmanid);
            dpr.DepartmanAd = departman.DepartmanAd;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanDetay(int id)
        {
            var degerler = _context.Personels.Where(x => x.Departmanid == id).ToList();
            var dpt = _context.Departmans.Where(x => x.Departmanid == id).Select(y => y.DepartmanAd).FirstOrDefault();
            ViewBag.ad = dpt;
            return View(degerler);
        }
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var dpt = _context.Personels.Where(x => x.Personelid == id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.ad = dpt;
            var degerler = _context.SatisHarekets.Where(x => x.Personelid == id).ToList();
            return View(degerler);
        }
    }
}