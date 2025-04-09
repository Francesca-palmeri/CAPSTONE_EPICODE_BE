namespace CapstoneTravelBlog.Services
{
    using CapstoneTravelBlog.Data;
    using CapstoneTravelBlog.DTOs.Viaggio;
    using Microsoft.EntityFrameworkCore;

    public class GiornoViaggioService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GiornoViaggioService> _logger;

        public GiornoViaggioService(ApplicationDbContext context, ILogger<GiornoViaggioService> logger)
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

        // Crea l'oggetto (in memoria) ma non salva
        public async Task<GiornoViaggio?> CreateGiornoAsync(AddGiornoViaggioDto dto)
        {
            try
            {
                // Eventualmente controlla se esiste un viaggio con id dto.ViaggioId
                var viaggio = await _context.Viaggi.FindAsync(dto.ViaggioId);
                if (viaggio == null) return null;

                var giorno = new GiornoViaggio
                {
                    GiornoNumero = dto.GiornoNumero,
                    Titolo = dto.Titolo,
                    Descrizione = dto.Descrizione,
                    ViaggioId = dto.ViaggioId
                };
                return giorno;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        // Inserisce su DB
        public async Task<bool> AddGiornoAsync(GiornoViaggio giorno)
        {
            try
            {
                _context.GiorniViaggio.Add(giorno);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        // Ottiene tutti i Giorni di un determinato Viaggio
        public async Task<List<GiornoViaggio>?> GetGiorniByViaggioAsync(int viaggioId)
        {
            try
            {
                return await _context.GiorniViaggio
                    .Where(g => g.ViaggioId == viaggioId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        // Mappo la lista di GiornoViaggio in una lista di DTO
        public async Task<List<GetGiornoViaggioDto>?> GetGiorniDtoAsync(List<GiornoViaggio> giorni)
        {
            try
            {
                return giorni.Select(g => new GetGiornoViaggioDto
                {
                    Id = g.Id,
                    GiornoNumero = g.GiornoNumero,
                    Titolo = g.Titolo,
                    Descrizione = g.Descrizione,
                    ViaggioId = g.ViaggioId
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        // Ottiene il DTO di un singolo Giorno
        public async Task<GetGiornoViaggioDto?> GetGiornoDtoByIdAsync(int id)
        {
            try
            {
                var giorno = await _context.GiorniViaggio.FindAsync(id);
                if (giorno == null) return null;

                return new GetGiornoViaggioDto
                {
                    Id = giorno.Id,
                    GiornoNumero = giorno.GiornoNumero,
                    Titolo = giorno.Titolo,
                    Descrizione = giorno.Descrizione,
                    ViaggioId = giorno.ViaggioId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        // Elimina un Giorno
        public async Task<bool> DeleteGiornoAsync(int giornoId)
        {
            try
            {
                var giorno = await _context.GiorniViaggio.FindAsync(giornoId);
                if (giorno == null) return false;

                _context.GiorniViaggio.Remove(giorno);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        // Aggiorna un Giorno
        public async Task<bool> UpdateGiornoAsync(int giornoId, UpdateGiornoViaggioDto dto)
        {
            try
            {
                var giorno = await _context.GiorniViaggio.FindAsync(giornoId);
                if (giorno == null) return false;

                // controlla se esiste un nuovo viaggioId
                var viaggio = await _context.Viaggi.FindAsync(dto.ViaggioId);
                if (viaggio == null) return false;

                giorno.GiornoNumero = dto.GiornoNumero;
                giorno.Titolo = dto.Titolo;
                giorno.Descrizione = dto.Descrizione;
                giorno.ViaggioId = dto.ViaggioId; // per aggiornare il Viaggio associato

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

       
    }

}
