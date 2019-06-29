using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TA.Models
{
    public class RezervacijaViewModel
    {
        public int RezervacijaID { get; set; }
        public int KorisnikID { get; set; }
        public int AranzmanID { get; set; }
        public string Ime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Mesto { get; set; }
    }
}