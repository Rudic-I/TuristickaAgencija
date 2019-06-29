using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; //zbog SelectList klase

namespace TA.Models
{
    public class Admin
    {
        public static int Sifra { get; set; }

        //pravljenje lista i popunjavanje podacima iz baze; koriste se za DDL
        public static SelectList PopuniHotele()
        {
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            IEnumerable<SelectListItem> hoteli = (from h in dbEntitet.Hotels select h).AsEnumerable().OrderBy(h => h.Destinacija.Zemlja).Select(h => new SelectListItem()
            { Text = h.Naziv + ", " + h.Destinacija.Grad + ", " + h.Destinacija.Zemlja, Value = h.HotelID.ToString() });
            return new SelectList(hoteli, "Value", "Text", Sifra);
        }

        public static SelectList PopuniListuDestinacijaID()
        {
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            IEnumerable<SelectListItem> destinacije = (from a in dbEntitet.Destinacijas select a).AsEnumerable().OrderBy(d => d.Zemlja).Select(a => new SelectListItem()
            { Text = a.Grad + ", " + a.Zemlja, Value = a.DestinacijaID.ToString() });
            return new SelectList(destinacije, "Value", "Text", Sifra);
        }

        public static SelectList PopuniUsluge()
        {
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            IEnumerable<SelectListItem> usluge = (from u in dbEntitet.Uslugas select u).AsEnumerable().Select(u => new SelectListItem()
            { Text = u.VrstaUsluge, Value = u.UslugaID.ToString() });
            return new SelectList(usluge, "Value", "Text", Sifra);
        }

        public static SelectList PopuniTermine()
        {
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            IEnumerable<SelectListItem> termini = (from t in dbEntitet.Termins select t).AsEnumerable().Select(t => new SelectListItem()
            { Text = t.Pocetak.Day.ToString() + ". " + t.Pocetak.Month.ToString() + "." + " do " + t.Kraj.Day.ToString() + ". " + t.Kraj.Month.ToString() + ".", Value = t.TerminID.ToString() });
            return new SelectList(termini, "Value", "Text", Sifra);
        }


        //vracanje lista popunjenih podacima svih destinacija / hotela / aranzmana iz baze koji se ispisuju u pogledu
        public static List<Destinacija> PrikaziDestinacije()
        {
            using (TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities())
            {
                List<Destinacija> destinacije = (from d in dbEntitet.Destinacijas
                                                 orderby d.Zemlja, d.Grad
                                                 select d).ToList();
                return destinacije;
            }
        }

        public static List<Hotel> PrikaziHotele()
        {
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            List<Hotel> hoteli = (from h in dbEntitet.Hotels
                                  orderby h.Destinacija.Zemlja, h.Destinacija.Grad
                                  select h).ToList();
            return hoteli;
        }

        public static List<AranzmanViewModel> PrikaziAranzmane()
        {
            using (TuristickaAgencijaEntities db = new TuristickaAgencijaEntities())
            {
                List<AranzmanViewModel> aranzmani = (from a in db.Aranzmen
                                                     where a.Aktivan == 1
                                                     orderby a.Hotel.Destinacija.Zemlja, a.Hotel.Destinacija.Grad
                                                     select new AranzmanViewModel { SifraAranzmana = a.AranzmanID, NazivHotela = a.Hotel.Naziv, Destinacija = a.Hotel.Destinacija.Zemlja, Mesto = a.Hotel.Destinacija.Grad, VrstaUsluge = a.Usluga.VrstaUsluge, TerminPutovanja = a.Termin.Pocetak.Day.ToString() + "." + a.Termin.Pocetak.Month.ToString() + "." + " do " + a.Termin.Kraj.Day.ToString() + "." + a.Termin.Kraj.Month.ToString() + ".", Cena = a.Cena, Raspolozivost = a.Raspolozivost, Aktivan = a.Aktivan }).ToList();
                return aranzmani;
            }
        }

        //vracanje podataka o jednom izabranom aranzmanu
        public static AranzmanViewModel PrikaziAranzmanPoSifri(int sifra)
        {
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            AranzmanViewModel aranzman = (from a in dbEntitet.Aranzmen
                                          where a.AranzmanID == sifra
                                          select new AranzmanViewModel { SifraAranzmana = a.AranzmanID, NazivHotela = a.Hotel.Naziv, VrstaUsluge = a.Usluga.VrstaUsluge, TerminPutovanja = a.Termin.Pocetak.Day.ToString() + "." + a.Termin.Pocetak.Month.ToString() + " do " + a.Termin.Kraj.Day.ToString() + "." + a.Termin.Kraj.Day.ToString() + ".", Cena = a.Cena, Raspolozivost = a.Raspolozivost }).Single();
            return aranzman;

        }

