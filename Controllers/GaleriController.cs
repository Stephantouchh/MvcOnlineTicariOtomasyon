using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class GaleriController : Controller
    {
        // GET: Galeri
        Context _context = new Context();
      
        public ActionResult Index()
        {
            var degerler = _context.Uruns.ToList();
            return View(degerler);
        }
    }
}