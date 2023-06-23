using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiMovies.Models
{
public class PeliculaUsuario
{
    public int PeliculaId { get; set; }

    public Pelicula Pelicula { get; set; }

    public int UsuarioId { get; set; }

    public Usuario Usuario { get; set; }
}

}
