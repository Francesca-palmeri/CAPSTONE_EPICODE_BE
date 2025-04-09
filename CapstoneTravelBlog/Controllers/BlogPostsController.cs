using CapstoneTravelBlog.Data;
using CapstoneTravelBlog.DTOs.Blog;
using CapstoneTravelBlog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneTravelBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BlogPostService> _logger;
        private readonly BlogPostService _blogService;

        public BlogPostsController(
            ApplicationDbContext context,
            ILogger<BlogPostService> logger,
            BlogPostService blogService)
        {
            _context = context;
            _logger = logger;
            _blogService = blogService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBlogPost([FromBody] AddBlogPostDto dto)
        {
            var newPost = await _blogService.CreateBlogPostAsync(dto);
            if (newPost == null)
            {
                return BadRequest(new AddBlogPostResponseDto
                {
                    Message = "Failed to create a new Blog Post object!"
                });
            }

            var result = await _blogService.AddBlogPostAsync(newPost);
            return result
                ? Ok(new AddBlogPostResponseDto
                {
                    Message = "Blog Post correctly added!",
                    Details = newPost
                })
                : BadRequest(new AddBlogPostResponseDto
                {
                    Message = "Something went wrong!"
                });
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogPosts()
        {
            try
            {
                var posts = await _blogService.GetBlogPostsAsync();
                if (posts == null)
                {
                    return BadRequest(new GetBlogPostResponseDto
                    {
                        Message = "Something went wrong",
                        BlogPosts = null
                    });
                }

                if (!posts.Any())
                {
                    return NotFound(new GetBlogPostResponseDto
                    {
                        Message = "No blog posts found!",
                        BlogPosts = null
                    });
                }

                var postsDto = await _blogService.GetBlogPostsDtoAsync(posts);
                return Ok(new GetBlogPostResponseDto
                {
                    Message = "Blog posts correctly retrieved!",
                    BlogPosts = postsDto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong retrieving blog posts. {Error}", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBlogPostById(int id)
        {
            var postDto = await _blogService.GetBlogPostDtoByIdAsync(id);
            if (postDto == null)
            {
                return BadRequest(new
                {
                    message = $"Blog post with ID {id} not found or something went wrong."
                });
            }

            return Ok(new GetSingleBlogPostResponseDto
            {
                Message = "Blog post correctly found!",
                BlogPost = postDto
            });
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBlogPost(int id, [FromBody] UpdateBlogPostDto dto)
        {
            var result = await _blogService.UpdateBlogPostAsync(id, dto);
            return result
                ? Ok(new { Message = "Blog post correctly updated!" })
                : BadRequest(new { Message = "Something went wrong!" });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var result = await _blogService.DeleteBlogPostAsync(id);
            return result
                ? Ok(new { message = "Blog post correctly removed!" })
                : BadRequest(new { message = "Something went wrong!" });
        }
    }
}
