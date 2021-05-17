using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RuilWinkelVaals.BusinessLogic.Authentication;
using RuilWinkelVaals.Models;
using RuilWinkelVaals.ViewModel;

namespace RuilWinkelVaals.Controllers
{
    public class RegisterController : Controller
    {
        UserDataModel db = new UserDataModel();
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "Email, Password, ValidationPassword, Voornaam, Achternaam, Straat, Huisnummer, Woonplaats, Postcode, Geboortedatum, Zakelijk")]Register model)
        {
            if (ModelState.IsValid)
            {
                //Try to get an Profile where email is similar to the userinput
                var profileExists = db.ProfileData.Where(user => user.Email == model.Email).FirstOrDefault();
                if(profileExists == null) //If there is not an registered user with the given Email
                {
                    var ProfileAge = GetAge(Convert.ToDateTime(model.Geboortedatum).Date);
                    if(ProfileAge >= 16) //Customer is old enough to register
                    {
                        if(model.Password == model.ValidationPassword) //Given passwords are equal
                        {
                            ProfileData newProfile = new ProfileData() //Create new ProfileObject
                            {
                                Email = model.Email,
                                Voornaam = model.Voornaam,
                                Achternaam = model.Achternaam,
                                Straat = model.Straat,
                                Huisnummer = model.Huisnummer,
                                Woonplaats = model.Woonplaats,
                                Postcode = model.Postcode,
                                Geboortedatum = model.Geboortedatum,
                                DateCreated = DateTime.Today.Date,
                            };

                            //Save profile to DB
                            db.ProfileData.Add(newProfile);
                            db.SaveChanges();

                            //Get ProfileId for Foreign relation
                            var ProfileId = db.ProfileData.Where(profile => profile.Email == model.Email).FirstOrDefault();
                            //Create new Account with relation to ProfileData
                            HashSalt hashSalt = HashSalt.GenerateHashSalt(16, model.Password);
                            AccountData newAccount = new AccountData()
                            {
                                ProfileId = ProfileId.Id,
                                Hash = hashSalt.hash,
                                Salt = hashSalt.salt,
                            };
                            db.AccountData.Add(newAccount);
                            db.SaveChanges();
                            return RedirectToAction("About", "Home");

                        }
                        else //Passwords are not equal
                        {
                            ModelState.AddModelError("ValidationError", "De gegeven wachtwoorden komen niet overeen met elkaar.");
                            return View();
                        }

                    }
                    else //Customer is not old enough to register
                    {
                        ModelState.AddModelError("ValidationError", "U dient minimaal 16jaar te zijn om te registreren.");
                        return View();
                    }
                }
                else //If there is an user with the given Email
                {
                    ModelState.AddModelError("ValidationError", "Er bestaat al een account met dit Email adres.");
                    return View();
                }
            }
            else
            {
                return View();
            }

        }

        public int GetAge(DateTime bornDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - bornDate.Year;
            if (bornDate > today.AddYears(-age))
                age--;
            return age;
        }
    }
}