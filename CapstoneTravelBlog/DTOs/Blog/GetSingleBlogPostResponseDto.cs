namespace CapstoneTravelBlog.DTOs.Blog
{
    public class GetSingleBlogPostResponseDto
    {
        public string Message { get; set; } = "";
        public GetBlogPostDto? BlogPost { get; set; }
    }
}
