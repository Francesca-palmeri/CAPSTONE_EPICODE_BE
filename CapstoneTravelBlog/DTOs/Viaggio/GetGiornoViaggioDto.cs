namespace CapstoneTravelBlog.DTOs.Viaggio
{
    public class GetGiornoViaggioDto
    {
        public int Id { get; set; }
        public int GiornoNumero { get; set; }
        public string Titolo { get; set; } = "";
        public string Descrizione { get; set; } = "";
        public int ViaggioId { get; set; }
    }

}
