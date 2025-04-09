namespace CapstoneTravelBlog.DTOs.Viaggio
{
    public class GetSingleViaggioResponseDto
    {
        public string Message { get; set; } = "";
        public GetViaggioDto? Viaggio { get; set; }
    }
}
