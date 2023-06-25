
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
            var peliculasJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "peliculas.json");
            var peliculasJson = File.ReadAllText(peliculasJsonPath);
            var peliculas = JsonSerializer.Deserialize<List<Pelicula>>(peliculasJson);

            modelBuilder.Entity<Pelicula>().HasData(peliculas);
        }
        
        public static void SeedUsers(ModelBuilder modelBuilder)
        {
            var usuariosJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "usuarios.json");
            var usuariosJson = File.ReadAllText(usuariosJsonPath);
            var usuarios = JsonSerializer.Deserialize<List<Usuario>>(usuariosJson);

            modelBuilder.Entity<Usuario>().HasData(usuarios);
        }

    }
}