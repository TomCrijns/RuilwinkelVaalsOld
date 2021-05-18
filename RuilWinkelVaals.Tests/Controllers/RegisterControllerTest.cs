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
    class RegisterControllerTest
    {
        [TestMethod]
        public void Register()
        {
            //Arange
            RegisterController regControler = new RegisterController();
            //Act
            ViewResult result = regControler.Register() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void NoEmailFilledIn()
        {
            //Arrange
            RegisterController regController = new RegisterController();
            Register newAccount = new Register();
            newAccount.Email = "";
            newAccount.Password = "testpassword";
            newAccount.ValidationPassword = "testpassword";
            newAccount.Geboortedatum = new DateTime(2000, 1, 1);

            var modelBinder = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(
                    () => newAccount, newAccount.GetType()),
                ValueProvider = new NameValueCollectionValueProvider(
                    new NameValueCollection(), CultureInfo.InvariantCulture)
            };

            var binder = new DefaultModelBinder().BindModel(
                new ControllerContext(), modelBinder);
            regController.ModelState.Clear();
            regController.ModelState.Merge(modelBinder.ModelState);
            //Act
            ViewResult result = (ViewResult)regController.Register(newAccount);
            //Assert
            Assert.IsTrue(!result.ViewData.ModelState.IsValid);
            Assert.AreEqual(result.ViewData.ModelState["Email"].Errors[0].ErrorMessage, "Er is geen e-mailadres ingevuld");
        }

        [TestMethod]
        public void NoPasswordFilledIn()
        {
            //Arrange
            RegisterController regController = new RegisterController();
            Register newAccount = new Register();
            newAccount.Email = "testmail@mail.com";
            newAccount.Password = "";
            newAccount.ValidationPassword = "testpassword";
            newAccount.Geboortedatum = new DateTime(2000, 1, 1);

            var modelBinder = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(
                    () => newAccount, newAccount.GetType()),
                ValueProvider = new NameValueCollectionValueProvider(
                    new NameValueCollection(), CultureInfo.InvariantCulture)
            };

            var binder = new DefaultModelBinder().BindModel(
                new ControllerContext(), modelBinder);
            regController.ModelState.Clear();
            regController.ModelState.Merge(modelBinder.ModelState);
            //Act
            ViewResult result = (ViewResult)regController.Register(newAccount);
            //Assert
            Assert.IsTrue(!result.ViewData.ModelState.IsValid);
            Assert.AreEqual(result.ViewData.ModelState["Password"].Errors[0].ErrorMessage, "Er is geen wachtwoord ingevuld");
        }

        [TestMethod]
        public void NoValidationPasswordFilledIn()
        {
            //Arrange
            RegisterController regController = new RegisterController();
            Register newAccount = new Register();
            newAccount.Email = "testmail@mail.com";
            newAccount.Password = "testpassword";
            newAccount.ValidationPassword = "";
            newAccount.Geboortedatum = new DateTime(2000, 1, 1);

            var modelBinder = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(
                    () => newAccount, newAccount.GetType()),
                ValueProvider = new NameValueCollectionValueProvider(
                    new NameValueCollection(), CultureInfo.InvariantCulture)
            };

            var binder = new DefaultModelBinder().BindModel(
                new ControllerContext(), modelBinder);
            regController.ModelState.Clear();
            regController.ModelState.Merge(modelBinder.ModelState);
            //Act
            ViewResult result = (ViewResult)regController.Register(newAccount);
            //Assert
            Assert.IsTrue(!result.ViewData.ModelState.IsValid);
            Assert.AreEqual(result.ViewData.ModelState["ValidationPassword"].Errors[0].ErrorMessage, "Er is geen wachtwoord ingevuld");
        }

        [TestMethod]
        public void NoDoBFilledIn()
        {
            //Arrange
            RegisterController regController = new RegisterController();
            Register newAccount = new Register();
            newAccount.Email = "testmail@mail.com";
            newAccount.Password = "testpassword";
            newAccount.ValidationPassword = "testpassword";
            newAccount.Geboortedatum = Convert.ToDateTime("");

            var modelBinder = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(
                    () => newAccount, newAccount.GetType()),
                ValueProvider = new NameValueCollectionValueProvider(
                    new NameValueCollection(), CultureInfo.InvariantCulture)
            };

            var binder = new DefaultModelBinder().BindModel(
                new ControllerContext(), modelBinder);
            regController.ModelState.Clear();
            regController.ModelState.Merge(modelBinder.ModelState);
            //Act
            ViewResult result = (ViewResult)regController.Register(newAccount);
            //Assert
            Assert.IsTrue(!result.ViewData.ModelState.IsValid);
            Assert.AreEqual(result.ViewData.ModelState["Geboortedatum"].Errors[0].ErrorMessage, "Er is geen geboortedatum ingevuld");
        }

        [TestMethod]
        public void EmailExists()
        {
            // Arrange
            RegisterController regController = new RegisterController();
            Register newAccount = new Register();
            newAccount.Email = "test";
            newAccount.Password = "testpassword";
            newAccount.ValidationPassword = "testpassword";
            newAccount.Geboortedatum = new DateTime(2000, 1, 1);

            var modelBinder = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(
                    () => newAccount, newAccount.GetType()),
                ValueProvider = new NameValueCollectionValueProvider(
                    new NameValueCollection(), CultureInfo.InvariantCulture)
            };

            var binder = new DefaultModelBinder().BindModel(
                new ControllerContext(), modelBinder);
            regController.ModelState.Clear();
            regController.ModelState.Merge(modelBinder.ModelState);
            //Act
            ViewResult result = (ViewResult)regController.Register(newAccount);
            //Assert
            Assert.IsTrue(!result.ViewData.ModelState.IsValid);
            Assert.AreEqual(result.ViewData.ModelState["ValidationError"].Errors[0].ErrorMessage, "Er bestaat al een account met dit Email adres.");
        }

        [TestMethod]
        public void Ageunder16()
        {
            // Arrange
            RegisterController regController = new RegisterController();
            Register newAccount = new Register();
            newAccount.Email = "testmail@mail.com";
            newAccount.Password = "testpassword";
            newAccount.ValidationPassword = "testpassword";
            newAccount.Geboortedatum = DateTime.Today.Date;

            var modelBinder = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(
                    () => newAccount, newAccount.GetType()),
                ValueProvider = new NameValueCollectionValueProvider(
                    new NameValueCollection(), CultureInfo.InvariantCulture)
            };

            var binder = new DefaultModelBinder().BindModel(
                new ControllerContext(), modelBinder);
            regController.ModelState.Clear();
            regController.ModelState.Merge(modelBinder.ModelState);
            //Act
            ViewResult result = (ViewResult)regController.Register(newAccount);
            //Assert
            Assert.IsTrue(!result.ViewData.ModelState.IsValid);
            Assert.AreEqual(result.ViewData.ModelState["ValidationError"].Errors[0].ErrorMessage, "De gegeven wachtwoorden komen niet overeen met elkaar.");
        }

        [TestMethod]
        public void PasswordsNotEqual()
        {
            // Arrange
            RegisterController regController = new RegisterController();
            Register newAccount = new Register();
            newAccount.Email = "testmail@mail.com";
            newAccount.Password = "testpassword1";
            newAccount.ValidationPassword = "testpassword2";
            newAccount.Geboortedatum = new DateTime(2000, 1, 1);

            var modelBinder = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(
                    () => newAccount, newAccount.GetType()),
                ValueProvider = new NameValueCollectionValueProvider(
                    new NameValueCollection(), CultureInfo.InvariantCulture)
            };

            var binder = new DefaultModelBinder().BindModel(
                new ControllerContext(), modelBinder);
            regController.ModelState.Clear();
            regController.ModelState.Merge(modelBinder.ModelState);
            //Act
            ViewResult result = (ViewResult)regController.Register(newAccount);
            //Assert
            Assert.IsTrue(!result.ViewData.ModelState.IsValid);
            Assert.AreEqual(result.ViewData.ModelState["ValidationError"].Errors[0].ErrorMessage, "De gegeven wachtwoorden komen niet overeen met elkaar.");
        }
    }
}
