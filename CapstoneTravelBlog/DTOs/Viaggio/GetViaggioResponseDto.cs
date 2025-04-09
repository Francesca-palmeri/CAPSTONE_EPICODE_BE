namespace CapstoneTravelBlog.DTOs.Viaggio
{
    public class GetViaggioResponseDto
    {
        public string Message { get; set; } = "";
        public List<GetViaggioDto>? Viaggi { get; set; }
    }
}
