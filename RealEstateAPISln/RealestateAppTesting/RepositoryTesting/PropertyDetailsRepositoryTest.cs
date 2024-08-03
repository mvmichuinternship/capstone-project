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
    public class PropertyDetailsRepositoryTest
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
            IRepository<int, PropertyDetails> repository = new PropertyDetailsRepository(context);

            PropertyDetails pd = new PropertyDetails
            {
                CommercialAreaInSqFt = 1300,
                HasConstructions = true,
                PropertyDimensionsLength = 100,
                PropertyDimensionsWidth = 100,
                WidthofFacingRoad = 1000,
                
            };
            var result = await repository.Add(pd);
            Assert.That(result.CommercialAreaInSqFt, Is.EqualTo(1300));
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
            IRepository<int, PropertyDetails> repository = new PropertyDetailsRepository(context);

            PropertyDetails pd = new PropertyDetails
            {
                CommercialAreaInSqFt = 1300,
                HasConstructions = true,
                PropertyDimensionsLength = 100,
                PropertyDimensionsWidth = 100,
                WidthofFacingRoad = 1000
            };
            var repo = await repository.Add(pd);
            var result = await repository.Delete(repo.Id);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTest()
        {
            IRepository<int, PropertyDetails> repository = new PropertyDetailsRepository(context);


            var result = await repository.Get(1);
            result.CommercialAreaInSqFt = 1200;
            var res = await repository.Update(result);
            Assert.AreEqual(1200, res.CommercialAreaInSqFt);
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
            IRepository<int, PropertyDetails> repository = new PropertyDetailsRepository(context);
            PropertyDetails admin = new PropertyDetails()
            
                {
                    CommercialAreaInSqFt = 1300,
                    HasConstructions = true,
                    PropertyDimensionsLength = 100,
                    PropertyDimensionsWidth = 100,
                    WidthofFacingRoad = 1000
                };
            var rep = await repository.Add(admin);
            var result = await repository.Get(rep.Id);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllTest()
        {
            
            IRepository<int, PropertyDetails> repository = new PropertyDetailsRepository(context);
            PropertyDetails admin = new PropertyDetails()
            
                {
                    CommercialAreaInSqFt = 1300,
                    HasConstructions = true,
                    PropertyDimensionsLength = 100,
                    PropertyDimensionsWidth = 100,
                    WidthofFacingRoad = 1000
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
