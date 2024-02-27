using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActorRepositoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActorRepositoryLibTests;
using Microsoft.EntityFrameworkCore;

namespace ActorRepositoryLib.Tests
{
    [TestClass()]
    public class ActorsRepositoryDBTests
    {

        private const bool useDatabase = true;
        private static IActorsRepository _repo;
        

        [ClassInitialize]
        public static void InitOnce(TestContext context)
        {
            if (useDatabase)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ActorsDbContext>();
               
                optionsBuilder.UseSqlServer(Secrets.ConnectionStringSimply);
               
                ActorsDbContext _dbContext = new(optionsBuilder.Options);
                // clean database table: remove all rows
                _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Movies");
                _repo = new ActorsRepositoryDB(_dbContext);
            }
            else
            {
                _repo = new ActorsRepositoryList();
            }
        }


        [TestMethod()]
        public void ActorsRepositoryDBTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddActorTest()
        {
            _repo.AddActor(new Actor { Name = "Z", BirthYear = 1895 });
            Actor willSmith = _repo.AddActor(new Actor { Name = "Will Smith", BirthYear = 1968 });
            Assert.IsTrue(willSmith.Id >= 0);
            IEnumerable<Actor> all = _repo.GetActor();
            Assert.AreEqual(2, all.Count());

            Assert.ThrowsException<ArgumentNullException>(
                () => _repo.AddActor(new Actor { Name = null, BirthYear = 1895 }));
            Assert.ThrowsException<ArgumentException>(
                () => _repo.AddActor(new Actor { Name = "", BirthYear = 1895 }));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => _repo.AddActor(new Actor { Name = "B", BirthYear = 1894 }));
        }

        [TestMethod()]
        public void DeleteActorTest()
        {
            Actor a = _repo.AddActor(new Actor { Name = "Sandra Bullock", BirthYear = 1964 });
            Actor? actor = _repo.DeleteActor(a.Id);
            Assert.IsNotNull(actor);
            Assert.AreEqual("Sandra Bullock", actor.Name);

            Actor? actor2 = _repo.DeleteActor(a.Id);
            Assert.IsNull(actor2);
        }

        [TestMethod()]
        public void GetActorTest()
        {
            IEnumerable<Actor> actors = _repo.GetActor(sortby: "Name");

            Assert.AreEqual(actors.First().Name, "Will Smith");

            actors = _repo.GetActor(sortby: "Birthyear");
            Assert.AreEqual(actors.First().Name, "Z");

            actors = _repo.GetActor(nameStart: "Wi");
            Assert.AreEqual(1, actors.Count());
            Assert.AreEqual(actors.First().Name, "Will Smith");
        }

        [TestMethod()]
        public void GetActorByIdTest()
        {
            Actor a = _repo.AddActor(new Actor { Name = "Emma Stone", BirthYear = 1988 });
            Actor? actor = _repo.GetActorById(a.Id);
            Assert.IsNotNull(actor);
            Assert.AreEqual("Emma Stone", actor.Name);
            Assert.AreEqual(1988, actor.BirthYear);

            Assert.IsNull(_repo.GetActorById(-1));
        }

        [TestMethod()]
        public void UpdateActorTest()
        {
            Actor a = _repo.AddActor(new Actor { Name = "Ryan Reynolds", BirthYear = 1976 });
            Actor? actor = _repo.UpdateActor(a.Id, new Actor { Name = "Wrexham Owner", BirthYear = 1976 });
            Assert.IsNotNull(actor);
            Actor? actor2 = _repo.GetActorById(a.Id);
            Assert.AreEqual("Wrexham Owner", actor.Name);

            Assert.IsNull(
                _repo.UpdateActor(-1, new Actor { Name = "Chris Hemsworth", BirthYear = 1983 }));
            Assert.ThrowsException<ArgumentException>(
                () => _repo.UpdateActor(a.Id, new Actor { Name = "", BirthYear = 1976 }));
        }
    }
}