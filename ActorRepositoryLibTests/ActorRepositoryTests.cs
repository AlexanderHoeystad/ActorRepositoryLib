using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActorRepositoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ActorRepositoryLibTests;


namespace ActorRepositoryLib.Tests
{
    [TestClass()]
    public class ActorRepositoryTests
    {

        //ActorsRepositoryList actorsRepositoryList = new ActorsRepositoryList();
        //Actor data = new Actor()
        //{
        //    Id = 1,
        //    Name = "Alex",
        //    BirthYear = 1997
        //};

        [TestMethod()]
        public void GetActorTest()
        {
            //var Actor = actorsRepositoryList.GetActor();
            //Assert.AreEqual(5, Actor.Count());
            Assert.Fail();

        }

        [TestMethod()]
        public void AddActorTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetActorByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteActorTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateActorTest()
        {
            Assert.Fail();
        }
    }
}