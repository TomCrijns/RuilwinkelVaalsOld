using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RuilWinkelVaals.ViewModel
{
    public class Login
    {
        [Required(ErrorMessage = "Er is geen e-mailadres ingevuld")]
        public string emailAddress { get; set; }
        [Required(ErrorMessage = "Er is geen wachtwoord ingevuld")]
        public string password { get; set; }
        public int loginAttempts { get; set; }
    }
}