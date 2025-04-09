namespace CapstoneTravelBlog.DTOs.Blog
{
    public class AddBlogPostResponseDto
    {
        public string Message { get; set; } = "";
        public BlogPost Details { get; internal set; }
    }
}
