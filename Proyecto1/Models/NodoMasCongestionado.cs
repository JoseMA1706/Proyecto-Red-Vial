using System.ComponentModel.DataAnnotations;

namespace Proyecto1.Models
{
    public class NodoMasCongestionado
    {
        [Key]
        public int Id { get; set; }
        public string NodoId { get; set; } = "";
        public int VehiculosEnEspera { get; set; }
    }
}
