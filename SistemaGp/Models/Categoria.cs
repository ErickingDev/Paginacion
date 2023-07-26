using System.ComponentModel.DataAnnotations;

namespace SistemaGp.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage ="El nombre es obligatorio")]

        public string NombreCategoria { get; set; }

        [Required(ErrorMessage = "Numero de orden es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage ="El numero debe ser mayor a 0")]
        public int MostrarOrden { get; set; }

    }
    
}
