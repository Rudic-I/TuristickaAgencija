using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc; //klasa ActionResult
using TA.Models;

namespace TA.Controllers
{
    public class AdminController : Controller
    {
        // poziva metodu iz modela koja vraca listu popunjenu podacima iz baze
        // smesta u viewbag koji se poziva na pogledu u ddl
        private void PopuniSifreDestinacija()
        {
            SelectList items = Admin.PopuniListuDestinacijaID();
            if (items != null)
            {
                ViewBag.destinacije = items;
            }
        }

        private void PopuniListuHotela()
        {
            SelectList items = Admin.PopuniHotele();
            if (items != null)
            {
                ViewBag.hoteli = items;
            }
        }

        private void PopuniListuUsluga()
        {
            SelectList items = Admin.PopuniUsluge();
            if (items != null)
            {
                ViewBag.usluge = items;
            }
        }

        private void PopuniListuTermina()
        {
            SelectList items = Admin.PopuniTermine();
            if (items != null)
            {
                ViewBag.termini = items;
            }
        }

        // GET: destinacije, hoteli, aranzmani, aranzman po sifri
        public ActionResult AdminDestinacijeView()
        {
            return View();
        }

        public ActionResult AdminHoteliView()
        {
            PopuniSifreDestinacija();
            return View();
        }

        public ActionResult AranzmaniDodajView()
        {
            PopuniListuHotela();
            PopuniListuUsluga();
            PopuniListuTermina();
            return View();
        }

        public ActionResult AranzmanPrikaziPoSifri(int sifra)
        {
            AranzmanViewModel prikazi = Admin.PrikaziAranzmanPoSifri(sifra);
            return View("AranzmanPrikaziPoSifriView", prikazi);
        }

        // POST: putem forme dodavanje destinacija, hotela, aranzmana i izmena aranzmana
        [HttpPost]
        public ActionResult DodajDestinaciju(FormCollection destinacija)
        {
            if (ModelState.IsValid)
            {
                string zemlja = destinacija["Zemlja"];
                string grad = destinacija["Grad"];
                bool uspeh = Admin.DodajDestinaciju(zemlja, grad);
                if (uspeh)
                {
                    TempData["dodataDestinacija"] = "Destinacija je uspešno dodata";
                    return RedirectToAction("AdminDestinacijeView");
                }
                else
                {
                    TempData["postojiDestinacija"] = "Ova destinacija već postoji u bazi";
                    return RedirectToAction("AdminDestinacijeView");
                }
            }
            else
            {
                return View("AdminDestinacijeView");
            }
        }

        [HttpPost]
        public ActionResult DodajHotel(FormCollection hotel)
        {
            if (ModelState.IsValid)
            {
                int destinacijaID = int.Parse(hotel["listaDestinacijaID"]);
                string naziv = hotel["Naziv"];
                int brojZvezdica = int.Parse(hotel["BrojZvezdica"]);
                bool uspeh = Admin.DodajHotel(destinacijaID, naziv, brojZvezdica);
                if (uspeh)
                {
                    TempData["dodatHotel"] = "Hotel je uspešno dodat";
                    return RedirectToAction("AdminHoteliView");
                }
                else
                {
                    TempData["postojiHotel"] = "Ovaj hotel već postoji u bazi";
                    return RedirectToAction("AdminHoteliView");
                }
            }
            else
            {
                //ViewBag.Greska = "Podaci nisu dobro uneseni";
                return View();
            }
        }

        [HttpPost]
        public ActionResult AranzmaniDodajView(FormCollection aranzman)
        {
            PopuniListuHotela();
            PopuniListuUsluga();
            PopuniListuTermina();

            int hotelID = Convert.ToInt32(aranzman["listaHotela"]);
            int uslugaID = Convert.ToInt32(aranzman["listaUsluga"]);
            int terminID = Convert.ToInt32(aranzman["listaTermina"]);
            int cena = Convert.ToInt32(aranzman["Cena"]);
            int raspolozivost = Convert.ToInt32(aranzman["Raspolozivost"]);
            Admin.DodajAranzman(hotelID, uslugaID, terminID, cena, raspolozivost);
            TempData["aranzmanDodat"] = "Aranžman je uspešno dodat";

            return RedirectToAction("AranzmaniDodajView");
        }

        [HttpPost]
        public ActionResult AranzmanIzmeni(FormCollection aranzman)
        {
                Aranzman nov = new Aranzman();
                nov.AranzmanID = Convert.ToInt32(aranzman["SifraAranzmana"]);
                nov.Cena = Convert.ToInt32(aranzman["Cena"]);
                nov.Raspolozivost = Convert.ToInt32(aranzman["Raspolozivost"]);
                //nov.Aktivan = Convert.ToInt32(aranzman["Aktivan"]);
                Admin.IzmeniAranzman(nov);
                TempData["Izmena"] = "Aranžman je uspešno izmenjen.";
                return RedirectToAction("AranzmaniDodajView");
        }

        public ActionResult AranzmanObrisi(int sifra)
        {
            if (Admin.DaLiJeMoguceObrisatiAranzman(sifra))
            {
                Admin.ObrisiAranzmanPoSifri(sifra);
                TempData["Obrisan"] = "Aranžman je uspešno obrisan.";
                return RedirectToAction("AranzmaniDodajView");
            }
            else
            {
                TempData["greskaUBrisanju"] = "Za ovaj aranžman već postoje rezervacije i ne može da bude obrisan!";
                return RedirectToAction("AranzmaniDodajView");
            }
        }
    }
}