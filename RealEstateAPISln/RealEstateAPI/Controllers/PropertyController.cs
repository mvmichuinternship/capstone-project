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

        //[Authorize(Roles = "seller")]
        [HttpPost("PostProperty")]
        [EnableCors]
        [ProducesResponseType(typeof(Property), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<Property>> PostProperty([FromForm]PostPropertyDTO postPropertyDTO)
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

        //[Authorize(Roles = "seller")]
        [HttpPut("UpdateProperty")]
        [EnableCors]
        [ProducesResponseType(typeof(Property), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<Property>> UpdateProperty([FromForm]PostPropertyDTO postPropertyDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _propertyService.UpdateProperty(postPropertyDTO);
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

        //[Authorize(Roles = "seller")]
        [HttpDelete("DeleteProperty")]
        [EnableCors]
        [ProducesResponseType(typeof(Property), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<Property>> DeleteProperty(int postPropertyDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _propertyService.RemoveProperty(postPropertyDTO);
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

        //[Authorize(Roles = "seller,buyer")]
        [HttpGet("GetProperty")]
        [EnableCors]
        [ProducesResponseType(typeof(GetPropertyDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<GetPropertyDTO>> GetProperty(int property)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _propertyService.GetPropertyById(property);
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

        //[Authorize(Roles = "seller,buyer")]
        [HttpGet("GetProperties")]
        [EnableCors]
        [ProducesResponseType(typeof(IList<GetPropertyDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        [ExcludeFromCodeCoverage]
        public async Task<ActionResult<IList<GetPropertyDTO>>> GetAllProperties()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _propertyService.GetAllProperties();
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
