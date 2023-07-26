using System.ComponentModel.DataAnnotations;

namespace SistemaGp.Models
{
    public class TipoAplicacion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre{ get; set; }


    }
    
}
