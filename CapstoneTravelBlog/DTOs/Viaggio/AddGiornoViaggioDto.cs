namespace CapstoneTravelBlog.DTOs.Viaggio
{
    public class AddGiornoViaggioDto
    {
        public int GiornoNumero { get; set; }
        public string Titolo { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public int ViaggioId { get; set; }
    }
}
