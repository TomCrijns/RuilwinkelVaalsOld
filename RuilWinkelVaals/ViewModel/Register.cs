using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RuilWinkelVaals.ViewModel
{
    public class Register
    {
        [Required(ErrorMessage = "Er is geen e-mailadres ingevuld")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Er is geen wachtwoord ingevuld")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Er is geen wachtwoord ingevuld")]
        public string ValidationPassword { get; set; }

        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        public int Balans { get; set; }

        public int AccountType { get; set; }

        public int Ledenpas { get; set; }

        public string Straat { get; set; }

        public string Huisnummer { get; set; }

        public string Woonplaats { get; set; }

        public string Postcode { get; set; }

        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "Er is geen geboortedatum ingevuld")]
        public DateTime Geboortedatum { get; set; }
        public bool Zakelijk { get; set; }
    }
}