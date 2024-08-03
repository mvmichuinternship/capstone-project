using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using RealEstateAPI.Context;
using RealEstateAPI.Interfaces;
using RealEstateAPI.Models;
using RealEstateAPI.Models.DTOs.Login;
using RealEstateAPI.Models.DTOs.Register;
using RealEstateAPI.Repositories;
using RealEstateAPI.Services;

namespace RealestateAppTesting.ServiceTesting
{
    public class UserServiceTest
    {
        RealEstateAppContext context;

        IRepository<string, TokenData> tokenDataRepo;
        IRepository<string, User> userRepo;
        OTPRepository _otpRepository;
        UserRepository _userRepository;
        TokenRepository _tokenRepository;


        ISmsService _smsService;
        ILoginService registerService;
        ITokenService tokenService;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            context = new RealEstateAppContext(optionsBuilder.Options);

            tokenDataRepo = new TokenRepository(context);
            registerService = new LoginService(userRepo,_tokenRepository, tokenDataRepo, tokenService, _otpRepository, _smsService, _userRepository);
        }

        [Test]
        public void AddTest()
        {
            //Arrange
            UserDTO registerTokenData = new UserDTO()
            {
                Name = "test",
                Password = "test",
                Phone = "7338985215",
                Plan = "Basic",
                Role = "seller",
                UserEmail  = "test",
            };

            //Action
            var result = registerService.Register(registerTokenData);

            //Assert
            Assert.IsNotNull(result);

        }

        [Test]
        public void LoginTest()
        {
            //Arrange
            PasswordDTO registerCustomer = new PasswordDTO()
            {
                Password = "test",
                Email= "test",
            };

            //Action
            var result = registerService.LoginPassword(registerCustomer);

            //Assert
            Assert.IsNotNull(result);

        }


        [Test]
        public void GenerateSmsTest()
        {
            //Arrange
            var phone = "7338985215";

            //Action
            var result = registerService.GenerateOTP(phone);

            //Assert
            Assert.IsNotNull(result);

        }


        [Test]
        public void VerifySmsTest()
        {
            //Arrange
            var phone = "7338985215";
            var otp = "123456";

            //Action
            var result = registerService.VerifyOTP(phone, otp);

            //Assert
            Assert.IsNotNull(result);

        }

        [Test]
        public void SwitchRoleTest()
        {
            //Arrange
            LoginTokenDTO registerCustomer = new LoginTokenDTO()
            {
                Email = "test",
                Phone="7338985215",
                Plan= "Basic",
                Role = "seller",
                Token= "test",
            };

            //Action
            var result = registerService.SwitchRole(registerCustomer);

            //Assert
            Assert.IsNotNull(result);

        }

        [Test]
        public void SwitchPlanTest()
        {
            //Arrange
            string email = "test";
            bool upgrade = true;

            //Action
            var result = registerService.UpgradePlan(email,upgrade);

            //Assert
            Assert.IsNotNull(result);

        }

        [Test]
        public void CreateTokenTest()
        {
            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
            congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);
            ITokenService service = new TokenService(mockConfig.Object);

            //Action
            var token = service.GenerateToken(new TokenData { UserEmail="test", Role = "seller"  });

            //Assert
            Assert.IsNotNull(token);

        }
        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
