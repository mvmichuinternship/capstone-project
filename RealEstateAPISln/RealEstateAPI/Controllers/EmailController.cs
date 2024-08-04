using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Models;
using RealEstateAPI.Models.DTOs.Email;
using RealEstateAPI.Services;
using System.Threading.Tasks;

namespace RealEstateAPI.Controllers
{
   

    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            try
            {

            await _emailService.SendEmailAsync(request.ToEmail, request.Subject, request.HtmlContent);
            return Ok();
            }
            catch (Exception ex)
            {
                //_logger.LogCritical("User not authenticated");
                return BadRequest(new ErrorModel(401, ex.Message));
            }
        }
    }

    

}
