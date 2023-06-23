using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiMovies.Models
{
public class PeliculaUsuario
{
    [Key]
    [Column(Order = 1)]
    public int id_pelicula { get; set; }

    [Key]
    [Column(Order = 2)]
    public int id_usuario { get; set; }
}
}
