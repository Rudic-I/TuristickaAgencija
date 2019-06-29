using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; //klasa ActionResult
using TA.Models;

namespace TA.Controllers
{
    public class KlijentController : Controller
    {
        // GET: otvara se stranica sa podacima o aranzmanu koji je klijent rezervisao
        public ActionResult KlijentView(int id)
        {
            AranzmanViewModel prikazi = null;
            prikazi = Klijent.PrikaziRezervacijuPoSifri(id);
            return View("KlijentView", prikazi);
        }
    }
}