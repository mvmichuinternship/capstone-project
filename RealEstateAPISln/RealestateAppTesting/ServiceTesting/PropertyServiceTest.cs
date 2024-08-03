

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using RealEstateAPI.Context;
using RealEstateAPI.Interfaces;
using RealEstateAPI.Models.DTOs.Login;
using RealEstateAPI.Models.DTOs.Register;
using RealEstateAPI.Models;
using RealEstateAPI.Repositories;
using RealEstateAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateAPI.Models.DTOs.Properties;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using RealEstateAPI.Enums;

namespace RealestateAppTesting.ServiceTesting
{
    public class PropertyServiceTest
    {
        RealEstateAppContext context;
        IRepository<int, Property> _propertyRepo;
        IRepository<int, PropertyDetails> _propertyDetailsRepo;
        IRepository<int, Media> _mediaRepo;
        IPropertyService propertyService;

        [SetUp]
        public async Task Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RealEstateAppContext>()
                                 .UseInMemoryDatabase("dummyDB");
            context = new RealEstateAppContext(optionsBuilder.Options);

            _propertyRepo = new PropertyRepository(context);
            _propertyDetailsRepo = new PropertyDetailsRepository(context);
            _mediaRepo = new MediaRepository(context);
            propertyService = new PropertyService(_propertyRepo, _propertyDetailsRepo, _mediaRepo);
        }

        [Test]
        public async Task AddTest()
        {
            // Arrange
            var content = "This is a test file";
            var fileName = "test.txt";
            var fileMock = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes(content)), 0, content.Length, "id_from_form", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };
            var media = new Media { Type = "Image", File = fileMock };

            var postPropertyDTO = new PostPropertyDTO
            {
                PId =1,
                Name = "test",
                UserEmail = "test",
                CommercialSubtype = "Plot",
                Location = "test",
                Phone = "test",
                Price = 10000,
                PropertyType = "Commercial",
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

            // Act
            var result = await propertyService.AddNewProperty(postPropertyDTO);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task ViewTest()
        {
            // Arrange
            //var property = new Property
            //{
            //    PId = 5,
            //    Name = "test",
            //    UserEmail = "test",
            //    CommercialSubtype = CommercialSubtype.Plot,
            //    Location = "test",
            //    Phone = "test",
            //    Price = 10000,
            //    PropertyType = PropertyType.Commercial,
            //    PropertyDetails = new PropertyDetails
            //    {
            //        PId = 5,
            //        CommercialAreaInSqFt = 1300,
            //        HasConstructions = true,
            //        PropertyDimensionsLength = 100,
            //        PropertyDimensionsWidth = 100,
            //        WidthofFacingRoad = 1000
            //    },
            //    Media = new List<Media>()
            //};
            //context.Properties.Add(property);
            //await context.SaveChangesAsync();

            var id = 5;

            // Act
            var result = await propertyService.GetPropertyById(id);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTest()
        {
            // Arrange

            var content1 = "This is a test file";
            var fileName1 = "test.txt";
            var fileMock1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes(content1)), 0, content1.Length, "id_from_form", fileName1)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };
            var media1 = new Media { Type = "Image", File = fileMock1, PropertyPId = 1 };
            var property = new Property
            {
                PId = 1,
                Name = "test",
                UserEmail = "test",
                CommercialSubtype = CommercialSubtype.Plot,
                Location = "test",
                Phone = "test",
                Price = 10000,
                PropertyType = PropertyType.Commercial,
                PropertyDetails = new PropertyDetails
                {
                    PId = 1,
                    CommercialAreaInSqFt = 1300,
                    HasConstructions = true,
                    PropertyDimensionsLength = 100,
                    PropertyDimensionsWidth = 100,
                    WidthofFacingRoad = 1000
                },
                Media = new List<Media>() {media1 }
            };
            context.Properties.Add(property);
            await context.SaveChangesAsync();

            var content = "This is a test file";
            var fileName = "test.txt";
            var fileMock = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes(content)), 0, content.Length, "id_from_form", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };
            var media = new Media { Type = "Image", File = fileMock, PropertyPId = 1 };

            var postPropertyDTO = new PostPropertyDTO
            {
                PId = 1,
                Name = "test",
                UserEmail = "test",
                CommercialSubtype = "Plot",
                Location = "test",
                Phone = "test",
                Price = 10000,
                PropertyType = "Commercial",
                PropertyDetails = new PropertyDetails
                {
                    PId = 1,
                    CommercialAreaInSqFt = 1300,
                    HasConstructions = true,
                    PropertyDimensionsLength = 100,
                    PropertyDimensionsWidth = 100,
                    WidthofFacingRoad = 1000
                },
                Media = new List<Media> { media }
            };

            // Act
            var result = await propertyService.UpdateProperty(postPropertyDTO);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task DeleteTest()
        {
            // Arrange
            var property = new Property
            {
                PId = 2,
                Name = "test",
                UserEmail = "test",
                CommercialSubtype = CommercialSubtype.Plot,
                Location = "test",
                Phone = "test",
                Price = 10000,
                PropertyType = PropertyType.Commercial,
                PropertyDetails = new PropertyDetails
                {
                    PId = 2,
                    CommercialAreaInSqFt = 1300,
                    HasConstructions = true,
                    PropertyDimensionsLength = 100,
                    PropertyDimensionsWidth = 100,
                    WidthofFacingRoad = 1000
                },
                Media = new List<Media>()
            };
            context.Properties.Add(property);
            await context.SaveChangesAsync();

            var id = 1;

            // Act
            var result = await propertyService.RemoveProperty(id);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAll()
        {
            // Act
            var result = await propertyService.GetAllProperties();

            // Assert
            Assert.IsNotNull(result);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
