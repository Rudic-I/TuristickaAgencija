using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TA.Models
{
    public class LoginViewModel
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
    }
}