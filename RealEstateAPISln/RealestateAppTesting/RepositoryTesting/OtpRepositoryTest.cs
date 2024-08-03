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
    public class OtpRepositoryTest
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
            OTPRepository repository = new OTPRepository(context);
            OTP otp = new OTP()
            {
                Expiration = DateTime.UtcNow.AddMinutes(5),
                Otp = "123456",
                Phone = "7338985215"
            };
            var result = await repository.Add(otp);
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task GetTest()
        {
            OTPRepository repository = new OTPRepository(context);
            OTP otp = new OTP()
            {
                Expiration = DateTime.UtcNow.AddMinutes(5),
                Otp = "123456",
                Phone = "7338985213"
            };
            string phone = "7338985213";
            var result = await repository.Add(otp);
            var res = await repository.GetByPhoneNumber(phone);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task DelTest()
        {
            OTPRepository repository = new OTPRepository(context);
            OTP otp = new OTP()
            {
                Expiration = DateTime.UtcNow.AddMinutes(5),
                Otp = "123456",
                Phone = "7338985214"
            };
            var result = await repository.Add(otp);
            var res = await repository.Remove(otp);
            Assert.IsNotNull(result);
        }

        

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
