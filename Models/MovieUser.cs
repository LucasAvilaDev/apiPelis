using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiMovies.Models
{
    public class PeliculaUsuario
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("MovieId")]
        public int MovieId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public Movie Movie { get; set; }

        public User User { get; set; }
    }
}
