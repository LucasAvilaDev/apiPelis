using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using apiMovies.Models;

namespace apiMovies.Data
{
    public static class DataSeeder
    {
        public static void SeedCategories(ModelBuilder modelBuilder)
        {
            var categoriasJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "categorias.json");
            var categoriasJson = File.ReadAllText(categoriasJsonPath);
            var categorias = JsonSerializer.Deserialize<List<Categoria>>(categoriasJson);

            modelBuilder.Entity<Categoria>().HasData(categorias);
        }

    
        public static void SeedMovies(ModelBuilder modelBuilder)
        {
            var moviesJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "peliculas.json");
            var moviesJson = File.ReadAllText(moviesJsonPath);
            var movies = JsonSerializer.Deserialize<List<Pelicula>>(moviesJson);

            modelBuilder.Entity<Pelicula>().HasData(movies);
        }

        public static void SeedUsers(ModelBuilder modelBuilder)
        {
            var usuariosJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "usuarios.json");
            var usuariosJson = File.ReadAllText(usuariosJsonPath);
            var usuarios = JsonSerializer.Deserialize<List<Pelicula>>(usuariosJson);

            modelBuilder.Entity<Pelicula>().HasData(usuarios);
        }

    }
}
