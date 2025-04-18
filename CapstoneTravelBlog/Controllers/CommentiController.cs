using CapstoneTravelBlog.Data;
using CapstoneTravelBlog.DTOs.Blog;
using CapstoneTravelBlog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class CommentiController : ControllerBase
{
    private readonly CommentoService _service;
    private readonly ApplicationDbContext _context;

    public CommentiController(CommentoService service, ApplicationDbContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> AddCommento([FromBody] AddCommentoDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var commento = await _service.CreateCommentoAsync(dto, userId);
        if (commento == null)
            return BadRequest(new { Message = "Impossibile creare il commento." });

        var result = await _service.AddCommentoAsync(commento);
        if (!result)
            return BadRequest(new { Message = "Errore durante il salvataggio del commento." });

        var user = await _context.Users.FindAsync(userId);

        return Ok(new
        {
            id = commento.Id,
            testo = commento.Testo,
            dataCreazione = commento.DataCreazione,
            utenteId = user?.Id,
            utenteNome = user != null ? $"{user.FirstName} {user.LastName}" : "Anonimo",
            avatarUrl = user?.AvatarUrl
        });
    }

    [HttpGet("by-post/{postId:int}")]
    public async Task<IActionResult> GetCommentiByPost(int postId)
    {
        var commenti = await _service.GetCommentiByPostIdAsync(postId);
        return Ok(new
        {
            message = "Commenti trovati.",
            commenti = commenti
        });
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> UpdateCommento(int id, [FromBody] UpdateCommentoDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userIsAdmin = User.IsInRole("Admin");

        var commento = await _service.GetCommentoByIdAsync(id);
        if (commento == null)
            return NotFound("Commento non trovato.");

        if (commento.UtenteId != userId && !userIsAdmin)
            return Forbid("Non hai i permessi per modificare questo commento.");

        var result = await _service.UpdateCommentoAsync(id, dto.Testo);
        return result
            ? Ok(new { Message = "Commento aggiornato con successo.", nuovoTesto = dto.Testo })
            : BadRequest(new { Message = "Errore durante l'aggiornamento del commento." });
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> DeleteCommento(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userIsAdmin = User.IsInRole("Admin");

        var commento = await _service.GetCommentoByIdAsync(id);
        if (commento == null)
            return NotFound("Commento non trovato.");

        if (commento.UtenteId != userId && !userIsAdmin)
            return Forbid("Non hai i permessi per eliminare questo commento.");

        var result = await _service.DeleteCommentoAsync(id);
        return result
            ? Ok(new { Message = "Commento eliminato con successo." })
            : BadRequest(new { Message = "Errore durante l'eliminazione del commento." });
    }
}
