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
        private readonly TokenRepository _tokenRepository;
        private readonly ISmsService _smsService;
        private readonly ITokenService _tokenService;

        public LoginService(IRepository<string, User> userRepo, TokenRepository tokenRepository, IRepository<string, TokenData> tokenRepo, ITokenService tokenService, OTPRepository otpRepository, ISmsService smsService, UserRepository userRepository)
        {
            _userRepo = userRepo;
            _tokenRepo = tokenRepo;
            _tokenService = tokenService;
            _otpRepository = otpRepository;
            _smsService = smsService;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
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
            returnDTO.Phone= user.Phone;
            returnDTO.Plan= user.Plan;
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
            tokenData.Phone=userDTO.Phone;
            tokenData.Plan=userDTO.Plan;
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
        public async Task<LoginTokenDTO> VerifyOTP(string phoneNumber, string otp)
        {
            var otpRecord =await  _otpRepository.GetByPhoneNumber(phoneNumber);
            var user = await _tokenRepository.GetByPhoneNumber(phoneNumber);

          

            if (user!=null && otpRecord != null && otpRecord.Otp == otp && DateTime.UtcNow < otpRecord.Expiration)
            {
                LoginTokenDTO loginReturnDTO = MapEmployeeToLoginReturn(user);
                _otpRepository.Remove(otpRecord);

                return loginReturnDTO;
            }

           throw new NoUserException("Invalid Otp");
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
                    res.Token = _tokenService.GenerateToken(td);
                    res.Email = td.UserEmail;
                    res.Role = td.Role;
                    res.Plan = td.Plan;
                    res.Phone = td.Phone;
                }
                return res;
            }
            catch (Exception ex) 
            {
                throw new NoUserException("No user with that email");
            }

        }
        /// <summary>
        /// Upgrade plan from basic to premium
        /// </summary>
        /// <param name="email"></param>
        /// <param name="upgradePlan"></param>
        /// <returns></returns>
        /// <exception cref="NoUserException"></exception>
        public async Task<LoginTokenDTO> UpgradePlan(string email, bool upgradePlan)
        {
            try
            {
                User user=null;
                User user2=null ;
                user = await _userRepo.Get(email);
                if (user != null)
                {
                    if (upgradePlan)
                    {
                        user.Plan = "Premium";
                        user2 =await _userRepo.Update(user);

                    }
                    else
                    {
                        user.Plan = "Basic";
                        user2 = await _userRepo.Update(user);
                    }
                    
                }
                var userDB = await _tokenRepo.Get(email);
                userDB.Plan= user2?.Plan;
                var tokenddata = await _tokenRepo.Update(userDB);
                LoginTokenDTO res = new LoginTokenDTO();
                if (userDB != null)
                {
                    
                    var td = await _tokenRepo.Get(email);
                    res.Token = _tokenService.GenerateToken(td);
                    res.Email = td.UserEmail;
                    res.Role = td.Role;
                    res.Plan = td.Plan;
                    res.Phone = td.Phone;
                }
                return res;

            }
            catch (Exception ex)
            {
                throw new NoUserException("User not found");
            }
        }
    }
}
