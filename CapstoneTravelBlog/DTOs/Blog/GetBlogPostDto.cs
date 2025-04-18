namespace CapstoneTravelBlog.DTOs.Blog
{
    public class GetBlogPostDto
    {
        public int Id { get; set; }
        public string Titolo { get; set; } = "";
        public string Contenuto { get; set; } = "";
        public string ImmagineCopertina { get; set; } = "";
        public string Categoria { get; set; } = "";
        public DateTime DataPubblicazione { get; set; }

        public List<GetCommentoDto> Commenti { get; set; } = new();

    }

}
