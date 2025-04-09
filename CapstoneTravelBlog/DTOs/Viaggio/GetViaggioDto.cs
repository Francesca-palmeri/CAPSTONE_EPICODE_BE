namespace CapstoneTravelBlog.DTOs.Viaggio
{
    public class GetViaggioDto
    {
        public int Id { get; set; }
        public string Titolo { get; set; } = "";
        public string ImmagineCopertina { get; set; } = "";
        public string Descrizione { get; set; } = "";
        public DateTime DataPartenza { get; set; }
        public DateTime DataRitorno { get; set; }
        public int DurataGiorni { get; set; }
        public decimal Prezzo { get; set; }
        public string Tipologia { get; set; } = "";

        public List<GetGiornoViaggioDto> Giorni { get; set; } = new();
    }

}
