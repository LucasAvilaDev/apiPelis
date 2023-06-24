using Microsoft.EntityFrameworkCore;
using apiMovies.Models;

namespace apiMovies.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Pelicula> Pelicula { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<PeliculaUsuario> PeliculaUsuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //DataSeeder.SeedCategories(modelBuilder);
            //DataSeeder.SeedUsers(modelBuilder);
            //DataSeeder.SeedMovies(modelBuilder);
            
            modelBuilder.Entity<PeliculaUsuario>()
            .HasKey(pu => new { pu.id_pelicula, pu.id_usuario });
        }
    }
}
