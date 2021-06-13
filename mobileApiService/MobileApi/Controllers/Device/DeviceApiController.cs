using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileApi.Core.Repository;

namespace MobileApi.Controllers.Device
{
    [Authorize]
    [ApiController]
    [Route("api/device")]
    public class DeviceApiController : ControllerBase
    {
        private readonly IObjectRepository _repository;

        public DeviceApiController(IObjectRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("getAllDevices")]
        public async Task<IActionResult> GetAllDevices([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Переданны некорректные данные");
            
            return Ok();
        }
        
        [HttpGet("getDevice")]
        public async Task<IActionResult> GetDevice(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Переданны некорректные данные");
            
            return Ok();
        }
    }
}