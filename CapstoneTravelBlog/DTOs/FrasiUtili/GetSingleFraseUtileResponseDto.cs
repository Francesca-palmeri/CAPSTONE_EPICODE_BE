namespace CapstoneTravelBlog.DTOs.FrasiUtili
{
    public class GetSingleFraseUtileResponseDto
    {

        public string Message { get; set; } = "";

        public GetFraseUtileDto? Frase { get; set; }
    }
}
