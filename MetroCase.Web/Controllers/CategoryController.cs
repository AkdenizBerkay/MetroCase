using MetroCase.BusinessLayer;
using MetroCase.Entities;
using MyEverNote.WebApp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroCase.Web.Controllers
{
    [MustAdmin]
    public class CategoryController : Controller
    {
        Manager<Kategoriler> KategorilerBusiness = new Manager<Kategoriler>();

        public ActionResult CategoryIslemleri()
        {
            ViewBag.calljavascriptfunction = TempData["calljavascriptfunction"];
            List<Kategoriler> CategoryList = new List<Kategoriler>();
            CategoryList = KategorilerBusiness.GetAll().ToList();
            if (CategoryList.Count > 0)
            {
                return View(CategoryList);
            }
            else
            {
                return View();
            }
        }

        public ActionResult CategoryAdd(string txt, string ack)
        {
            Kategoriler newcat = new Kategoriler();
            newcat.KategoriAciklama = ack != string.Empty ? ack : "";
            newcat.KategoriAdi = txt != string.Empty ? txt : "";
            KategorilerBusiness.Add(newcat);

            TempData["calljavascriptfunction"] = "Popup()";
            return RedirectToAction("CategoryIslemleri", "Category");
        }

        public ActionResult CategoryEdit(int id, string txt, string ack)
        {
            Kategoriler dbcat = KategorilerBusiness.Get(k => k.KategoriId == id);

            if (dbcat != null)
            {
                dbcat.KategoriAdi = txt;
                dbcat.KategoriAciklama = ack;
                KategorilerBusiness.Update(dbcat);
            }
            TempData["calljavascriptfunction"] = "Popup()";
            return RedirectToAction("CategoryIslemleri", "Category");
        }
        public ActionResult CategoryDelete(int id)
        {
            Kategoriler dbcat = KategorilerBusiness.Get(k => k.KategoriId == id);

            if (dbcat != null)
            {
                KategorilerBusiness.Delete(dbcat);
            }
            TempData["calljavascriptfunction"] = "Popup()";
            return RedirectToAction("CategoryIslemleri", "Category");
        }
        //public ActionResult AddorEdit(int? KategoriId, bool? adminpanel)
        //{
        //    Kategoriler pro = new Kategoriler();

        //    if (KategoriId != null)
        //    {
        //        pro = KategorilerBusiness.GetById(KategoriId.Value);

        //        if (pro == null)
        //        {
        //            return HttpNotFound();
        //        }
        //    }

        //    return View(pro);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddorEdit(Kategoriler product, HttpPostedFileBase Image)
        //{
        //    Urunler dbpro = new Urunler();


        //    if (ModelState.IsValid)
        //    {
        //        if (Image != null && (Image.ContentType.Equals("image/jpeg") || Image.ContentType.Equals("image/png") || Image.ContentType.Equals("image/jpg")))
        //        {
        //            string filename = $"product_{product.UrunAdi}.{Image.ContentType.Split('/')[1]}";
        //            Image.SaveAs(Server.MapPath($"~/Images/{filename}"));
        //            product.UrunFoto = filename;
        //        }

        //        if (product.UrunId != 0)
        //        {
        //            dbpro = UrunlerBusiness.Get(k => k.UrunId == product.UrunId);

        //            dbpro.KategoriId = product.KategoriId;
        //            dbpro.UrunAciklama = product.UrunAciklama;
        //            dbpro.UrunAdi = product.UrunAdi;
        //            dbpro.UrunFiyat = product.UrunFiyat;
        //            dbpro.UrunFoto = Image != null ? product.UrunFoto : dbpro.UrunFoto;
        //            dbpro.UrunStokAdedi = product.UrunStokAdedi;

        //            UrunlerBusiness.Update(dbpro);
        //        }
        //        else
        //        {
        //            dbpro.KategoriId = product.KategoriId;
        //            dbpro.UrunAciklama = product.UrunAciklama;
        //            dbpro.UrunAdi = product.UrunAdi;
        //            dbpro.UrunFiyat = product.UrunFiyat;
        //            dbpro.UrunFoto = Image != null ? product.UrunFoto : dbpro.UrunFoto;
        //            dbpro.UrunStokAdedi = product.UrunStokAdedi;

        //            UrunlerBusiness.Add(dbpro);


        //        }
        //        ViewBag.calljavascriptfunction = "Popup()";
        //        ViewBag.Kategoriler = TempData["Kategoriler"];
        //        TempData["Kategoriler"] = ViewBag.Kategoriler;
        //        return View(dbpro);

        //    }
        //    else
        //    {
        //        return View(product);
        //    }
        //}

        //public ActionResult Delete(int? UrunId, bool? adminpanel)
        //{

        //    ViewBag.Kategoriler = CategoryList;
        //    TempData["Kategoriler"] = ViewBag.Kategoriler;

        //    ViewBag.id = UrunId.Value;

        //    if (UrunId == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Urunler pro = UrunlerBusiness.GetById(UrunId.Value);
        //    if (pro == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(pro);
        //}

        //public ActionResult DeletePost(string id)
        //{
        //    Urunler dpro = UrunlerBusiness.GetById(Convert.ToInt32(id));

        //    UrunlerBusiness.Delete(dpro);

        //    TempData["calljavascriptfunction"] = "Popup()";

        //    return RedirectToAction("UrunIslemleri");
        //}
    }
}