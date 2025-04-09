using CapstoneTravelBlog.Data;
using CapstoneTravelBlog.DTOs.Viaggio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneTravelBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViaggiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ViaggioService> _logger;
        private readonly ViaggioService _viaggioService;

        public ViaggiController(
            ApplicationDbContext context,
            ILogger<ViaggioService> logger,
            ViaggioService viaggioService)
        {
            _context = context;
            _logger = logger;
            _viaggioService = viaggioService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddViaggio([FromBody] AddViaggioRequestDto dto)
        {
            var newViaggio = await _viaggioService.CreateViaggioAsync(dto);
            if (newViaggio == null)
            {
                return BadRequest(new AddViaggioResponseDto
                {
                    Message = "Failed to create a new Viaggio object!"
                });
            }

            var result = await _viaggioService.AddViaggioAsync(newViaggio);
            return result
                ? Ok(new AddViaggioResponseDto
                {
                    Message = "Viaggio correctly added!",
                    Contenuto = newViaggio
                  
                })
                : BadRequest(new AddViaggioResponseDto
                {
                    Message = "Something went wrong!"
                });
        }

        [HttpGet]
        public async Task<IActionResult> GetViaggi()
        {
            try
            {
                var viaggi = await _viaggioService.GetViaggiAsync();
                if (viaggi == null)
                {
                    return BadRequest(new GetViaggioResponseDto
                    {
                        Message = "Something went wrong",
                        Viaggi = null
                    });
                }

                if (!viaggi.Any())
                {
                    return NotFound(new GetViaggioResponseDto
                    {
                        Message = "No trips found!",
                        Viaggi = null
                    });
                }

                var viaggiDto = await _viaggioService.GetViaggiDtoAsync(viaggi);
                return Ok(new GetViaggioResponseDto
                {
                    Message = "Trips correctly retrieved!",
                    Viaggi = viaggiDto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong retrieving trips. {Error}", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetViaggioById(int id)
        {
            var viaggioDto = await _viaggioService.GetViaggioDtoByIdAsync(id);
            if (viaggioDto == null)
            {
                return BadRequest(new
                {
                    message = $"Viaggio with ID {id} not found or something went wrong."
                });
            }

            return Ok(new GetSingleViaggioResponseDto
            {
                Message = "Viaggio correctly found!",
                Viaggio = viaggioDto
            });
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateViaggio(int id, [FromBody] UpdateViaggioRequestDto dto)
        {
            var result = await _viaggioService.UpdateViaggioAsync(id, dto);
            return result
                ? Ok(new { Message = "Viaggio correctly updated!",
                           Details = dto })
                : BadRequest(new { Message = "Something went wrong!" });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteViaggio(int id)
        {
            var result = await _viaggioService.DeleteViaggioAsync(id);
            return result
                ? Ok(new { message = "Viaggio correctly removed!" })
                : BadRequest(new { message = "Something went wrong!" });
        }
    }
}

