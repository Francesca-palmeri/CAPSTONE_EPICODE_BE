using CapstoneTravelBlog.DTOs;
using CapstoneTravelBlog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneTravelBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContattiController : ControllerBase
    {
        private readonly ContattoService _contattoService;

        public ContattiController(ContattoService contattoService)
        {
            _contattoService = contattoService;
        }

        [HttpPost]
        public async Task<IActionResult> InviaContatto([FromBody] ContattoRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _contattoService.InviaMessaggioAsync(dto);
            if (!success)
                return StatusCode(500, "Errore nell'invio della mail");

            return Ok(new { Message = "Messaggio inviato correttamente!" });
        }

    }
}
