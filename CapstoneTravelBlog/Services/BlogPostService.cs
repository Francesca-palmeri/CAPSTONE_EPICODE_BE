namespace CapstoneTravelBlog.Services
{
    using CapstoneTravelBlog.Data;
    using CapstoneTravelBlog.DTOs.Blog;
    using Microsoft.EntityFrameworkCore;

    public class BlogPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BlogPostService> _logger;

        public BlogPostService(ApplicationDbContext context, ILogger<BlogPostService> logger)
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

        public async Task<BlogPost?> CreateBlogPostAsync(AddBlogPostDto dto)
        {
            try
            {
                var newPost = new BlogPost
                {
                    Titolo = dto.Titolo,
                    Contenuto = dto.Contenuto,
                    ImmagineCopertina = dto.ImmagineCopertina,
                    Categoria = dto.Categoria,
                    DataPubblicazione = DateTime.Now
                };
                return newPost;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> AddBlogPostAsync(BlogPost post)
        {
            try
            {
                _context.BlogPosts.Add(post);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<List<BlogPost>?> GetBlogPostsAsync()
        {
            try
            {
                return await _context.BlogPosts.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<GetBlogPostDto>?> GetBlogPostsDtoAsync(List<BlogPost> posts)
        {
            try
            {
                var postsDto = posts.Select(p => new GetBlogPostDto
                {
                    Id = p.Id,
                    Titolo = p.Titolo,
                    Contenuto = p.Contenuto,
                    ImmagineCopertina = p.ImmagineCopertina,
                    Categoria = p.Categoria,
                    DataPubblicazione = p.DataPubblicazione
                }).ToList();
                return postsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<GetBlogPostDto?> GetBlogPostDtoByIdAsync(int id)
        {
            try
            {
                var post = await _context.BlogPosts.FirstOrDefaultAsync(b => b.Id == id);
                if (post == null) return null;

                var postDto = new GetBlogPostDto
                {
                    Id = post.Id,
                    Titolo = post.Titolo,
                    Contenuto = post.Contenuto,
                    ImmagineCopertina = post.ImmagineCopertina,
                    Categoria = post.Categoria,
                    DataPubblicazione = post.DataPubblicazione
                };
                return postDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteBlogPostAsync(int id)
        {
            try
            {
                var existingPost = await _context.BlogPosts.FirstOrDefaultAsync(b => b.Id == id);
                if (existingPost == null) return false;

                _context.BlogPosts.Remove(existingPost);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateBlogPostAsync(int id, UpdateBlogPostDto dto)
        {
            try
            {
                var existingPost = await _context.BlogPosts.FirstOrDefaultAsync(b => b.Id == id);
                if (existingPost == null) return false;

                existingPost.Titolo = dto.Titolo;
                existingPost.Contenuto = dto.Contenuto;
                existingPost.ImmagineCopertina = dto.ImmagineCopertina;
                existingPost.Categoria = dto.Categoria;
                // Non aggiorniamo DataPubblicazione se non serve

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
