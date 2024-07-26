using RealEstateAPI.Interfaces;
using RealEstateAPI.Exceptions;
using RealEstateAPI.Models;
using RealEstateAPI.Models.DTOs.Login;
using RealEstateAPI.Models.DTOs.Register;
using System.Security.Cryptography;
using System.Text;
using Twilio.Types;
using RealEstateAPI.Repositories;
using FirebaseAdmin.Auth;
using Twilio.Http;

namespace RealEstateAPI.Services
{
    public class LoginService : ILoginService
    {

        private readonly IRepository<string, User> _userRepo;
        private readonly IRepository<string, TokenData> _tokenRepo;
        private readonly OTPRepository _otpRepository;
        private readonly UserRepository _userRepository;
        private readonly ISmsService _smsService;
        private readonly ITokenService _tokenService;

        public LoginService(IRepository<string, User> userRepo, IRepository<string, TokenData> tokenRepo, ITokenService tokenService, OTPRepository otpRepository, ISmsService smsService, UserRepository userRepository)
        {
            _userRepo = userRepo;
            _tokenRepo = tokenRepo;
            _tokenService = tokenService;
            _otpRepository = otpRepository;
            _smsService = smsService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Generating Otp for mobile verification
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        /// <exception cref="NoUserException"></exception>
        public async Task<string> GenerateOTP(string phoneNumber)

        {
            if(await _userRepository.GetByPhoneNumber(phoneNumber) == null)
            {
                throw new NoUserException("No user with given number");
            }
            var otp = new Random().Next(100000, 999999).ToString();
            var expiration = DateTime.UtcNow.AddMinutes(5);

            // Remove any existing OTP
            var existingOtp = await _otpRepository.GetByPhoneNumber(phoneNumber);
            if (existingOtp != null)
            {
               await _otpRepository.Remove(existingOtp);
            }

            // Add new OTP
            var newOtpRecord = new OTP
            {
                Phone = phoneNumber,
                Otp = otp,
                Expiration = expiration
            };
            var res = await _otpRepository.Add(newOtpRecord);
            
            

            // Send OTP via SMS
           //var otp = await _smsService.SendOtpAsync(phoneNumber);
            _smsService.SendSms(phoneNumber, otp);

            return otp;
        }

        /// <summary>
        /// Login using credentials
        /// </summary>
        /// <param name="passwordDTO"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        public async Task<LoginTokenDTO> LoginPassword(PasswordDTO passwordDTO)
        {
            var userDB = await _tokenRepo.Get(passwordDTO.Email);
            if (userDB == null)
            {
                throw new UnauthorizedUserException("Invalid username or password");
            }
            HMACSHA512 hMACSHA = new HMACSHA512(userDB.PasswordKey);
            var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(passwordDTO.Password));
            bool isPasswordSame = ComparePassword(encrypterPass, userDB.Password);
            if (isPasswordSame)
            {
                var user = await _tokenRepo.Get(passwordDTO.Email);
                LoginTokenDTO loginReturnDTO = MapEmployeeToLoginReturn(user);
                return loginReturnDTO;

            }
            throw new UnauthorizedUserException("Invalid username or password");
        }
        private LoginTokenDTO MapEmployeeToLoginReturn(TokenData user)
        {
            LoginTokenDTO returnDTO = new LoginTokenDTO();
            returnDTO.Email = user.UserEmail;
            returnDTO.Role = user.Role;
            returnDTO.Token = _tokenService.GenerateToken(user);
            return returnDTO;
        }

        private bool ComparePassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }
        
        /// <summary>
        /// Register users
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        /// <exception cref="UnableToAddException"></exception>
        public async Task<User> Register(UserDTO userDTO)
        {
            TokenData tokenData = null;
            User user = null;
            try
            {
                
                user = await _userRepo.Add(userDTO);
                tokenData = MapUserDTOToTokenData(userDTO);
                tokenData = await _tokenRepo.Add(tokenData);
                return user;
            }
            catch (Exception ex) {
                if (tokenData != null)
                {
                    await RevertTokenInsert(tokenData);
                }
                if (user != null && tokenData == null)
                {
                    await RevertUserInsert(user);
                }
                throw new UnableToAddException("Not able to register at this moment");
            }
        }

        private async Task RevertUserInsert(User user)
        {
            await _userRepo.Delete(user.UserEmail);
        }

        private async Task RevertTokenInsert(TokenData tokenData)
        {
            await _tokenRepo.Delete(tokenData.UserEmail);
        }

        private TokenData MapUserDTOToTokenData(UserDTO userDTO)
        {
            TokenData tokenData = new TokenData();
            tokenData.UserEmail=userDTO.UserEmail;
            tokenData.Role=userDTO.Role;
            HMACSHA512 hMACSHA = new HMACSHA512();
            tokenData.PasswordKey= hMACSHA.Key;
            tokenData.Password= hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
            return tokenData;
        }


        /// <summary>
        /// Verify otp sent to mobile
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        public async Task<bool> VerifyOTP(string phoneNumber, string otp)
        {
            var otpRecord =await  _otpRepository.GetByPhoneNumber(phoneNumber);
            //FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance
            //    .VerifyIdTokenAsync(otpRecord.Otp);

            //// Token is valid
            //string uid = decodedToken.Uid;

            if (otpRecord != null && otpRecord.Otp == otp && DateTime.UtcNow < otpRecord.Expiration)
            {
                _otpRepository.Remove(otpRecord); 
                return true;
            }

            return false;
        }

        /// <summary>
        /// Switch role
        /// </summary>
        /// <param name="loginTokenDTO"></param>
        /// <returns></returns>
        /// <exception cref="NoUserException"></exception>

        public async Task<LoginTokenDTO> SwitchRole(LoginTokenDTO loginTokenDTO)
        {
            try
            {
                var userDB = await _tokenRepo.Get(loginTokenDTO.Email);
                LoginTokenDTO res = new LoginTokenDTO();
                if (userDB != null)
                {
                    userDB.Role = loginTokenDTO.Role;
                    await _tokenRepo.Update(userDB);
                    var td = await _tokenRepo.Get(loginTokenDTO.Email);
                    res.Token = loginTokenDTO.Token;
                    res.Email = td.UserEmail;
                    res.Role = td.Role;
                }
                return res;
            }
            catch (Exception ex) 
            {
                throw new NoUserException("No user with that email");
            }

        }
    }
}
