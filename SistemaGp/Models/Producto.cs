using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGp.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre del Producto es requerido")]
        public string NombreProducto { get; set; }
        [Required(ErrorMessage = "Descripcion Corta es Requerida")]
        public string DescripcionCorta {get; set; }
        [Required(ErrorMessage = "Descripcion del Producto es requerida")]
        public string DescripcionProducto { get; set; }

        [Required(ErrorMessage = "El Precio del Producto es Requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "El Precio debe de ser Mayor a cero")]
        public double Precio { get; set; }

        public string? ImagenUrl { get; set; }

        //llave foranea. para categoria en producto

        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria? Categoria { get; set; }

        //llave foranea. para Tipo Aplicacion en producto

        public int TipoAplicacionId { get; set; }

        [ForeignKey("TipoAplicacionId")]
        public virtual TipoAplicacion? TipoAplicacion { get; set; }

    }
}
