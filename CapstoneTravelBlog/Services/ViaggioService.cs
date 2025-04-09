using CapstoneTravelBlog.Data;
using CapstoneTravelBlog.DTOs.Viaggio;
using Microsoft.EntityFrameworkCore;

public class ViaggioService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ViaggioService> _logger;

    public ViaggioService(ApplicationDbContext context, ILogger<ViaggioService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Salvataggio con gestione eccezioni
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

    // Crea un Viaggio a partire dalla DTO di creazione (senza salvarlo ancora su DB)
    public async Task<Viaggio?> CreateViaggioAsync(AddViaggioRequestDto dto)
    {
        try
        {
            var newViaggio = new Viaggio
            {
                Titolo = dto.Titolo,
                ImmagineCopertina = dto.ImmagineCopertina,
                Descrizione = dto.Descrizione,
                DataPartenza = dto.DataPartenza,
                DataRitorno = dto.DataRitorno,
                DurataGiorni = dto.DurataGiorni,
                Prezzo = dto.Prezzo,
                Tipologia = dto.Tipologia
            };
            return newViaggio;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    // Aggiunge il Viaggio creato sopra al DB e salva
    public async Task<bool> AddViaggioAsync(Viaggio viaggio)
    {
        try
        {
            _context.Viaggi.Add(viaggio);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
    }

    // Ottiene tutte le entità Viaggio (includendo la lista di GiorniViaggio)
    public async Task<List<Viaggio>?> GetViaggiAsync()
    {
        try
        {
            return await _context.Viaggi
                .Include(v => v.ProgrammaGiorni)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    // Mappa la lista di Viaggi in una lista di DTO
    public async Task<List<GetViaggioDto>?> GetViaggiDtoAsync(List<Viaggio> viaggi)
    {
        try
        {
            var viaggiDto = viaggi.Select(v => new GetViaggioDto
            {
                Id = v.Id,
                Titolo = v.Titolo,
                ImmagineCopertina = v.ImmagineCopertina,
                Descrizione = v.Descrizione,
                DataPartenza = v.DataPartenza,
                DataRitorno = v.DataRitorno,
                DurataGiorni = v.DurataGiorni,
                Prezzo = v.Prezzo,
                Tipologia = v.Tipologia,
                Giorni = v.ProgrammaGiorni?.Select(g => new GetGiornoViaggioDto
                {
                    Id = g.Id,
                    GiornoNumero = g.GiornoNumero,
                    Titolo = g.Titolo,
                    Descrizione = g.Descrizione
                }).ToList() ?? new List<GetGiornoViaggioDto>()
            }).ToList();

            return viaggiDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    // Restituisce la DTO di un singolo Viaggio (inclusi i giorni) cercato per Id
    public async Task<GetViaggioDto?> GetViaggioDtoByIdAsync(int id)
    {
        try
        {
            var viaggio = await _context.Viaggi
                .Include(v => v.ProgrammaGiorni)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (viaggio == null) return null;

            var viaggioDto = new GetViaggioDto
            {
                Id = viaggio.Id,
                Titolo = viaggio.Titolo,
                ImmagineCopertina = viaggio.ImmagineCopertina,
                Descrizione = viaggio.Descrizione,
                DataPartenza = viaggio.DataPartenza,
                DataRitorno = viaggio.DataRitorno,
                DurataGiorni = viaggio.DurataGiorni,
                Prezzo = viaggio.Prezzo,
                Tipologia = viaggio.Tipologia,
                Giorni = viaggio.ProgrammaGiorni?.Select(g => new GetGiornoViaggioDto
                {
                    Id = g.Id,
                    GiornoNumero = g.GiornoNumero,
                    Titolo = g.Titolo,
                    Descrizione = g.Descrizione
                }).ToList() ?? new List<GetGiornoViaggioDto>()
            };
            return viaggioDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    // Cancella un Viaggio esistente
    public async Task<bool> DeleteViaggioAsync(int id)
    {
        try
        {
            var existingViaggio = await _context.Viaggi.FirstOrDefaultAsync(v => v.Id == id);
            if (existingViaggio == null) return false;

            _context.Viaggi.Remove(existingViaggio);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
    }

    // Aggiorna un Viaggio esistente con i dati in DTO
    public async Task<bool> UpdateViaggioAsync(int id, UpdateViaggioRequestDto dto)
    {
        try
        {
            var existingViaggio = await _context.Viaggi.FirstOrDefaultAsync(v => v.Id == id);
            if (existingViaggio == null) return false;

            existingViaggio.Titolo = dto.Titolo;
            existingViaggio.ImmagineCopertina = dto.ImmagineCopertina;
            existingViaggio.Descrizione = dto.Descrizione;
            existingViaggio.DataPartenza = dto.DataPartenza;
            existingViaggio.DataRitorno = dto.DataRitorno;
            existingViaggio.DurataGiorni = dto.DurataGiorni;
            existingViaggio.Prezzo = dto.Prezzo;
            existingViaggio.Tipologia = dto.Tipologia;

            return await SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
    }

   
}
