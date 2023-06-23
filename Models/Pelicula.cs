using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiMovies.Models
{
public class Pelicula
{
    [Key]
    public int PeliculaId { get; set; }

    public string Titulo { get; set; }

    public string Descripcion { get; set; }

    public string Director { get; set; }

    public int Anio { get; set; }

    public int Duracion { get; set; }

    public string Imagen { get; set; }


    [ForeignKey("CategoriaId")]
    public int CategoriaId { get; set; }

    public Categoria Categoria { get; set; }
}

}
