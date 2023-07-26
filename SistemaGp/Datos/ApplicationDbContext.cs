

using SistemaGp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SistemaGp.Datos
{
    //con esto Identity decimos que ahora puede tener un login
    //cuando tiene solo DbContext podemos ver todo lo del sistema 
    public class ApplicationDbContext : IdentityDbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<TipoAplicacion> TipoAplicacion { get; set; }

        public DbSet<Producto> Producto { get; set; }

        public DbSet<UsuarioAplicacion> UsuarioAplicacion { get; set; }

    }
}
