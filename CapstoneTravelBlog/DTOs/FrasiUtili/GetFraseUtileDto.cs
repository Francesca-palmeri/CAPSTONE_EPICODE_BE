namespace CapstoneTravelBlog.DTOs.FrasiUtili
{
    public class GetFraseUtileDto
    {
        public int Id { get; set; }
        public string Categoria { get; set; } = "";
        public string Italiano { get; set; } = "";
        public string GiapponeseKana { get; set; } = "";
        public string Romaji { get; set; } = "";
        public string? AudioUrl { get; set; }
    }
}
