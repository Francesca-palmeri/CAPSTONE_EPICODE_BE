using CapstoneTravelBlog.Data;
using CapstoneTravelBlog.DTOs.Viaggio;
using CapstoneTravelBlog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneTravelBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiorniViaggioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GiornoViaggioService> _logger;
        private readonly GiornoViaggioService _giornoService;

        public GiorniViaggioController(
            ApplicationDbContext context,
            ILogger<GiornoViaggioService> logger,
            GiornoViaggioService giornoService)
        {
            _context = context;
            _logger = logger;
            _giornoService = giornoService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddGiorno([FromBody] AddGiornoViaggioDto ViaggioDto)
        {
            var newGiorno = await _giornoService.CreateGiornoAsync(ViaggioDto);
            if (newGiorno == null)
            {
                return BadRequest(new
                {
                    Message = "Failed to create a new GiornoViaggio object!"
                });
            }

            var result = await _giornoService.AddGiornoAsync(newGiorno);
            return result
                ? Ok(new {
                    Message = "Day correctly added to trip!",
                    Content = newGiorno
                })
                : BadRequest(new { Message = "Something went wrong adding the day!" });
        }

        [HttpGet("by-viaggio/{viaggioId:int}")]
        public async Task<IActionResult> GetGiorniByViaggio(int viaggioId)
        {
            var giorni = await _giornoService.GetGiorniByViaggioAsync(viaggioId);
            if (giorni == null)
            {
                return BadRequest(new
                {
                    Message = "Something went wrong fetching days or the trip does not exist.",
                    Giorni = (object)null
                });
            }
            if (!giorni.Any())
            {
                return NotFound(new
                {
                    Message = $"No days found for viaggio with ID={viaggioId}",
                    Giorni = (object)null
                });
            }

            var giorniDto = await _giornoService.GetGiorniDtoAsync(giorni);
            return Ok(new
            {
                Message = "Days for the trip found!",
                Giorni = giorniDto
            });
        }

        [HttpGet("{giornoId:int}")]
        public async Task<IActionResult> GetGiornoById(int giornoId)
        {
            var giornoDto = await _giornoService.GetGiornoDtoByIdAsync(giornoId);
            if (giornoDto == null)
            {
                return BadRequest(new
                {
                    Message = $"Day with ID {giornoId} not found."
                });
            }

            return Ok(new
            {
                Message = "Day found!",
                Giorno = giornoDto
            });
        }

        [HttpPut("{giornoId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGiorno(int giornoId, [FromBody] UpdateGiornoViaggioDto dto)
        {
            var result = await _giornoService.UpdateGiornoAsync(giornoId, dto);
            return result
                ? Ok(new { 
                    Message = "Day updated!",
                    Content = dto        
                             })
                : BadRequest(new { Message = "Something went wrong updating the day!" });
        }

        [HttpDelete("{giornoId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGiorno(int giornoId)
        {
            var result = await _giornoService.DeleteGiornoAsync(giornoId);
            return result
                ? Ok(new { Message = "Day correctly removed!" })
                : BadRequest(new { Message = "Something went wrong removing the day!" });
        }
    }
}
