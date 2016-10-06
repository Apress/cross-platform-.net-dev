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
            Assert.IsNotNull(this.jase, "Simple constructor fails");
            Assert.IsNotNull(this.mj, "1st constructor overload fails");
        }

	[Test]
	public void TestPropertyIsNull()
	{
 	   // Trivial demonstration
	    Assert.IsNull(this.jase.Email, "Email should be null for default constructor");
	}

	[Test]
	public void TestObjectsEqual()
	{
	    string email = "Mj.Easton@Crossplatform.NET";
	    Person mj2 = new Person("Mj", "Easton", "111-222", email);
	    Assert.AreEqual(this.mj, mj2, "Objects are not equal.");
	}

	[Test]
	public void TestObjectsSame()
	{
	    Person mj2 = this.mj;
	    Assert.AreSame(this.mj, mj2, "Objects should be the same");
	}


	[SetUp]
	public void SetUp()
	{
	    this.jase = new Person();
	    string email = "Mj.Easton@Crossplatform.NET";
	    this.mj = new Person("Mj", "Easton", "111-222", email);
	}
	[Test]
	public void TestObjectExist()
	{
	    // this is now smaller and neater
	    Assert.IsNotNull(this.jase, "Simple constructor fails");
	    Assert.IsNotNull(this.mj, "First overloaded constructor fails");
	}

	[TearDown]
	public void TearDown()
	{
	    // clean up code here.  No clean up needed
	    // in this trivial example, so demo console stuff
	    Console.WriteLine("This output is redirected to the screen...");
	    Console.Error.WriteLine("... if you are running the GUI version");
	}


    }
}
