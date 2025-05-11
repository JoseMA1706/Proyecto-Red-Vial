using System.ComponentModel.DataAnnotations;

namespace Proyecto1.Models
{
    public class CuelloBotella
    {
        [Key]
        public int Id { get; set; }
        public string NodoId { get; set; } = "";
        public int VehiculosEnEspera { get; set; }
        public string EstadoSemaforo { get; set; } = "";
        public int TiempoPromedioCruce { get; set; }
    }
}
