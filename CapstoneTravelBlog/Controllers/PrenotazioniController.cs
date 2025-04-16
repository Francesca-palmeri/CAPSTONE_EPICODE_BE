using CapstoneTravelBlog.Data;
using CapstoneTravelBlog.DTOs.Prenotazioni;
using CapstoneTravelBlog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PrenotazioniController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<PrenotazioneService> _logger;
    private readonly PrenotazioneService _service;

    public PrenotazioniController(
        ApplicationDbContext context,
        ILogger<PrenotazioneService> logger,
        PrenotazioneService service)
    {
        _context = context;
        _logger = logger;
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "User , Admin")]
    public async Task<IActionResult> AddPrenotazione([FromBody] AddPrenotazioneDto dto)
    {
        var newPren = await _service.CreatePrenotazioneAsync(dto);
        if (newPren == null)
        {
            return BadRequest(new
            {
                Message = "Failed to create a new Prenotazione object! Check if Utente or Viaggio exist."
            });
        }

        var result = await _service.AddPrenotazioneAsync(newPren);
        return result
            ? Ok(new { Message = "Booking correctly added!" })
            : BadRequest(new { Message = "Something went wrong saving the booking!" });
    }

    [HttpGet]
    [Authorize(Roles = "Admin, User")] 
    public async Task<IActionResult> GetPrenotazioni()
    {
        var prenotazioni = await _service.GetPrenotazioniAsync();
        if (prenotazioni == null)
        {
            return BadRequest(new
            {
                Message = "Something went wrong getting the bookings.",
                Prenotazioni = (object)null
            });
        }
        if (!prenotazioni.Any())
        {
            return NotFound(new
            {
                Message = "No bookings found!",
                Prenotazioni = (object)null
            });
        }

        var prenDto = await _service.GetPrenotazioniDtoAsync(prenotazioni);
        return Ok(new
        {
            Message = "Bookings successfully retrieved!",
            Prenotazioni = prenDto
        });
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> GetPrenotazioneById(int id)
    {
        var prenDto = await _service.GetPrenotazioneDtoByIdAsync(id);
        if (prenDto == null)
        {
            return BadRequest(new
            {
                Message = $"Booking with ID {id} not found or something went wrong."
            });
        }

        return Ok(new
        {
            Message = "Booking found!",
            Prenotazione = prenDto
        });
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> UpdatePrenotazione(int id, [FromBody] UpdatePrenotazioneDto dto)
    {
        var result = await _service.UpdatePrenotazioneAsync(id, dto);
        return result
            ? Ok(new { Message = "Booking updated!" })
            : BadRequest(new { Message = "Something went wrong updating the booking!" });
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePrenotazione(int id)
    {
        var result = await _service.DeletePrenotazioneAsync(id);
        return result
            ? Ok(new { Message = "Booking correctly removed!" })
            : BadRequest(new { Message = "Something went wrong removing the booking!" });
    }

    // Esempio: GET /api/Prenotazioni/utente/someUserId
    [HttpGet("utente/{userId}")]
    public async Task<IActionResult> GetPrenotazioniByUtente(string userId)
    {
        var prenotazioni = await _service.GetPrenotazioniByUtenteAsync(userId);
        if (prenotazioni == null)
        {
            return BadRequest(new
            {
                Message = "Something went wrong fetching user bookings.",
                Prenotazioni = (object)null
            });
        }
        if (!prenotazioni.Any())
        {
            return NotFound(new
            {
                Message = "No bookings found for the given user!",
                Prenotazioni = (object)null
            });
        }

        var prenDto = await _service.GetPrenotazioniDtoAsync(prenotazioni);
        return Ok(new
        {
            Message = "User bookings retrieved!",
            Prenotazioni = prenDto
        });
    }
}
