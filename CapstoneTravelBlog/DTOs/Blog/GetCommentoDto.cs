namespace CapstoneTravelBlog.DTOs.Blog
{
    public class GetCommentoDto
    {
        public int Id { get; set; }
        public string Testo { get; set; } = "";
        public DateTime DataCreazione { get; set; }
        public string UtenteId { get; set; } = "";
        public string UtenteNome { get; set; } = "";
        public string? AvatarUrl { get; set; }
    }

}
