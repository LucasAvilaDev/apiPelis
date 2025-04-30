using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiPelis.Models
{
public class PeliculaUsuario
{
    [Key]
    [Column(Order = 1)]
    [ForeignKey("Pelicula")]
    public int id_pelicula { get; set; }

    [Key]
    [Column(Order = 2)]
    [ForeignKey("Usuario")]
    public int id_usuario { get; set; }

    public Pelicula Pelicula { get; set; }
    public Usuario Usuario { get; set; }
}
}
