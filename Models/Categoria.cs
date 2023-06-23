using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiMovies.Models
{
public class Categoria
{
    [Key]
    public int id_categoria { get; set; }
    public string nombre { get; set; }
}
}
