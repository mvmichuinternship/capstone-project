using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Exceptions;
using RealEstateAPI.Interfaces;
using RealEstateAPI.Models;
using RealEstateAPI.Models.DTOs.Login;
using RealEstateAPI.Models.DTOs.Register;
using System.Diagnostics.CodeAnalysis;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("Register")]
        [EnableCors]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<User>> Register([FromForm] UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    User result = await _loginService.Register(userDTO);
                    return Ok(result);
                }
                catch (UnableToAddException ex)
                {
                    //_logger.LogCritical("Unable to add admin");
                    return BadRequest(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }


        [HttpPost("LoginwithPassword")]
        [EnableCors]
        [ProducesResponseType(typeof(LoginTokenDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<LoginTokenDTO>> Login([FromForm] PasswordDTO userLoginDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _loginService.LoginPassword(userLoginDTO);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    //_logger.LogCritical("User not authenticated");
                    return BadRequest(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }


        [HttpPost("GenerateSms")]
        [EnableCors]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<string>> GenerateOTP(string phone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _loginService.GenerateOTP(phone);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    //_logger.LogCritical("User not authenticated");
                    return BadRequest(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }


        [HttpPost("LoginViaOtp")]
        [EnableCors]
        [ProducesResponseType(typeof(LoginTokenDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<LoginTokenDTO>> VerifyOtp(string phone, string otp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _loginService.VerifyOTP(phone, otp);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    //_logger.LogCritical("User not authenticated");
                    return BadRequest(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }

        [Authorize(Roles ="seller,buyer")]
        [HttpPost("SwitchRoles")]
        [EnableCors]
        [ProducesResponseType(typeof(LoginTokenDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<LoginTokenDTO>> Switch([FromForm]LoginTokenDTO userLoginDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _loginService.SwitchRole(userLoginDTO);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    //_logger.LogCritical("User not authenticated");
                    return BadRequest(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }


        [Authorize(Roles = "seller,buyer")]
        [HttpPut("UpgradePlan")]
        [EnableCors]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<User>> UpgradePlan([FromForm]string email, bool upgradeplan)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _loginService.UpgradePlan(email,upgradeplan);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    //_logger.LogCritical("User not authenticated");
                    return BadRequest(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
    }
}
