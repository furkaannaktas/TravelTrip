using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelTripProje.Models.Siniflar;

namespace TravelTripProje.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        Context c = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var degerler = c.Blogs.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniBlog()
        {
            return View();
        }
        [HttpPost]  
        public ActionResult YeniBlog(Blog p)
        {
            c.Blogs.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BlogSil(int id)
        {
            var b = c.Blogs.Find(id);
            c.Blogs.Remove(b);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BlogGetir(int id)
        {
            var bl = c.Blogs.Find(id);
            return View("BlogGetir", bl);
        }
        public ActionResult BlogGuncelle(Blog b)
        {
            var blg = c.Blogs.Find(b.ID);
            if (blg != null)
            {
                blg.Aciklama = b.Aciklama;
                blg.Baslik = b.Baslik;
                blg.BlogImage = b.BlogImage;

                // Tarih kontrolü
                if (b.Tarih == null || b.Tarih < new DateTime(1753, 1, 1))
                {
                    blg.Tarih = DateTime.Now; // Varsayılan tarih olarak bugünü kullan
                }
                else
                {
                    blg.Tarih = b.Tarih;
                }

                c.SaveChanges();
            }
            return RedirectToAction("Index");
        }
         public ActionResult YorumListesi()
        {
           var yorumlar = c.Yorumlars.ToList();
            return View(yorumlar);
        }
        public ActionResult YorumSil(int id)
        {
            var b = c.Yorumlars.Find(id);
            c.Yorumlars.Remove(b);
            c.SaveChanges();
            return RedirectToAction("YorumListesi");
        }
        public ActionResult YorumGetir(int id)
        {
            var yr = c.Yorumlars.Find(id);
            return View("YorumGetir", yr);
        }
        public ActionResult YorumGuncelle(Yorumlar y)
        {
            var yrm = c.Yorumlars.Find(y.ID);

            if (yrm != null)
            {
                // Yorumları, kullanıcı adı, mail, yorum alanlarını yeni değerlerle güncelle
                yrm.KullaniciAdi = y.KullaniciAdi;
                yrm.Mail = y.Mail;
                yrm.Yorum = y.Yorum;

                // Veritabanındaki değişiklikleri kaydet
                c.SaveChanges();
            }

            // Güncelleme işlemi başarılı olduğunda YorumListesi sayfasına yönlendir
            return RedirectToAction("YorumListesi");
        }

    }
}