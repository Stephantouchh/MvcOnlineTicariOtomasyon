using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context _context = new Context();

        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = _context.Mesajlars.Where(x => x.Alıcı == mail).OrderByDescending(x => x.MesajID).ToList();
            ViewBag.m = mail;
            var mailid = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            ViewBag.mid = mailid;
            var toplamsatis = _context.SatisHarekets.Where(x => x.Cariid == mailid).Count();
            ViewBag.toplamsatis = toplamsatis;
            var toplamtutar = _context.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => (decimal?)y.ToplamTutar);
            ViewBag.toplamtutar = toplamtutar;
            var toplamurun = _context.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => (decimal?)y.Adet);
            ViewBag.tplmurnys = toplamurun;
            var adsoyad = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var gorsel = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariGorsel).FirstOrDefault();
            ViewBag.gorsel = gorsel;
            var meslek = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariMeslek).FirstOrDefault();
            ViewBag.meslek = meslek;
            var telefon = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariTelefon).FirstOrDefault();
            ViewBag.telefon = telefon;
            var sehir = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariSehir).FirstOrDefault();
            ViewBag.sehir = sehir;
            return View(degerler);
        }
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var adsoyad = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var id = _context.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.Cariid).FirstOrDefault();
            var degerler = _context.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }
        public ActionResult CariLogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult GelenMesajlar()
        {
            string mail = (string)Session["CariMail"];
            var adsoyad = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var mesajlar = _context.Mesajlars.Where(x => x.Alıcı == mail).OrderByDescending(x => x.MesajID).ToList();
            return View(mesajlar);
        }
        public ActionResult GidenMesajlar()
        {
            string mail = (string)Session["CariMail"];
            var adsoyad = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var mesajlar = _context.Mesajlars.Where(x => x.Gönderici == mail).OrderByDescending(x => x.MesajID).ToList();
            return View(mesajlar);
        }
        public ActionResult MesajDetay(int id)
        {
            string mail = (string)Session["CariMail"];
            var adsoyad = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var detay = _context.Mesajlars.Where(x => x.MesajID == id).ToList();
            return View(detay);
        }
        public PartialViewResult SolMenu()
        {
            string p = (string)Session["CariMail"];
            var mail = _context.Carilers.Where(x => x.CariMail == p).Select(y => y.Cariid).FirstOrDefault();

            var alc = _context.Mesajlars.Count(m => m.Alıcı == p).ToString();
            ViewBag.alici = alc;

            var gndrci = _context.Mesajlars.Count(m => m.Gönderici == p).ToString();
            ViewBag.gndrn = gndrci;

            //var contact = _context.Contacts.Count().ToString();
            //ViewBag.contact = contact;

            //var draft = _context.Drafts.Count().ToString();
            //ViewBag.draft = draft;

            //var readMessage = messagemanager.GetList().Count();
            //ViewBag.readMessage = readMessage;

            //var unReadMessage = messagemanager.GetListUnRead().Count();
            //ViewBag.unReadMessage = unReadMessage;

            return PartialView();
        }
        public PartialViewResult Partial()
        {
            var mail = (string)Session["CariMail"];
            var id = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            var crbl = _context.Carilers.Find(id);
            return PartialView("Partial", crbl);
        }
        public PartialViewResult Partial2()
        {
            var veriler = _context.Mesajlars.Where(x => x.Gönderici == "Admin").ToList();
            return PartialView(veriler);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            string mail = (string)Session["CariMail"];
            var adsoyad = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar mesaj)
        {
            string mail = (string)Session["CariMail"];
            mesaj.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            mesaj.Gönderici = mail;
            _context.Mesajlars.Add(mesaj);
            _context.SaveChanges();
            return RedirectToAction("GidenMesajlar", "CariPanel");
        }
        public ActionResult KargoTakip(string p)
        {
            string mail = (string)Session["CariMail"];
            var adsoyad = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var k = from x in _context.KargoDetays select x;
            k = k.Where(y => y.TakipKodu.Contains(p));
            return View(k.ToList());
        }
        public ActionResult CariKargoTakip(string id)
        {
            string mail = (string)Session["CariMail"];
            var adsoyad = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var degerler = _context.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult KargoQr(int id)
        {
            string mail = (string)Session["CariMail"];
            var adsoyad = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var deger2 = _context.KargoDetays.Find(id);
            ViewBag.dgr2 = deger2.TakipKodu;
            return View();
        }
        [HttpPost]
        public ActionResult KargoQr(string kod)
        {
            string mail = (string)Session["CariMail"];
            var adsoyad = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator koduret = new QRCodeGenerator();
                QRCodeGenerator.QRCode karekod = koduret.CreateQrCode(kod, QRCodeGenerator.ECCLevel.Q);
                using (Bitmap resim = karekod.GetGraphic(10))
                {
                    resim.Save(ms, ImageFormat.Png);
                    ViewBag.karekodimage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return View();
        }
        public ActionResult CariBilgiGuncelle(Cariler cari)
        {
            var cariler = _context.Carilers.Find(cari.Cariid);
            cariler.CariAd = cari.CariAd;
            cariler.CariSoyad = cari.CariSoyad;
            cariler.CariSifre = cari.CariSifre;
            cariler.CariSehir = cari.CariSehir;
            cariler.CariTelefon = cari.CariTelefon;
            cariler.CariMeslek = cari.CariMeslek;
            cariler.CariGorsel = cari.CariGorsel;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}