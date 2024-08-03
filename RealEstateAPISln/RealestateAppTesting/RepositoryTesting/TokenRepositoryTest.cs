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
    public class TokenRepositoryTest
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
            IRepository<string, TokenData> repository = new TokenRepository(context);
            TokenData admin = new TokenData()
            {
                Password = Encoding.ASCII.GetBytes("test"),
                PasswordKey=Encoding.ASCII.GetBytes("key"),
                Tid =1,
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
            IRepository<string, TokenData> repository = new TokenRepository(context);
            TokenData admin = new TokenData()
            {
                Password = Encoding.ASCII.GetBytes("test"),
                PasswordKey = Encoding.ASCII.GetBytes("key"),
                Tid = 2,
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
            IRepository<string, TokenData> repository = new TokenRepository(context);


            var result = await repository.Get("test");
            result.Phone = "1234567890";
            var res = await repository.Update(result);
            Assert.AreEqual("test", res.UserEmail);
        }

        [Test]
        public async Task GetTest()
        {
            IRepository<string, TokenData> repository = new TokenRepository(context);
            TokenData admin = new TokenData()
            {
                Password = Encoding.ASCII.GetBytes("test"),
                PasswordKey = Encoding.ASCII.GetBytes("key"),
                Tid = 3,
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
            IRepository<string, TokenData> repository = new TokenRepository(context);
            TokenData admin = new TokenData()
            {
                Password = Encoding.ASCII.GetBytes("test"),
                PasswordKey = Encoding.ASCII.GetBytes("key"),
                Tid = 4,
                Phone = "7338985215",
                Plan = "Basic",
                Role = "seller",
                UserEmail = "test3",
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
