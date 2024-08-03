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
    public class MediaRepositoryTest
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
            IRepository<int, Media> repository = new MediaRepository(context);

            
            var result = await repository.Add(media);
            Assert.That(result.Type, Is.EqualTo("Image"));
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
            IRepository<int, Media> repository = new MediaRepository(context);
            var repo = await repository.Add(media);
            var result = await repository.Delete(repo.Id);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task UpdateTest()
        {
            IRepository<int, Media> repository = new MediaRepository(context);


            var result = await repository.Get(1);
            result.Type = "Video";
            var res = await repository.Update(result);
            Assert.AreEqual("Video", res.Type);
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
            IRepository<int, Media> repository = new MediaRepository(context);
            var rep = await repository.Add(media);
            var result = await repository.Get(rep.Id);
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
            IRepository<int, Media> repository = new MediaRepository(context);
            var rep = await repository.Add(media);
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
