//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rezervacija
    {
        public int RezervacijaID { get; set; }
        public int KorisnikID { get; set; }
        public int AranzmanID { get; set; }
        public int Aktivan { get; set; }
    
        public virtual Aranzman Aranzman { get; set; }
        public virtual Korisnik Korisnik { get; set; }
    }
}