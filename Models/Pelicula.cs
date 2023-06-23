using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiMovies.Models
{
public class Pelicula
{
    [Key]
    public int id_pelicula { get; set; }

    public string titulo { get; set; }

    public string descripcion { get; set; }

    public string director { get; set; }

    public int year { get; set; }

    public int duracion { get; set; }

    public string fotoPelicula { get; set; }

    [ForeignKey("id_categoria")]
    public int id_categoria { get; set; }

    public Categoria Categoria { get; set; }
}

}
