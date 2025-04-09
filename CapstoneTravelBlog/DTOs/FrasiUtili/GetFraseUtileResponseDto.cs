namespace CapstoneTravelBlog.DTOs.FrasiUtili
{
    public class GetFraseUtileResponseDto
    {
        public string Message { get; set; } = "";

        public List<GetFraseUtileDto>? Frasi { get; set; }
    }
}
