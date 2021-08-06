using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class UrunDetayController : Controller
    {
        // GET: UrunDetay
        Context _context = new Context();
       
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            //var degerler = _context.Uruns.Where(x => x.UrunID == 1).ToList();
            cs.Deger1 = _context.Uruns.Where(x => x.UrunID == 1).ToList();
            cs.Deger2 = _context.Detays.Where(y => y.DetayID == 1).ToList();
            return View(cs);
        }
    }
}