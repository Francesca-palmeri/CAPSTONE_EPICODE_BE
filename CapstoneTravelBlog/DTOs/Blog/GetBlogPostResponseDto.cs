namespace CapstoneTravelBlog.DTOs.Blog
{
    public class GetBlogPostResponseDto
    {
        public string Message { get; set; } = "";
        public List<GetBlogPostDto>? BlogPosts { get; set; }
    }
}
