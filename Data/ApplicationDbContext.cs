using Microsoft.EntityFrameworkCore;
using apiPelis.Models;

namespace apiPelis.Data
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
    DataSeeder.SeedCategories(modelBuilder);
    DataSeeder.SeedUsers(modelBuilder);
    DataSeeder.SeedMovies(modelBuilder);

    // Relación PeliculaUsuario (muchos a muchos)
    modelBuilder.Entity<PeliculaUsuario>()
        .HasKey(pu => new { pu.id_pelicula, pu.id_usuario });

    modelBuilder.Entity<PeliculaUsuario>()
        .HasOne(pu => pu.Pelicula)
        .WithMany()
        .HasForeignKey(pu => pu.id_pelicula)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<PeliculaUsuario>()
        .HasOne(pu => pu.Usuario)
        .WithMany()
        .HasForeignKey(pu => pu.id_usuario)
        .OnDelete(DeleteBehavior.Cascade);

    // ✅ Relación Pelicula → Categoria
    modelBuilder.Entity<Pelicula>()
        .HasOne(p => p.Categoria)
        .WithMany()
        .HasForeignKey(p => p.id_categoria)
        .OnDelete(DeleteBehavior.Cascade); // o Cascade, según tu lógica
}

    }

}
