using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; //klasa ActionResult
using System.Web.Security; //klasa FormsAuthentication
using TA.Models;

namespace TA.Controllers
{
    public class LoginController : Controller
    {
        //prikaz stranice za login
        public ActionResult Login()
        {
            return View();
        }

        //provera podataka unetih preko forme i redirekcija na odredenu stranicu
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            Korisnik korisnik = LoginKlasa.ProveriKorisnika(model);
            if (korisnik != null)
            {
                if (korisnik.Uloga1.NazivUloge == "admin")
                {
                    Session["korisnik"] = korisnik.Uloga1.NazivUloge;
                    return RedirectToAction("AranzmaniDodajView", "Admin");
                }
                else if (korisnik.Uloga1.NazivUloge == "radnik")
                {
                    Session["korisnik"] = korisnik.Uloga1.NazivUloge;
                    return RedirectToAction("RadnikMainView", "Radnik");
                }
                else
                {
                    if (LoginKlasa.DaLiPostojiRezervacijaKlijenta(model))
                    {
                        Session["korisnik"] = korisnik.KorisnickoIme;
                        return RedirectToAction("KlijentView", "Klijent", new { id = korisnik.KorisnikID });
                    }
                    else
                    {
                        ViewBag.LoginGreska = "Korisničko ime ili lozinka nisu validni!";
                        return View();
                    }
                }
            }
            else
            {
                ViewBag.LoginGreska = "Korisničko ime ili lozinka nisu validni!";
                return View();
            }
        }
        
        //odjava
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}