using Microsoft.EntityFrameworkCore;
using apiMovies.Models;

namespace apiMovies.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PeliculaUsuario> PeliculaUsuario { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DataSeeder.SeedCategories(modelBuilder);
            DataSeeder.SeedMovies(modelBuilder);
            DataSeeder.SeedUsers(modelBuilder);

        }


    }
}
