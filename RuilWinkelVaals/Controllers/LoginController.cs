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
    public class LoginController : Controller
    {
        UserDataModel db = new UserDataModel();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// This method is responsible for authenticating the user that tries to log in to the application
        /// </summary>
        /// <param name="model">Viewmodel for login</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login([Bind(Include = "emailAddress, password")] Login model)
        {
            if (ModelState.IsValid)
            {
                bool authenticated = false;
                var profileData = db.ProfileData.Where(user => user.Email == model.emailAddress).FirstOrDefault();
                if (profileData != null)
                {
                    var userCredentials = db.AccountData.Where(user => user.ProfileId == profileData.Id).FirstOrDefault();
                    authenticated = Authentication.AuthenticateUser(model.emailAddress, profileData.Email, model.password, userCredentials.Hash, userCredentials.Salt);
                }
                if (authenticated)
                {
                    var user = new User()
                    {
                        Id = profileData.Id,
                        Voornaam = profileData.Voornaam,
                        Achternaam = profileData.Achternaam
                    };
                    Session["User"] = user;
                    return RedirectToAction("About", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "De gebruikersnaam/wachtwoord is incorrect");

                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}