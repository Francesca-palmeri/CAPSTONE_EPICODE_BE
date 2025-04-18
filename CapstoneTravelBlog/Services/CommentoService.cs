using CapstoneTravelBlog.Data;
using CapstoneTravelBlog.DTOs.Blog;
using CapstoneTravelBlog.Models;
using CapstoneTravelBlog.Models.Account;
using Microsoft.EntityFrameworkCore;

public class CommentoService
{
    private readonly ApplicationDbContext _context;

    public CommentoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Commento?> CreateCommentoAsync(AddCommentoDto dto, string userId)
    {
        var user = await _context.Users.FindAsync(userId);
        var post = await _context.BlogPosts.FindAsync(dto.BlogPostId);
        if (user == null || post == null) return null;

        var commento = new Commento
        {
            Testo = dto.Testo,
            BlogPostId = dto.BlogPostId,
            UtenteId = userId,
            DataCreazione = DateTime.Now
        };

        return commento;
    }

    public async Task<bool> AddCommentoAsync(Commento commento)
    {
        _context.Commenti.Add(commento);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<GetCommentoDto>> GetCommentiByPostIdAsync(int blogPostId)
    {
        return await _context.Commenti
            .Where(c => c.BlogPostId == blogPostId)
            .Include(c => c.Utente)
            .OrderByDescending(c => c.DataCreazione)
            .Select(c => new GetCommentoDto
            {
                Id = c.Id,
                Testo = c.Testo,
                DataCreazione = c.DataCreazione,
                UtenteId = c.UtenteId,
                UtenteNome = c.Utente!.FirstName + " " + c.Utente.LastName,
                AvatarUrl = c.Utente.AvatarUrl
            })
            .ToListAsync();
    }

    public async Task<Commento?> GetCommentoByIdAsync(int id)
    {
        return await _context.Commenti
            .Include(c => c.Utente)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> UpdateCommentoAsync(int id, string nuovoTesto)
    {
        var commento = await _context.Commenti.FindAsync(id);
        if (commento == null) 
            return false;

        commento.Testo = nuovoTesto;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteCommentoAsync(int id)
    {
        var commento = await _context.Commenti.FindAsync(id);
        if (commento == null) 
            return false;

        _context.Commenti.Remove(commento);
        return await _context.SaveChangesAsync() > 0;
    }

}
