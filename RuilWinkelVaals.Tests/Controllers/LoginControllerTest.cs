using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RuilWinkelVaals.Controllers;
using System.Web.Mvc;
using RuilWinkelVaals.ViewModel;
using System.Collections.Specialized;
using System.Globalization;

namespace RuilWinkelVaals.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {

        [TestMethod]
        public void Login()
        {
            //Arange
            LoginController loginController = new LoginController();

            //Act
            ViewResult result = loginController.Login() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void NoPasswordFilledIn()
        {
            //Arange
            LoginController loginController = new LoginController();
            Login loginAttempt = new Login();
            loginAttempt.emailAddress = "ewfa";
            loginAttempt.password = "";
            var modelBinder = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(
                      () => loginAttempt, loginAttempt.GetType()),
                ValueProvider = new NameValueCollectionValueProvider(
                        new NameValueCollection(), CultureInfo.InvariantCulture)
            };

            var binder = new DefaultModelBinder().BindModel(
                 new ControllerContext(), modelBinder);
            loginController.ModelState.Clear();
            loginController.ModelState.Merge(modelBinder.ModelState);

            //Act
            ViewResult result = (ViewResult)loginController.Login(loginAttempt);

            //Assert
            Assert.IsTrue(!result.ViewData.ModelState.IsValid);
            Assert.AreEqual(result.ViewData.ModelState["password"].Errors[0].ErrorMessage, "Er is geen wachtwoord ingevuld");
        }

        [TestMethod]
        public void NoEmailFilledIn()
        {
            //Arange
            LoginController loginController = new LoginController();
            Login loginAttempt = new Login();
            loginAttempt.emailAddress = "";
            loginAttempt.password = "admin";
            var modelBinder = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(
                      () => loginAttempt, loginAttempt.GetType()),
                ValueProvider = new NameValueCollectionValueProvider(
                        new NameValueCollection(), CultureInfo.InvariantCulture)
            };

            var binder = new DefaultModelBinder().BindModel(
                 new ControllerContext(), modelBinder);
            loginController.ModelState.Clear();
            loginController.ModelState.Merge(modelBinder.ModelState);

            //Act
            ViewResult result = (ViewResult)loginController.Login(loginAttempt);

            //Assert
            Assert.IsTrue(!result.ViewData.ModelState.IsValid);
            Assert.AreEqual(result.ViewData.ModelState["emailAddress"].Errors[0].ErrorMessage, "Er is geen e-mailadres ingevuld");
        }
    }
}
