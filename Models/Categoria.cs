using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiPelis.Models
{
public class Categoria
{
    [Key]
    public int id_categoria { get; set; }
    public string nombre { get; set; }
}
}
