using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TA.Models
{
    public class Home
    {
        //vraca IQueryable kolekciju koju cine imena zemalja iz baze; navigacija na pocetnoj stranici
        public static IQueryable<string> spisakZemalja()
        {
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            IQueryable<string> destinacije = (from d in dbEntitet.Destinacijas
                                             select d.Zemlja).Distinct();
            return destinacije;
        }

        //vraca listu ponuda koju cine aranzmani iz jedne zemlje
        public static List<Aranzman> SpisakPonudaInicijalni()
        {
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            List<Aranzman> ponude = (from a in dbEntitet.Aranzmen
                                     where a.Hotel.Destinacija.Zemlja == "Grčka" && a.Aktivan == 1
                                     orderby a.Hotel.Destinacija.Zemlja, a.Hotel.Destinacija.Grad
                                     select a).ToList();
            return ponude;
        }

        //vraca listu ponuda koju cine svi aranzmani iz baze
        public static List<Aranzman> SpisakPonudaNaKlik(string zemlja)
        {
            TuristickaAgencijaEntities dbEntitet = new TuristickaAgencijaEntities();
            List<Aranzman> ponude = (from a in dbEntitet.Aranzmen
                                     where a.Hotel.Destinacija.Zemlja == zemlja && a.Aktivan == 1
                                     orderby a.Hotel.Destinacija.Zemlja, a.Hotel.Destinacija.Grad
                                     select a).ToList();
            return ponude;
        }
    }
}