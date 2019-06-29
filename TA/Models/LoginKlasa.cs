using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TA.Models
{
    public class LoginKlasa
    {
        //vraca korisnika ukoliko se korisnicko ime i lozinka nalaze u bazi
        public static Korisnik ProveriKorisnika(LoginViewModel model)
        {
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            Korisnik korisnik = (from k in dbEntitet.Korisniks
                                 where k.KorisnickoIme == model.KorisnickoIme && k.Lozinka == model.Lozinka
                                 select k).SingleOrDefault();
            return korisnik;   
        }

        //vraca true ukoliko je rezervacija klijenta aktivna
        public static bool DaLiPostojiRezervacijaKlijenta(LoginViewModel model)
        {
            var lozinka = int.Parse(model.Lozinka);
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            var klijent = (from r in dbEntitet.Rezervacijas
                           where r.RezervacijaID == lozinka && r.Aktivan == 1
                           select r).SingleOrDefault();
            if (klijent == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}