using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Models.DTOs.Login;
using RealEstateAPI.Models;
using System.Diagnostics.CodeAnalysis;
using RealEstateAPI.Interfaces;
using RealEstateAPI.Models.DTOs.Properties;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [Authorize(Roles = "seller")]
        [HttpPost("PostProperty")]
        [EnableCors]
        [ProducesResponseType(typeof(Property), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<Property>> PostProperty(PostPropertyDTO postPropertyDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _propertyService.AddNewProperty(postPropertyDTO);
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
