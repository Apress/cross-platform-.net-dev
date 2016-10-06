//Filename PersonTest.cs
using System;
using NUnit.Framework;
using Crossplatform.NET.Chapter09;

namespace Crossplatform.NET.Chapter09.PersonTest
{
    [TestFixture]
    public class PersonTest
    {
        public PersonTest(){}

        [Test]
        public void TestObjectExist()
        {
            Person jase = new Person();
            Assert.IsNotNull(jase, "Simple constructor fails");
            string email = "Mj.Easton@Crossplatform.NET";
            Person mj = new Person("Mj", "Easton", "111-222", email);
            Assert.IsNotNull(mj, "1st constructor overload fails");
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

    }
}
