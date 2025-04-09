namespace CapstoneTravelBlog.Services
{
    using CapstoneTravelBlog.Data;
    using CapstoneTravelBlog.DTOs.FrasiUtili;
    using Microsoft.EntityFrameworkCore;

    public class FraseUtileService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FraseUtileService> _logger;

        public FraseUtileService(ApplicationDbContext context, ILogger<FraseUtileService> logger)
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

        public async Task<FraseUtile?> CreateFraseAsync(AddFraseUtileDto dto)
        {
            try
            {
                var newFrase = new FraseUtile
                {
                    Categoria = dto.Categoria,
                    Italiano = dto.Italiano,
                    GiapponeseKana = dto.GiapponeseKana,
                    Romaji = dto.Romaji,
                    AudioUrl = dto.AudioUrl
                };
                return newFrase;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> AddFraseAsync(FraseUtile frase)
        {
            try
            {
                _context.FrasiUtili.Add(frase);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<List<FraseUtile>?> GetFrasiAsync()
        {
            try
            {
                return await _context.FrasiUtili.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<GetFraseUtileDto>?> GetFrasiDtoAsync(List<FraseUtile> frasi)
        {
            try
            {
                var frasiDto = frasi.Select(f => new GetFraseUtileDto
                {
                    Id = f.Id,
                    Categoria = f.Categoria,
                    Italiano = f.Italiano,
                    GiapponeseKana = f.GiapponeseKana,
                    Romaji = f.Romaji,
                    AudioUrl = f.AudioUrl
                }).ToList();

                return frasiDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<GetFraseUtileDto?> GetFraseDtoByIdAsync(int id)
        {
            try
            {
                var frase = await _context.FrasiUtili.FirstOrDefaultAsync(f => f.Id == id);
                if (frase == null) return null;

                var fraseDto = new GetFraseUtileDto
                {
                    Id = frase.Id,
                    Categoria = frase.Categoria,
                    Italiano = frase.Italiano,
                    GiapponeseKana = frase.GiapponeseKana,
                    Romaji = frase.Romaji,
                    AudioUrl = frase.AudioUrl
                };
                return fraseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteFraseAsync(int id)
        {
            try
            {
                var existingFrase = await _context.FrasiUtili.FirstOrDefaultAsync(f => f.Id == id);
                if (existingFrase == null) return false;

                _context.FrasiUtili.Remove(existingFrase);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateFraseAsync(int id, UpdateFraseUtileDto dto)
        {
            try
            {
                var existingFrase = await _context.FrasiUtili.FirstOrDefaultAsync(f => f.Id == id);
                if (existingFrase == null) return false;

                existingFrase.Categoria = dto.Categoria;
                existingFrase.Italiano = dto.Italiano;
                existingFrase.GiapponeseKana = dto.GiapponeseKana;
                existingFrase.Romaji = dto.Romaji;
                existingFrase.AudioUrl = dto.AudioUrl;

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