        //da li postoji destinacija
        public static bool DaLiPostojiDestinacija(string zemlja, string grad)
        {
            using (TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities())
            {
                var destinacija = (from d in dbEntitet.Destinacijas
                                      where d.Zemlja == zemlja && d.Grad == grad
                                      select d).FirstOrDefault();
                if(destinacija == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        //dodavanje nove destinacije / hotela / aranzmana
        public static bool DodajDestinaciju(string zemlja, string grad)
        {
            if(DaLiPostojiDestinacija(zemlja, grad))
            {
                return false;
            }
            else
            {
                using (TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities())
                {
                    int sifra = (from d in dbEntitet.Destinacijas
                                 select d.DestinacijaID).Max();
                    Destinacija destinacija = new Destinacija { DestinacijaID = sifra + 1, Zemlja = zemlja, Grad = grad };
                    try
                    {
                        dbEntitet.Destinacijas.Add(destinacija);
                        dbEntitet.SaveChanges();
                    }
                    catch (Exception) { }
                }
                return true;
            }
            
        }

        //da li postoji hotel
        public static bool DaLiPostojiHotel(int destinacijaID, string naziv)
        {
            using(TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities())
            {
                var hotel = (from h in dbEntitet.Hotels
                             where h.DestinacijaID == destinacijaID && h.Naziv == naziv
                             select h).FirstOrDefault();
                if (hotel == null)
                {
                    return false;
                }
                else {
                    return true;
                }

            }
        }

        public static bool DodajHotel(int destinacijaID, string naziv, int brojZvezdica)
        {
            if (DaLiPostojiHotel(destinacijaID, naziv))
            {
                return false;
            }
            else
            {
                using (TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities())
                {
                    int sifra = (from h in dbEntitet.Hotels
                                 select h.HotelID).Max();
                    Hotel hotel = new Hotel { HotelID = sifra + 1, DestinacijaID = destinacijaID, Naziv = naziv, BrojZvezdica = brojZvezdica };
                    try
                    {
                        dbEntitet.Hotels.Add(hotel);
                        dbEntitet.SaveChanges();
                    }
                    catch (Exception) { }
                }
                return true;
            }
        }

        public static void DodajAranzman(int hotelID, int uslugaID, int terminID, int cena, int raspolozivost)
        {
            using (TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities())
            {
                int sifra = (from a in dbEntitet.Aranzmen
                             select a.AranzmanID).Max();
                Aranzman aranzman = new Aranzman { AranzmanID = sifra + 1, HotelID = hotelID, UslugaID = uslugaID, TerminID = terminID, Cena = cena, Raspolozivost = raspolozivost, Aktivan = 1 };
                try
                {
                    dbEntitet.Aranzmen.Add(aranzman);
                    dbEntitet.SaveChanges();
                }
                catch (Exception e) { }
            }
        }

        //proverava da li postoji neka rezarvacija odredenog aranzmana; ukoliko postoji, brisanje nije moguce
        public static bool DaLiJeMoguceObrisatiAranzman(int sifra)
        {
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            int brojRezervacijaAranzmana = (from r in dbEntitet.Rezervacijas
                                            where r.AranzmanID == sifra && r.Aktivan == 1
                                            select r).Count();
            if(brojRezervacijaAranzmana > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //izmena i brisanje aranzmana
        public static void ObrisiAranzmanPoSifri(int sifra)
        {
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            try
            {
                Aranzman aranzman = (from a in dbEntitet.Aranzmen where a.AranzmanID == sifra select a).Single();
                aranzman.Aktivan = 0;
                dbEntitet.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        public static void IzmeniAranzman(Aranzman aranzman)
        {
            using (TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities())
            {
                Aranzman izmena = (from a in dbEntitet.Aranzmen
                                    where a.AranzmanID == aranzman.AranzmanID
                                    select a).Single();
                izmena.Cena = aranzman.Cena;
                izmena.Raspolozivost = aranzman.Raspolozivost;
                try
                {
                    dbEntitet.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
    }
}