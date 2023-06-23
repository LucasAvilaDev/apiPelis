using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiMovies.Models
{
    public class Usuario
{
    [Key]
    public int id_usuario { get; set; }

    public string nombre { get; set; }

    public string correo_electronico { get; set; }

    public string password { get; set; }
    
    public string tipo { get; set; }
    public string fotoUsuario { get; set; }


}

}
