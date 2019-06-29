using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; //klasa ActionResult
using TA.Models;

namespace TA.Controllers
{
    public class HomeController : Controller
    {
        //GET: otvara se pocetna stranica sa spiskom ponuda za jednu zemlju
        public ActionResult Index()
        {
            List<Aranzman> ponude = Home.SpisakPonudaInicijalni();
            ViewData["nazivZemlje"] = ponude.First().Hotel.Destinacija.Zemlja;
            return View("Index", ponude);
        }

        //GET: otvara se stranica sa ponudom izabrane zemlje
        public ActionResult PrikaziPonudu(string zemlja)
        {
            List<Aranzman> ponude = Home.SpisakPonudaNaKlik(zemlja);
            ViewData["nazivZemlje"] = zemlja;
            return View("Index", ponude);
        }
    }
}