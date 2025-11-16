using CleanGo.Application.DTOs.Cleaners;
using CleanGo.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanGo.API.Controllers
{
    [ApiController]
    [Route("api/cleaners")]
    public class CleanersController : ControllerBase
    {
        private readonly ICleanerService _cleanerService;

        public CleanersController(ICleanerService cleanerService)
        {
            _cleanerService = cleanerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCleaners()
        {
            var cleaners = await _cleanerService.GetAllCleanersAsync();
            return Ok(cleaners);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCleaner([FromBody] CreateCleanerDto createCleanerDto)
        {
            var cleaner = await _cleanerService.CreateCleanerAsync(createCleanerDto);
            return CreatedAtAction(nameof(GetAllCleaners), new { id = cleaner.Id }, cleaner);
        }
    }
}
