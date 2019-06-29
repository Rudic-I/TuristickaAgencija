using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; //klasa ActionResult
using TA.Models;

namespace TA.Controllers
{
    public class RadnikController : Controller
    {
        // GET: aranzmani, korisnici, rezervacije
        public ActionResult RadnikMainView()
        {
            return View();
        }

        public ActionResult KorisniciPrikaziView()
        {
            return View();
        }

        public ActionResult RezervacijePrikaziView()
        {
            return View();
        }

        //prikaz stranice za rezervaciju izabranog aranzmana
        //ukoliko ima slobodnog mesta, odlazi se na pogled sa formom za pravljenje rezervacije
        //ukoliko nema ispisuje se poruka o gresci
        public ActionResult AranzmanRezervisi(int sifra)
        {
            //metoda RezervisiAranzmanPoSifri vraca true ako ima slobodnih mesta
            if(Radnik.DaLiJeMoguceRezervisati(sifra))
            {
                RezervacijaViewModel rezervacija = new RezervacijaViewModel();
                rezervacija.AranzmanID = sifra;
                ViewBag.Greska = null;
                return View("AranzmanRezervisiView", rezervacija);
            }
            else
            {
                ViewBag.Greska = "Kapaciteti za ovaj aranžman su popunjeni.";
                return View("AranzmanRezervisiView");
            }
        }

        //pravljenje nove rezervacije i dodavanje novog korisnika (klijenta)
        [HttpPost]
        public ActionResult AranzmanRezervisi(FormCollection r)
        {
            Rezervacija rezervacija = new Rezervacija();
            rezervacija.AranzmanID = Convert.ToInt32(r["AranzmanID"]);
            rezervacija.RezervacijaID = Convert.ToInt32(r["RezervacijaID"]);
            rezervacija.KorisnikID = Convert.ToInt32(r["RezervacijaID"]);
            rezervacija.Aktivan = 1;

            Korisnik korisnik = new Korisnik();
            korisnik.Ime = r["Ime"];
            korisnik.KorisnickoIme = r["KorisnickoIme"];
            korisnik.Lozinka = r["RezervacijaID"];
            korisnik.KorisnikID = Convert.ToInt32(r["RezervacijaID"]);
            korisnik.Uloga = 3;

            Radnik.DodajKlijenta(korisnik);

            string uspeh = Radnik.DodajRezervaciju(rezervacija);
            switch (uspeh)
            {
                case "uspeh":
                    TempData["rezervisan"] = "Rezervacija je uspešno dodata.";
                    return RedirectToAction("RezervacijePrikaziView");
                case "sifra":
                    TempData["PogresanID"] = "Već postoji rezervacija sa tom šifrom.";
                    return RedirectToAction("AranzmanRezervisi", new { sifra = Convert.ToInt32(r["AranzmanID"]) });
                case "opsti":
                    TempData["PogresanID"] = "Greška u sistemu. Pokušajte ponovo";
                    return RedirectToAction("AranzmanRezervisi", new { sifra = Convert.ToInt32(r["AranzmanID"]) });
                default:
                    TempData["PogresanID"] = "Greška u sistemu. Pokušajte ponovo";
                    return RedirectToAction("AranzmanRezervisi", new { sifra = Convert.ToInt32(r["AranzmanID"]) });
            }  
        }

        //brisanje / otkazivanje rezervacije
        public ActionResult RezervacijeObrisi(int sifra)
        {
            TempData["Obrisan"] = Radnik.ObrisiRezervacijuPoSifri(sifra);
            return RedirectToAction("RezervacijePrikaziView");
        }
    }
}