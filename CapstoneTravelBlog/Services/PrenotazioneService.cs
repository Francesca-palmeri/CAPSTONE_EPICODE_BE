namespace CapstoneTravelBlog.Services
{
    using CapstoneTravelBlog.Data;
    using CapstoneTravelBlog.DTOs.Prenotazioni;
    using Microsoft.EntityFrameworkCore;

    public class PrenotazioneService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PrenotazioneService> _logger;

        public PrenotazioneService(ApplicationDbContext context, ILogger<PrenotazioneService> logger)
        {
            _context = context;
            _logger = logger;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        
        public async Task<Prenotazione?> CreatePrenotazioneAsync(AddPrenotazioneDto dto)
        {
            try
            {
                
                var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UtenteId);
                if (!userExists) return null;

                var viaggioExists = await _context.Viaggi.AnyAsync(v => v.Id == dto.ViaggioId);
                if (!viaggioExists) return null;

                var pren = new Prenotazione
                {
                    DataPrenotazione = dto.DataPrenotazione ?? DateTime.Now,
                    UtenteId = dto.UtenteId,
                    ViaggioId = dto.ViaggioId,
                    NumeroPartecipanti = dto.NumeroPartecipanti,
                    Tipologia = dto.Tipologia,
                    Note = dto.Note
                };
                return pren;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        
        public async Task<bool> AddPrenotazioneAsync(Prenotazione pren)
        {
            try
            {
                _context.Prenotazioni.Add(pren);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

       
        public async Task<List<Prenotazione>?> GetPrenotazioniAsync()
        {
            try
            {
                return await _context.Prenotazioni
                    .Include(p => p.Utente)
                    .Include(p => p.Viaggio)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        
        public async Task<List<GetPrenotazioneDto>?> GetPrenotazioniDtoAsync(List<Prenotazione> prenotazioni)
        {
            try
            {
                var prenDto = prenotazioni.Select(p => new GetPrenotazioneDto
                {
                    Id = p.Id,
                    DataPrenotazione = p.DataPrenotazione,
                    UtenteId = p.UtenteId,
                    NomeUtente = p.Utente?.FirstName,
                    CognomeUtente = p.Utente?.LastName,
                    ViaggioId = p.ViaggioId,
                    TitoloViaggio = p.Viaggio?.Titolo,
                    NumeroPartecipanti = p.NumeroPartecipanti,
                    Tipologia = p.Tipologia,
                    Note = p.Note,
                }).ToList();

                return prenDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<GetPrenotazioneDto?> GetPrenotazioneDtoByIdAsync(int id)
        {
            try
            {
                var p = await _context.Prenotazioni
                    .Include(p => p.Utente)
                    .Include(p => p.Viaggio)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (p == null) return null;

                var dto = new GetPrenotazioneDto
                {
                    Id = p.Id,
                    DataPrenotazione = p.DataPrenotazione,
                    UtenteId = p.UtenteId,
                    NomeUtente = p.Utente?.FirstName,
                    CognomeUtente = p.Utente?.LastName,
                    ViaggioId = p.ViaggioId,
                    TitoloViaggio = p.Viaggio?.Titolo,
                    NumeroPartecipanti = p.NumeroPartecipanti,
                    Tipologia = p.Tipologia,
                    Note = p.Note,
                };
                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

      
        public async Task<bool> DeletePrenotazioneAsync(int id)
        {
            try
            {
                var existing = await _context.Prenotazioni.FirstOrDefaultAsync(p => p.Id == id);
                if (existing == null) return false;

                _context.Prenotazioni.Remove(existing);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdatePrenotazioneAsync(int id, UpdatePrenotazioneDto dto)
        {
            try
            {
                var p = await _context.Prenotazioni.FirstOrDefaultAsync(x => x.Id == id);
                if (p == null) return false;

                p.DataPrenotazione = dto.DataPrenotazione;
                p.UtenteId = dto.UtenteId;
                p.ViaggioId = dto.ViaggioId;
                p.NumeroPartecipanti = dto.NumeroPartecipanti;
                p.Tipologia = dto.Tipologia;
                p.Note = dto.Note;

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<List<Prenotazione>?> GetPrenotazioniByUtenteAsync(string userId)
        {
            try
            {
                return await _context.Prenotazioni
                    .Include(p => p.Utente)
                    .Include(p => p.Viaggio)
                    .Where(p => p.UtenteId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }

}
