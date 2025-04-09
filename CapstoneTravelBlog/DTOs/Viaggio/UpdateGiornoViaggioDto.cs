namespace CapstoneTravelBlog.DTOs.Viaggio
{
    public class UpdateGiornoViaggioDto
    {
        public int GiornoNumero { get; set; }
        public string Titolo { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public int ViaggioId { get; set; } //  modificabile
    }
}
