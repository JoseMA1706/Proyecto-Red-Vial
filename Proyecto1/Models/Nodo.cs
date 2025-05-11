using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Nodo
{
    [Key]
    [Required]
    public string Id { get; set; } = "";

    public int VehiculosEnEspera { get; set; }

    [Required]
    public string EstadoSemaforo { get; set; } = "Rojo";

    public int TiempoPromedioCruce { get; set; }

    public string TipoViaNorte { get; set; } = "Ninguna";
    public string TipoViaSur { get; set; } = "Ninguna";
    public string TipoViaEste { get; set; } = "Ninguna";
    public string TipoViaOeste { get; set; } = "Ninguna";

   
    public string? IdNorte { get; set; }
    public string? IdSur { get; set; }
    public string? IdEste { get; set; }
    public string? IdOeste { get; set; }

  
    [NotMapped] public Nodo? Norte { get; set; }
    [NotMapped] public Nodo? Sur { get; set; }
    [NotMapped] public Nodo? Este { get; set; }
    [NotMapped] public Nodo? Oeste { get; set; }
}
