using CleanGo.Application.DTOs.Services;
using CleanGo.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanGo.API.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return Ok(services);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceDto createServiceDto)
        {
            var service = await _serviceService.CreateServiceAsync(createServiceDto);
            return CreatedAtAction(nameof(GetAllServices), new { id = service.Id }, service);
        }
    }
}
