using System.ComponentModel.DataAnnotations;

public class FraseUtile
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Categoria")]
    public string Categoria { get; set; } = "";

    [Required]
    [Display(Name = "Frase in italiano")]
    public string Italiano { get; set; } = "";

    [Required]
    [Display(Name = "Giapponese (kana)")]
    public string GiapponeseKana { get; set; } = "";

    [Required]
    [Display(Name = "Romaji")]
    public string Romaji { get; set; } = "";

    [Url]
    [Display(Name = "Audio")]
    public string? AudioUrl { get; set; }
}

