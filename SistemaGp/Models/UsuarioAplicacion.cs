using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGp.Models
{
    public class UsuarioAplicacion : IdentityUser
    //IdentityUser con esto todos los campos que tiene la tabla
    //aspnet-user los podemos utilizar
    {
        public string NombreCompleto { get; set; }

        [NotMapped]
        public string Direccion { get; set; }

        [NotMapped]
        public string Ciudad { get; set; }
    }
}
