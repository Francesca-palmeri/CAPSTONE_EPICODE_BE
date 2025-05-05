namespace CapstoneTravelBlog.DTOs.Prenotazioni
{
    public class GetPrenotazioneDto
    {
        public int Id { get; set; }
        public DateTime DataPrenotazione { get; set; }

        public string UtenteId { get; set; } = "";
        public string? NomeUtente { get; set; }    
        public string? CognomeUtente { get; set; }

        public string? EmailUtente { get; set; }

        public int NumeroPartecipanti { get; set; }
        public string Tipologia { get; set; }
        public string? Note { get; set; }

        public int? ViaggioId { get; set; }
        public string? TitoloViaggio { get; set; }

        public string? DescrizionePersonalizzata { get; set; }


    }
}
