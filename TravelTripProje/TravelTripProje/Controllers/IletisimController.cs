using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelTripProje.Models.Siniflar;

namespace TravelTripProje.Controllers
{
    public class IletisimController : Controller
    {
        // Veritabanı bağlamı (Context)
        Context c = new Context();

        // GET: İletisim
        public ActionResult Index()
        {
            return View();
        }

        // İletişim formunu gönderen metot
        [HttpPost]
        public ActionResult MesajGonder(string AdSoyad, string Konu, string Mesaj)  // AdSoyad, Konu ve Mesaj parametre olarak alınır
        {
            if (!string.IsNullOrEmpty(AdSoyad) && !string.IsNullOrEmpty(Konu) && !string.IsNullOrEmpty(Mesaj))  // Formun tüm alanları dolu olmalı
            {
                // Yeni bir iletisim nesnesi oluşturuluyor
                iletisim yeniMesaj = new iletisim()
                {
                    AdSoyad = AdSoyad,
                    Konu = Konu,
                    Mesaj = Mesaj
                };

                // Veritabanına ekleme işlemi
                c.iletisims.Add(yeniMesaj);  // Yeni mesaj veritabanına eklenir
                c.SaveChanges();  // Veritabanına kaydedilir

                // Kullanıcıya teşekkür mesajı
                ViewBag.Message = "Mesajınız başarıyla gönderildi. Teşekkür ederiz!";
                return View("Index");
            }

            // Eğer formda herhangi bir alan boş ise, form tekrar gösterilir.
            ViewBag.Message = "Lütfen tüm alanları doldurduğunuzdan emin olun.";
            return View("Index");
        }
    }
}
