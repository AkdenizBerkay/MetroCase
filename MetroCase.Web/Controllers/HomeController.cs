using MetroCase.BusinessLayer;
using MetroCase.Common.Helper;
using MetroCase.Entities;
using MetroCase.Web.Models;
using MyEverNote.WebApp.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MetroCase.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home(int? id)
        {
            ViewBag.calljavascriptfunction2 = TempData["calljavascriptfunction2"];

            Manager<Urunler> nm = new Manager<Urunler>();
            List<Urunler> UrunList = new List<Urunler>();
            if (id == 0 || id == null)
            {
                UrunList = nm.GetAll().ToList();
            }
            else
            {
                UrunList = nm.GetAll(k => k.KategoriId == id).ToList();
            }
            return View(UrunList);
        }

        public ActionResult LogOut()
        {
            CurrentSession.Clear("loginuser");
            return RedirectToAction("Home");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            Validation<Kullanicilar> validate = new Validation<Kullanicilar>();
            Kullanicilar loginuser = new Kullanicilar();
            loginuser.Username = Username;

            if (!validate.IsUnique(loginuser, "username"))
            {
                ViewBag.username = Username;
                Manager<Kullanicilar> manager = new Manager<Kullanicilar>();
                loginuser = manager.Get(k => k.Username.Equals(loginuser.Username));
                if (Password.Equals(loginuser.Password))
                {
                    if (loginuser.IsActive)
                    {
                        CurrentSession.Set<Kullanicilar>("loginuser", loginuser);
                        return RedirectToAction("Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Lütfen hesabınızı aktivasyon mailinizdeki linkten aktive ediniz.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Yanlış şifre.");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Yanlış kullanıcı adı.");
                return View();
            }

        }
        public ActionResult UserActivate(Guid id)
        {
            Manager<Kullanicilar> mn = new Manager<Kullanicilar>();
            Kullanicilar actuser = mn.Get(k => k.ActivateGuid.Equals(id.ToString()));
            if (actuser != null && actuser.IsActive == false)
            {
                actuser.IsActive = true;
                mn.Update(actuser);
                return RedirectToAction("Login");
            }

            else if (actuser.IsActive)
            {
                return RedirectToAction("UserActivateCancel", new { user = actuser });
            }
            else if (actuser == null)
            {
                return RedirectToAction("UserActivateCancel", new { user = actuser });
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        public ActionResult UserActivateCancel(Kullanicilar user)
        {
            if (user == null)
            {

            }
            else if (user.IsActive)
            {

            }
            return View();
        }
        [MustLogin]
        public ActionResult UserProfile(int? id)
        {
            Kullanicilar user = null;
            if (id == null || id == 0)
            {
                user = CurrentSession.User;
            }
            else
            {
                Manager<Kullanicilar> UserBusiness = new Manager<Kullanicilar>();
                user = UserBusiness.GetById(id.Value);
                ViewBag.YazarId = id;
            }

            return View(user);
        }
        [MustLogin]
        [HttpPost]
        public ActionResult UserProfile(Kullanicilar user, HttpPostedFileBase profileImage)
        {
            Manager<Kullanicilar> mn = new Manager<Kullanicilar>();
            Kullanicilar dbuser = mn.Get(k => k.UserId == user.UserId);
            if (ModelState.IsValid)
            {
                Validation<Kullanicilar> validate = new Validation<Kullanicilar>();

                if (!validate.IsUnique(user, "username", true))
                {
                    ModelState.AddModelError("", "Kullanıcı adı mevcut");

                    return View(dbuser);
                }
                else if (!validate.IsUnique(user, "email", true))
                {
                    ModelState.AddModelError("", "Email adresi mevcut");
                    return View(dbuser);
                }
                else
                {
                    if (profileImage != null && (profileImage.ContentType.Equals("image/jpeg") || profileImage.ContentType.Equals("image/png") || profileImage.ContentType.Equals("image/jpg")))
                    {
                        string filename = $"user_{user.UserId}.{profileImage.ContentType.Split('/')[1]}";
                        profileImage.SaveAs(Server.MapPath($"~/Images/{filename}"));
                        user.ProfileImage = filename;
                    }

                    dbuser.Email = user.Email;
                    dbuser.Ad = user.Ad;
                    dbuser.Password = user.Password;
                    dbuser.ProfileImage = profileImage != null ? user.ProfileImage : dbuser.ProfileImage;
                    dbuser.Soyad = user.Soyad;
                    dbuser.Username = user.Username;
                    dbuser.Adres = user.Adres;
                    mn.Update(dbuser);

                    ViewBag.calljavascriptfunction = "Popup()";
                    CurrentSession.Set<Kullanicilar>("loginuser", dbuser);

                    return View(dbuser);
                }

            }
            else
            {
                return View(dbuser);
            }
        }
        public ActionResult Register()
        {
            Kullanicilar user = new Kullanicilar();
            return View(user);
        }
        [HttpPost]
        public ActionResult Register(Kullanicilar user, string RePassword)
        {
            Validation<Kullanicilar> validate = new Validation<Kullanicilar>();

            if (!user.Password.Equals(RePassword))
            {
                ModelState.AddModelError("RePassword", "Password alanı ile eşleşmiyor");
                return View();
            }
            else if (!validate.IsUnique(user, "username"))
            {
                ModelState.AddModelError("", "Kullanıcı adı mevcut");
                return View();
            }
            else if (!validate.IsUnique(user, "email"))
            {
                ModelState.AddModelError("", "Email adresi mevcut");
                return View();
            }
            else
            {
                Kullanicilar newuser = new Kullanicilar();
                newuser.IsAdmin = false;
                newuser.IsActive = false;
                newuser.ActivateGuid = Guid.NewGuid().ToString();
                newuser.Ad = user.Ad;
                newuser.Soyad = user.Soyad;
                newuser.Username = user.Username;
                newuser.Password = user.Password;
                newuser.Email = user.Email;
                newuser.Adres = user.Adres;
                Manager<Kullanicilar> manager = new Manager<Kullanicilar>();
                if (ModelState.IsValid)
                {

                    ViewBag.Name = newuser.Username;
                    ViewBag.calljavascriptfunction = "Popup()";
                    List<string> mailaddress = new List<string>();
                    mailaddress.Add(newuser.Email);

                    string SiteUri = ConfigHelper.Get<string>("SiteUri");
                    string ActivateUri = $"{SiteUri}/Home/UserActivate/{newuser.ActivateGuid}";
                    string img = "src='C:/Users/acer/documents/visual studio 2017/Projects/MetroCaseProject/MetroCase.Web/Images/mail.jpg'";

                    //string htmlstring = MailBodyHtml.GetHtml(ActivateUri,img);

                    StreamReader reader = new StreamReader(Server.MapPath("/Views/HtmlPage1.html"));

                    var html = reader.ReadToEnd();

                    html = html.Replace("{Url}", ActivateUri);

                    html = Regex.Replace(html, "src=\".+\"\\s", img);
                    //html = html.Replace("{img1}", img);


                    bool actmailresult = MailHelper.SendMail(html, mailaddress, "MetroCase Application Activation Link");
                    if (actmailresult)
                    {
                        manager.Add(newuser);
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kaydınız başarıyla gerçekleştirilememiştir.");
                        return View();
                    }
                }
                else
                {
                    return View(user);
                }
            }

        }
    }
}