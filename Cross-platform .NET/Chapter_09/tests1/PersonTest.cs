//Filename Person.Test.cs
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
    }
}
