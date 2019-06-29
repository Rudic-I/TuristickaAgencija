using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TA.Models
{
    public class Klijent
    {
        //vraca podatke o odredenoj rezervaciji / aranzmanu
        public static AranzmanViewModel PrikaziRezervacijuPoSifri(int id)
        {
            TuristickaAgencijaEntities db = new TuristickaAgencijaEntities();
            AranzmanViewModel rezervacija = null;
            try
            {
                rezervacija = (from a in db.Aranzmen
                               join r in db.Rezervacijas on a.AranzmanID equals r.AranzmanID
                               where r.KorisnikID == id
                               select new AranzmanViewModel { Destinacija = a.Hotel.Destinacija.Zemlja, Mesto = a.Hotel.Destinacija.Grad, NazivHotela = a.Hotel.Naziv, VrstaUsluge = a.Usluga.VrstaUsluge, TerminPutovanja = a.Termin.Pocetak.Day.ToString() +"." + a.Termin.Pocetak.Month.ToString() + "." + " do " + a.Termin.Kraj.Day.ToString() + "." + a.Termin.Kraj.Month.ToString() + ".", Cena = a.Cena }).SingleOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
            return rezervacija;
        }
    }
}