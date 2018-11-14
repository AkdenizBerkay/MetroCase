using MetroCase.BusinessLayer;
using MetroCase.Entities;
using MyEverNote.WebApp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MetroCase.Web.Controllers
{
    [MustAdmin]
    public class AdminController : Controller
    {
        Manager<Urunler> UrunlerBusiness = new Manager<Urunler>();
        Manager<Kategoriler> KategorilerBusiness = new Manager<Kategoriler>();
        List<Kategoriler> Categories = new List<Kategoriler>();
        List<SelectListItem> CategoryList = new List<SelectListItem>();

        public AdminController()
        {
           Categories = KategorilerBusiness.GetAll().ToList();
            foreach (Kategoriler c in Categories)
            {
                CategoryList.Add(new SelectListItem { Text = c.KategoriAdi, Value = c.KategoriId.ToString() });
            }
        }


        public ActionResult UrunIslemleri()
        {
            ViewBag.calljavascriptfunction = TempData["calljavascriptfunction"];
            List<Urunler> UrunList = new List<Urunler>();
            UrunList = UrunlerBusiness.GetAll().ToList();
            if (UrunList.Count > 0)
            {
                return View(UrunList);
            }
            else
            {
                return View();
            }
        }
   
        public ActionResult AddorEdit(int? UrunId, bool? adminpanel)
        {
            Urunler pro = new Urunler();

            if (UrunId != null)
            {
                pro = UrunlerBusiness.GetById(UrunId.Value);

                if (pro == null)
                {
                    return HttpNotFound();
                }
            }
            //List<SelectListItem> CategoryList = new List<SelectListItem>();
            //List<Kategoriler> Categories = KategorilerBusiness.GetAll().ToList();
            //foreach (Kategoriler c in Categories)
            //{
            //    CategoryList.Add(new SelectListItem { Text = c.KategoriAdi, Value = c.KategoriId.ToString() });
            //}
            ViewBag.Kategoriler = CategoryList;
            TempData["Kategoriler"] = ViewBag.Kategoriler;
            return View(pro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddorEdit(Urunler product, HttpPostedFileBase Image)
        {
            Urunler dbpro = new Urunler();


            if (ModelState.IsValid)
            {
                if (Image != null && (Image.ContentType.Equals("image/jpeg") || Image.ContentType.Equals("image/png") || Image.ContentType.Equals("image/jpg")))
                {
                    string filename = $"product_{product.UrunAdi}.{Image.ContentType.Split('/')[1]}";
                    Image.SaveAs(Server.MapPath($"~/Images/{filename}"));
                    product.UrunFoto = filename;
                }

                if (product.UrunId != 0)
                {
                    dbpro = UrunlerBusiness.Get(k => k.UrunId == product.UrunId);

                    dbpro.KategoriId = product.KategoriId;
                    dbpro.UrunAciklama = product.UrunAciklama;
                    dbpro.UrunAdi = product.UrunAdi;
                    dbpro.UrunFiyat = product.UrunFiyat;
                    dbpro.UrunFoto = Image != null ? product.UrunFoto : dbpro.UrunFoto;
                    dbpro.UrunStokAdedi = product.UrunStokAdedi;

                    UrunlerBusiness.Update(dbpro);
                }
                else
                {
                    dbpro.KategoriId = product.KategoriId;
                    dbpro.UrunAciklama = product.UrunAciklama;
                    dbpro.UrunAdi = product.UrunAdi;
                    dbpro.UrunFiyat = product.UrunFiyat;
                    dbpro.UrunFoto = Image != null ? product.UrunFoto : dbpro.UrunFoto;
                    dbpro.UrunStokAdedi = product.UrunStokAdedi;

                    UrunlerBusiness.Add(dbpro);


                }
                ViewBag.calljavascriptfunction = "Popup()";
                ViewBag.Kategoriler = TempData["Kategoriler"];
                TempData["Kategoriler"] = ViewBag.Kategoriler;
                return View(dbpro);

            }
            else
            {
                return View(product);
            }
        }

        public ActionResult Delete(int? UrunId, bool? adminpanel)
        {

            ViewBag.Kategoriler = CategoryList;
            TempData["Kategoriler"] = ViewBag.Kategoriler;

            ViewBag.id = UrunId.Value;

            if (UrunId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler pro = UrunlerBusiness.GetById(UrunId.Value);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }

        public ActionResult DeletePost(string id)
        {
            Urunler dpro = UrunlerBusiness.GetById(Convert.ToInt32(id));

            UrunlerBusiness.Delete(dpro);

            TempData["calljavascriptfunction"] = "Popup()";

            return RedirectToAction("UrunIslemleri");
        }

        public JsonResult CategoryDropBox()
        {
            Manager<Kategoriler> CategoryBusiness = new Manager<Kategoriler>();
            List<Kategoriler> categories = CategoryBusiness.GetAll().ToList();
            List<string> titles = new List<string>();
            foreach (Kategoriler cat in categories)
            {
                titles.Add(cat.KategoriAdi);
            }
            return Json(titles);
        }
    }
}