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
    
    public partial class Korisnik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Korisnik()
        {
            this.Rezervacijas = new HashSet<Rezervacija>();
        }
    
        public int KorisnikID { get; set; }
        public string Ime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public int Uloga { get; set; }
    
        public virtual Uloga Uloga1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rezervacija> Rezervacijas { get; set; }
    }
}
