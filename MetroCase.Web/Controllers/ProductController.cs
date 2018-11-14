using MetroCase.BusinessLayer;
using MetroCase.Entities;
using MetroCase.Web.Models;
using MyEverNote.WebApp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroCase.Web.Controllers
{
    public class ProductController : Controller
    {
        Manager<Urunler> mu = new Manager<Urunler>();

        // GET: Product
        public ActionResult UrunDetay(int id)
        {
            Urunler urun = mu.GetById(id);
            return View(urun);
        }
        [MustLogin]
        public ActionResult SepeteEkle(int id)
        {
            Urunler urun = mu.GetById(id);
            if (Session["Urunlerim"] == null)
            {
                List<Urunler> UrunList = new List<Urunler>();
                UrunList.Add(urun);
                Session["Urunlerim"] = UrunList;
            }
            else
            {
                List<Urunler> Urunlerim = (List<Urunler>)Session["Urunlerim"];
                Urunlerim.Add(urun);
                Session["Urunlerim"] = Urunlerim;
            }

            TempData["calljavascriptfunction2"] = "Sepet()";
            return RedirectToAction("Home","Home");
        }
        [MustLogin]
        public ActionResult SepetIslemleri()
        {
            if (Session["Urunlerim"] != null)
            {
                List<Urunler> Urunlerim = (List<Urunler>)Session["Urunlerim"];
                return View(Urunlerim);
            }
            return RedirectToAction("Home", "Home");
        }
        [MustLogin]
        public ActionResult SepetDelete(int id)
        {
            if (Session["Urunlerim"] != null)
            {
                List<Urunler> Urunlerim = (List<Urunler>)Session["Urunlerim"];
                Urunler silurun = new Urunler();
                foreach (Urunler u in Urunlerim)
                {
                    if (u.UrunId == id)
                    {
                        silurun = u;
                        break;
                    }
                }
                Urunlerim.Remove(silurun);
                Session["Urunlerim"] = Urunlerim;
                TempData["calljavascriptfunction"] = "Popup()";
                return RedirectToAction("SepetIslemleri", "Product");
            }
            return RedirectToAction("Home", "Home");
        }
    }
}