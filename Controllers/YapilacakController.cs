using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class YapilacakController : Controller
    {
        // GET: Yapilacak
        Context _context = new Context();
       
        public ActionResult Index()
        {
            var deger1 = _context.Carilers.Count().ToString();
            ViewBag.d1 = deger1;
            var deger2 = _context.Uruns.Count().ToString();
            ViewBag.d2 = deger2;
            var deger3 = _context.Kategoris.Count().ToString();
            ViewBag.d3 = deger3;
            var deger4 = (from x in _context.Carilers select x.CariSehir).Distinct().Count().ToString();
            ViewBag.d4 = deger4;

            var yapilacaklar = _context.Yapilacaks.ToList();
            return View(yapilacaklar);
        }
    }
}