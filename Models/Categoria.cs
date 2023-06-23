using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiMovies.Models
{
public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }

    public string Nombre { get; set; }

    public ICollection<Pelicula> Peliculas { get; set; }
}

}
