using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RuilWinkelVaals.ViewModel
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        public int? Balans { get; set; }

        public int? AccountType { get; set; }

        public int? Ledenpas { get; set; }

        public string Straat { get; set; }

        public string Huisnummer { get; set; }

        public string Woonplaats { get; set; }

        public string Postcode { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? Geboortedatum { get; set; }
    }
}