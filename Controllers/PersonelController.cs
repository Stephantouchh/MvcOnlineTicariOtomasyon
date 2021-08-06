using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class PersonelController : Controller
    {
        // GET: Personel
        Context _context = new Context();
        
        public ActionResult Index()
        {
            var per = _context.Personels.Where(x => x.Durum == true).ToList();
            return View(per);
        }
        [HttpGet]
        public ActionResult YeniPersonel()
        {
            List<SelectListItem> deger1 = (from x in _context.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniPersonel(Personel pers)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                pers.PersonelGorsel = "/Image/" + dosyaadi + uzanti;
            }
            pers.Durum = true;
            _context.Personels.Add(pers);
            _context.SaveChanges();
            return RedirectToAction("PersonelListe");
        }
        public ActionResult PersonelSil(int id)
        {
            var pers = _context.Personels.Find(id);
            pers.Durum = false;
            _context.SaveChanges();
            return RedirectToAction("PersonelListe");
        }
        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger2 = (from x in _context.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;

            var pers = _context.Personels.Find(id);
            return View("PersonelGetir", pers);
        }
        public ActionResult PersonelGuncelle(Personel personel)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                personel.PersonelGorsel = "/Image/" + dosyaadi + uzanti;
            }
            var per = _context.Personels.Find(personel.Personelid);
            per.PersonelAd = personel.PersonelAd;
            per.PersonelSoyad = personel.PersonelSoyad;
            per.PersonelGorsel = personel.PersonelGorsel;
            per.PersonelMail = personel.PersonelMail;
            per.PersonelSehir = personel.PersonelSehir;
            per.PersonelTelefon = personel.PersonelTelefon;
            per.Departmanid = personel.Departmanid;
            per.Durum = true;
            _context.SaveChanges();
            return RedirectToAction("PersonelListe");
        }
        public ActionResult PersonelListe()
        {
            var sorgu = _context.Personels.ToList();
            return View(sorgu);
        }
    }
}