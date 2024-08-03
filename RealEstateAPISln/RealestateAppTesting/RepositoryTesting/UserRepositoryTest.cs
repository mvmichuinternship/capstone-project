using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Context;
using RealEstateAPI.Interfaces;
using RealEstateAPI.Models;
using RealEstateAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealestateAppTesting.RepositoryTesting
{
    public class UserRepositoryTest
    {
        RealEstateAppContext context;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                       .UseInMemoryDatabase("dummyDB");
            context = new RealEstateAppContext(optionsBuilder.Options);

        }

        [Test]
        public async Task AddTest()
        {
            IRepository<string, User> repository = new UserRepository(context);
            User admin = new User()
            {
                Name = "test",
                Phone = "7338985215",
                Plan = "Basic",
                Role = "seller",
                UserEmail = "test",
            };
            var result = await repository.Add(admin);
            Assert.That(result.UserEmail, Is.EqualTo("test"));
        }

        [Test]
        public async Task DelTest()
        {
            IRepository<string, User> repository = new UserRepository(context);
            User admin = new User()
            {
                Name = "test",
                Phone = "7338985215",
                Plan = "Basic",
                Role = "seller",
                UserEmail = "test1",
            };
            var repo = await repository.Add(admin);
            var result = await repository.Delete(repo.UserEmail);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTest()
        {
            IRepository<string, User> repository = new UserRepository(context);


            var result = await repository.Get("test");
            result.Phone = "1234567890";
            var res = await repository.Update(result);
            Assert.AreEqual("test", res.UserEmail);
        }

        [Test]
        public async Task GetTest()
        {
            IRepository<string, User> repository = new UserRepository(context);
            User admin = new User()
            {
                Name = "test",
                Phone = "7338985215",
                Plan = "Basic",
                Role = "seller",
                UserEmail = "test2",
            };

            var rep = await repository.Add(admin);
            var result = await repository.Get(rep.UserEmail);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTest()
        {
            IRepository<string, User> repository = new UserRepository(context);
            User admin = new User()
            {
                Name = "test",
                Phone = "7338985215",
                Plan = "Basic",
                Role = "seller",
                UserEmail = "test",
            };

            var rep = await repository.Add(admin);
            var result = await repository.GetAll();
            Assert.IsNotNull(result);
        }


        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
