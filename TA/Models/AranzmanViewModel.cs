using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //dodato zbog anotacija
using System.Linq;
using System.Web;

namespace TA.Models
{
    public class AranzmanViewModel
    {
        public int SifraAranzmana { get; set; }
        public string Destinacija { get; set; }
        public string Mesto { get; set; }
        public string NazivHotela { get; set; }
        public string VrstaUsluge { get; set; }
        public string TerminPutovanja { get; set; }

        [Required(ErrorMessage = "Cena je obavezna")]
        [RegularExpression("[0-9]+", ErrorMessage = "Cena mora da bude ceo broj")]
        [Range(1, 9999, ErrorMessage = "Cena mora da bude veća od nule")]
        public int Cena { get; set; }

        [Required(ErrorMessage = "Raspoloživost je obavezna")]
        [RegularExpression("[0-9]+", ErrorMessage = "Raspoloživost mora da bude ceo broj")]
        [Range(1, 9999, ErrorMessage = "Raspoloživost mora da bude veća od nule")]
        public int Raspolozivost { get; set; }
        public int Aktivan { get; set; }
    }
}