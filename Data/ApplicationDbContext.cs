using Microsoft.EntityFrameworkCore;
using apiMovies.Models;

namespace apiMovies.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieUser> MovieUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
        new User
        {
            UserId = 1,
            Name = "John Doe",
            Email = "john@example.com",
            Password = "password123",
            Type = UserType.Client
        },
        new User
        {
            UserId = 2,
            Name = "Admin User",
            Email = "admin@example.com",
            Password = "adminpass",
            Type = UserType.Administrator
        }
    );


            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Drama" },
                new Category { CategoryId = 2, Name = "Comedy" },
                new Category { CategoryId = 3, Name = "Action" }
            );

            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    MovieId = 1,
                    Title = "The Dark Knight",
                    Description = "Batman faces off against the Joker in Gotham City",
                    Director = "Christopher Nolan",
                    Year = 2008,
                    Duration = 152,
                    CategoryId = 1
                },
                new Movie
                {
                    MovieId = 2,
                    Title = "Forrest Gump",
                    Description = "A man with disabilities lives an extraordinary life",
                    Director = "Robert Zemeckis",
                    Year = 1994,
                    Duration = 142,
                    CategoryId = 3
                },
                new Movie
                {
                    MovieId = 3,
                    Title = "Pulp Fiction",
                    Description = "A story of crime and redemption in Los Angeles",
                    Director = "Quentin Tarantino",
                    Year = 1994,
                    Duration = 154,
                    CategoryId = 3
                },
                new Movie
                {
                    MovieId = 4,
                    Title = "Inception",
                    Description = "A team of thieves invades people's dreams",
                    Director = "Christopher Nolan",
                    Year = 2010,
                    Duration = 148,
                    CategoryId = 3
                },
                new Movie
                {
                    MovieId = 5,
                    Title = "The Shawshank Redemption",
                    Description = "An innocent man finds hope in prison",
                    Director = "Frank Darabont",
                    Year = 1994,
                    Duration = 142,
                    CategoryId = 3
                }
            );
        }


    }
}
