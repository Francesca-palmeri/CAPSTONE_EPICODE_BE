using CapstoneTravelBlog.Data;
using CapstoneTravelBlog.DTOs.FrasiUtili;
using CapstoneTravelBlog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneTravelBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrasiUtiliController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FraseUtileService> _logger;
        private readonly FraseUtileService _fraseService;

        public FrasiUtiliController(
            ApplicationDbContext context,
            ILogger<FraseUtileService> logger,
            FraseUtileService fraseService)
        {
            _context = context;
            _logger = logger;
            _fraseService = fraseService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddFrase([FromBody] AddFraseUtileDto dto)
        {
            var newFrase = await _fraseService.CreateFraseAsync(dto);
            if (newFrase == null)
            {
                return BadRequest(new AddFraseUtileResponseDto
                {
                    Message = "Failed to create a new FraseUtile object!"
                });
            }

            var result = await _fraseService.AddFraseAsync(newFrase);
            return result
                ? Ok(new AddFraseUtileResponseDto
                {
                    Message = "Phrase correctly added!"
                })
                : BadRequest(new AddFraseUtileResponseDto
                {
                    Message = "Something went wrong!"
                });
        }

        [HttpGet]
        public async Task<IActionResult> GetFrasi()
        {
            try
            {
                var frasi = await _fraseService.GetFrasiAsync();
                if (frasi == null)
                {
                    return BadRequest(new GetFraseUtileResponseDto
                    {
                        Message = "Something went wrong",
                        Frasi = null
                    });
                }
                if (!frasi.Any())
                {
                    return NotFound(new GetFraseUtileResponseDto
                    {
                        Message = "No phrases found!",
                        Frasi = null
                    });
                }

                var frasiDto = await _fraseService.GetFrasiDtoAsync(frasi);
                return Ok(new GetFraseUtileResponseDto
                {
                    Message = "Phrases correctly retrieved!",
                    Frasi = frasiDto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong retrieving phrases. {Error}", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        
        public async Task<IActionResult> GetFraseById(int id)
        {
            var fraseDto = await _fraseService.GetFraseDtoByIdAsync(id);
            if (fraseDto == null)
            {
                return BadRequest(new
                {
                    message = $"Phrase with ID {id} not found or something went wrong."
                });
            }

            return Ok(new GetSingleFraseUtileResponseDto
            {
                Message = "Phrase correctly found!",
                Frase = fraseDto
            });
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateFrase(int id, [FromBody] UpdateFraseUtileDto dto)
        {
            var result = await _fraseService.UpdateFraseAsync(id, dto);
            return result
                ? Ok(new { Message = "Phrase correctly updated!" })
                : BadRequest(new { Message = "Something went wrong!" });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFrase(int id)
        {
            var result = await _fraseService.DeleteFraseAsync(id);
            return result
                ? Ok(new { message = "Phrase correctly removed!" })
                : BadRequest(new { message = "Something went wrong!" });
        }
    }
}

