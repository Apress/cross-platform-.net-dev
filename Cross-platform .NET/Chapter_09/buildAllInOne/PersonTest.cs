//Filename PersonTest.cs
using System;
using NUnit.Framework;
using Crossplatform.NET.Chapter09;

namespace Crossplatform.NET.Chapter09.PersonTest
{
    [TestFixture]
    public class PersonTest
    {
	private Person jase;
	private Person mj;

        public PersonTest(){}

	[Test]
	public void TestObjectExist()
	{
	    // this is now smaller and neater
	    Assert.IsNotNull(this.jase, "Simple constructor fails");
	    Assert.IsNotNull(this.mj, "First overloaded constructor fails");
	}


	[Test]
	public void TestPropertyIsNull()
	{
 	   // Trivial demonstration
	    Person Jase = new Person();
	    Assert.IsNull(Jase.Email, "Email should be null for default constructor");
	}

	[Test]
	public void TestObjectsEqual()
	{
	    string email = "Mj.Easton@Crossplatform.NET";
	    Person mj = new Person("Mj", "Easton", "111-222", email);
	    Person mj2 = new Person("Mj", "Easton", "111-222", email);
	    Assert.AreEqual(mj, mj2, "Objects are not equal.");
	}

	[Test]
	public void TestObjectsSame()
	{
	    string email = "Mj.Easton@Crossplatform.NET";
	    Person mj = new Person("Mj","Easton","111-222",email);
	    Person mj2 = mj;
	    Assert.AreSame(mj, mj2, "Objects should be the same");
	}


	[SetUp]
	public void SetUp()
	{
	    this.jase = new Person();
	    string email = "Mj.Easton@Crossplatform.NET";
	    this.mj = new Person("Mj", "Easton", "111-222", email);
	}

	[TearDown]
	public void TearDown()
	{
	    // clean up code here.  No clean up needed
	    // in this trivial example, so demo console stuff
	    Console.WriteLine("This output is redirected to the screen...");
	    Console.Error.WriteLine("... if you are running the GUI version");
	}

	[Test]
	public void TestIsEmailForTrue()
	{
	    string m = "IsEmail did not identify a valid address";
	    // email address is valid so Person.TestIsEmail() should return true
	    Assert.IsTrue(this.mj.TestIsEmail(this.mj.Email));
	}

	 [Test]
	 public void TestIsEmailForFalse()
	 {
	     string m = "IsEmail did not identify an invalid address properly";
	     // email address is rubbish, so IsEmail should return false.
	     Assert.IsFalse(this.mj.TestIsEmail("Scooby.Don't"), m);
	 }

	[Test]
	[ExpectedException(typeof(Person.InvalidEmailException))]
	public void TestEmailException()
	{
	    // deliberately try to throw exception
	    this.mj.Email = "mj#Snifferoo.com";
	}

	[Test]
	public void TestEmailExceptionLogic()
	{
	    string bogusEmail = "mj#Snifferoo.com";
	    string oldEmail = this.mj.Email;
	    bool testWorking = false;           
	    string m = "Test failed as InvalidEmailException was not thrown";
	    // deliberately try to throw exception
	    try
	    {
	        this.mj.Email = bogusEmail;
	    }
	    catch(Person.InvalidEmailException e)
	    {
	        testWorking = true;
	        // designed behaviour is to leave old email
	        // in event of new email being invalid
	        Assert.IsTrue((oldEmail == this.mj.Email), e.Message);
	        Assert.IsFalse((this.mj.Email == bogusEmail), "Bogus email was assigned");
	    }
	    finally
	    {
	        Assert.IsTrue(testWorking, m);
	    }
	}






    }
}
