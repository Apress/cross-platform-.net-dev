//Filename MVC.Test.cs
using System;
using NUnit.Framework;
using Crossplatform.NET.Chapter09;
namespace Crossplatform.NET.Chapter09.MVCTests
{
    [TestFixture]
    public class MVCTest
    {
        private Person model;
        private View view;
        private Controller controller;
       
        public MVCTest()
        {}

        [SetUp]
        public void SetUp()
        {
             //Set up all objects, and their relationships
            this.model = new Person("Timothy", 
                                      "Ring",
                                      "000-111",
                                      "Timothy.Ring@Crossplatform.net");

            this.controller = new Controller(this.model);
            this.view = new ConsoleView(this.model, this.controller);
            this.controller.View = this.view;
        }

        [Test]
        public void TestReferencesExist()
        {
            Assert.IsNotNull(this.controller.View, "Controller should have a view");
            // other items to test are private, so require recoding as before
            // to make public
        }

        [Test]
        public void TestControllerModelInteraction()
        {
            string newEmail = "Power.Unlimited@crossplatform.net";
            this.controller.ChangeFirstname("Monty");            
            this.controller.ChangeSurname("Burns");            
            this.controller.ChangeEmail(newEmail);            
            // this.controller.ChangeSSN("012-345");            
            // string SSNPostChange = this.model.SocialSecurityNumber;
            Assert.AreEqual("Monty", this.model.Firstname, "C-M Firstname broken");
            Assert.AreEqual("Burns", this.model.Surname, "C-M Surname broken");
            Assert.AreEqual(newEmail, this.model.Email, "C-M email broken");
            // Assert.AreEqual("012-345", SSNPostChange, "C-M ssn broken");
        }


        [Test][Ignore("Write wrappers and fill in tests")]
        public void TestControllerModelViewInteraction()
        {
            // test publicly exposed wrappers here
        }

    }
} 
