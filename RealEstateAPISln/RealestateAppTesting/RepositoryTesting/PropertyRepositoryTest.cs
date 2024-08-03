using Microsoft.AspNetCore.Http;
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
    public class PropertyRepositoryTest
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
            var content = "This is a test file";
            var fileName = "test.txt";
            var fileMock = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes(content)), 0, content.Length, "id_from_form", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };
            var media = new Media { Type = "Image", File = fileMock };
            IRepository<int, Property> repository = new PropertyRepository(context);
            Property admin = new Property()
            {
                PId = 1,
                Phone = "7338985215",
                UserEmail = "test",
                Name = "test",
                ResidentialSubtype = 0,
                Location = "test",
                Price = 10000,
                PropertyType = 0,
                PropertyDetails = new PropertyDetails
                {
                    CommercialAreaInSqFt = 1300,
                    HasConstructions = true,
                    PropertyDimensionsLength = 100,
                    PropertyDimensionsWidth = 100,
                    WidthofFacingRoad = 1000
                },
                Media = new List<Media> { media }
            };
            var result = await repository.Add(admin);
            Assert.That(result.UserEmail, Is.EqualTo("test"));
        }

        [Test]
        public async Task DelTest()
        {
            var content = "This is a test file";
            var fileName = "test.txt";
            var fileMock = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes(content)), 0, content.Length, "id_from_form", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };
            var media = new Media { Type = "Image", File = fileMock };
            IRepository<int, Property> repository = new PropertyRepository(context);
            Property admin = new Property()
            {
                PId = 2,
                Phone = "7338985215",
                UserEmail = "test",
                Name = "test",
                ResidentialSubtype = 0,
                Location = "test",
                Price = 10000,
                PropertyType = 0,
                PropertyDetails = new PropertyDetails
                {
                    CommercialAreaInSqFt = 1300,
                    HasConstructions = true,
                    PropertyDimensionsLength = 100,
                    PropertyDimensionsWidth = 100,
                    WidthofFacingRoad = 1000
                },
                Media = new List<Media> { media }
            };
            var repo = await repository.Add(admin);
            var result = await repository.Delete(repo.PId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTest()
        {
            IRepository<int, Property> repository = new PropertyRepository(context);


            var result = await repository.Get(1);
            result.Phone = "1234567890";
            var res = await repository.Update(result);
            Assert.AreEqual("test", res.UserEmail);
        }

        [Test]
        public async Task GetTest()
        {
            var content = "This is a test file";
            var fileName = "test.txt";
            var fileMock = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes(content)), 0, content.Length, "id_from_form", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };
            var media = new Media { Type = "Image", File = fileMock };
            IRepository<int, Property> repository = new PropertyRepository(context);
            Property admin = new Property()
            {
                PId = 3,
                Phone = "7338985215",
                UserEmail = "test",
                Name = "test",
                ResidentialSubtype = 0,
                Location = "test",
                Price = 10000,
                PropertyType = 0,
                PropertyDetails = new PropertyDetails
                {
                    CommercialAreaInSqFt = 1300,
                    HasConstructions = true,
                    PropertyDimensionsLength = 100,
                    PropertyDimensionsWidth = 100,
                    WidthofFacingRoad = 1000
                },
                Media = new List<Media> { media }
            };
            var rep = await repository.Add(admin);
            var result = await repository.Get(rep.PId);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTest()
        {
            var content = "This is a test file";
            var fileName = "test.txt";
            var fileMock = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes(content)), 0, content.Length, "id_from_form", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };
            var media = new Media { Type = "Image", File = fileMock };
            IRepository<int, Property> repository = new PropertyRepository(context);
            Property admin = new Property()
            {
                PId = 4,
                Phone = "7338985215",
                UserEmail = "test",
                ResidentialSubtype = 0,
                Name="test",
                Location = "test",
                Price = 10000,
                PropertyType = 0,
                PropertyDetails = new PropertyDetails
                {
                    CommercialAreaInSqFt = 1300,
                    HasConstructions = true,
                    PropertyDimensionsLength = 100,
                    PropertyDimensionsWidth = 100,
                    WidthofFacingRoad = 1000
                },
                Media = new List<Media> { media }
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
