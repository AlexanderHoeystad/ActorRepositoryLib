using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActorRepositoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorRepositoryLib.Tests
{
    [TestClass()]
    public class ActorTests
    {
        Actor data = new() { Id = 1, Name = "Mads Mikkelsen", BirthYear = 1965 };
        Actor data2 = new() { Id = 2, Name = "Denzel Washington", BirthYear = 1954 };
        Actor data3 = new() { Id = 3, Name = "Morgan Freeman", BirthYear = 1937 };
        Actor data4 = new() { Id = 4, Name = "The Rock", BirthYear = 1972 };
        Actor data5NameShort = new() { Id = 5, Name = "Tom", BirthYear = 1962 };
        Actor data6NameNull = new() { Id = 6, Name = null, BirthYear = 1931 };
        Actor data7InvalidBirthYear = new() { Id = 7, Name = "Tom Hanks", BirthYear = 1819 };


        [TestMethod()]
        public void ToStringTest()
        {
            string str = data.ToString();
            Assert.AreEqual("1 Mads Mikkelsen 1965", str);
        }

        [TestMethod()]
        public void ValidateNameTest()
        {
            data.ValidateName();
            Assert.ThrowsException<ArgumentNullException>(() => data6NameNull.ValidateName());
            Assert.ThrowsException<ArgumentException>(() => data5NameShort.ValidateName());

        }

        [TestMethod()]
        public void ValidateBirthYearTest()
        {
            data.ValidateBirthYear();
            Assert.ThrowsException<ArgumentException>(() => data7InvalidBirthYear.ValidateBirthYear());
        }

        [TestMethod()]
        public void Validate()
        {
            Assert.ThrowsException<ArgumentNullException>(() => data6NameNull.Validate());
            Assert.ThrowsException<ArgumentException>(() => data5NameShort.Validate());
            Assert.ThrowsException<ArgumentException>(() => data7InvalidBirthYear.Validate());
        }

    }
}