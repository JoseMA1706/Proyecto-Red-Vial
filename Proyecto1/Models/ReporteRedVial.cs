using System.ComponentModel.DataAnnotations;

namespace Proyecto1.Models
{
    public class ReporteRedVial
    {
        [Key]
        public int Id { get; set; }

        public string NodosTexto { get; set; } = "";
        public string InterseccionMasCongestionada { get; set; } = "";
        public string CuellosBotellaTexto { get; set; } = "";
        public DateTime FechaGeneracion { get; set; } = DateTime.Now;
    }
}
