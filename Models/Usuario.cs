using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiMovies.Models
{
    public class Usuario
{
    [Key]
    public int UsuarioId { get; set; }

    public string Nombre { get; set; }

    public string Email { get; set; }

    public string Contrasena { get; set; }
    
    public string Imagen { get; set; }


    public TipoUsuario Tipo { get; set; }
}

public enum TipoUsuario
{
    Cliente,
    Administrador
}

}
