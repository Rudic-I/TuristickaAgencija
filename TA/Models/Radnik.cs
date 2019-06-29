using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure; //dodato zbog DbUpdateException
using System.Data.SqlClient; //dodato zbog SqlException
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TA.Models
{
    public class Radnik
    {
        //vraca listu svih korisnika iz baze
        public static List<Korisnik> PrikaziKorisnike()
        {
            using (TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities())
            {
                List<Korisnik> korisnici = (from k in dbEntitet.Korisniks
                                            where k.Uloga == 3
                                            select k).ToList();
                return korisnici;
            }
        }

        //raspolozivost je fiksni broj koji predstavlja ukupan broj aranzmana koje je moguce napraviti
        //brojRezervacijaAranzmana je broj napravljenih rezervacija za taj aranzman; da bi se ostvarila rezervacija, mora biti manji od raspolozivosti
        //vraca true ukoliko je raspolozivost > brojRezervacijaAranzmana
        public static bool DaLiJeMoguceRezervisati(int sifra)
        {
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            int raspolozivost = (from a in dbEntitet.Aranzmen
                                 where a.AranzmanID == sifra
                                 select a.Raspolozivost).Single();

            int brojRezervacijaAranzmana = (from r in dbEntitet.Rezervacijas
                                            where r.AranzmanID == sifra && r.Aktivan == 1
                                            select r).Count();

            if (raspolozivost > brojRezervacijaAranzmana)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        public static string DodajRezervaciju(Rezervacija rezervacija)
        {
            using (TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities())
            {
                try
                {
                    dbEntitet.Rezervacijas.Add(rezervacija);
                    dbEntitet.SaveChanges();
                    return "uspeh";
                }
                catch (DbUpdateException)
                {
                    return "sifra";
                }
                catch (Exception)
                {
                    return "opsti";
                }
            }
        }



        //ukoliko je rezervacija moguca, dodaje novog korisnika (klijenta) u bazu
        public static void DodajKlijenta(Korisnik korisnik)
        {
            using (TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities())
            {
                try
                {
                    dbEntitet.Korisniks.Add(korisnik);
                    dbEntitet.SaveChanges();
                }
                catch (Exception e) { }
            }
        }

        //prikaz svih rezervacija iz baze
        public static List<RezervacijaViewModel> PrikaziRezervacije()
        {
            using (TuristickaAgencijaEntities db = new TuristickaAgencijaEntities())
            {
                List<RezervacijaViewModel> rezervacije = (from a in db.Aranzmen
                                                          join r in db.Rezervacijas on a.AranzmanID equals r.AranzmanID
                                                          where r.Aktivan == 1
                                                          select new RezervacijaViewModel { RezervacijaID = r.RezervacijaID, KorisnikID = r.Korisnik.KorisnikID, KorisnickoIme = r.Korisnik.KorisnickoIme, Ime = r.Korisnik.Ime, Mesto = a.Hotel.Naziv + ", " + a.Hotel.Destinacija.Grad + ", " + a.Hotel.Destinacija.Zemlja }).ToList();
                return rezervacije;
            }
        }

        //brise rezervaciju iz baze, odn. postavlja vrednost polja aktivan na 0
        public static bool ObrisiRezervacijuPoSifri(int sifra)
        {
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            try
            {
                Rezervacija rezervacija = (from r in dbEntitet.Rezervacijas where r.RezervacijaID == sifra select r).Single();

                if (rezervacija != null)
                {
                    rezervacija.Aktivan = 0;
                    dbEntitet.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}