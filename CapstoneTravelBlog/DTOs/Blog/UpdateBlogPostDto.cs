namespace CapstoneTravelBlog.DTOs.Blog
{
    public class UpdateBlogPostDto
    {
        public string Titolo { get; set; } = "";
        public string Contenuto { get; set; } = "";
        public string ImmagineCopertina { get; set; } = "";
        public string Categoria { get; set; } = "";
    }
}
