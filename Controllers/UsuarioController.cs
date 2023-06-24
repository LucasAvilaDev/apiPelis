using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiMovies.Data;
using apiMovies.Models;
using Newtonsoft.Json.Linq;


namespace Api.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/usuario/login
        [HttpPost("login")]
        public Usuario Login(LoginRequest data)
        {
            var correoElectronico = data.correo_electronico;
            var contrasena = data.password;
            var usuario = _context.Usuario.FirstOrDefault(u => u.correo_electronico == correoElectronico);

            if (usuario != null)
            {
                if (usuario.password == contrasena)
                {
                    return usuario;
                }
            }
            return null;
        }

        // POST: api/usuario/registro

        [HttpPost("registro")]
        public async Task<ActionResult<Usuario>> PostUser(Usuario usuario)
        {
            if (_context.Usuario == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Usuario'  is null.");
            }
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = usuario.id_usuario }, usuario);
        }


        // GET: api/usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUser(int id)
        {
            if (_context.Usuario == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/usuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, Usuario usuario)
        {
            if (id != usuario.id_usuario)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Usuario == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Agregando linea

        private bool UserExists(int id)
        {
            return (_context.Usuario?.Any(e => e.id_usuario == id)).GetValueOrDefault();
        }


        // POST: api/usuario/favorita
        [HttpPost("favorita")]
        public ActionResult AgregarFavorita(PeliculaUsuario peliculaUsuario)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.id_usuario == peliculaUsuario.id_usuario);
            var pelicula = _context.Pelicula.FirstOrDefault(m => m.id_pelicula == peliculaUsuario.id_pelicula);

            if (usuario == null || pelicula == null)
            {
                return NotFound();
            }

            // Verificar si la relación ya existe
            var existente = _context.PeliculaUsuario
                .FirstOrDefault(pu => pu.id_usuario == peliculaUsuario.id_usuario && pu.id_pelicula == peliculaUsuario.id_pelicula);

            if (existente != null)
            {
                return Conflict(); // Relación ya existente, retornar código de respuesta 409 Conflict
            }

            _context.PeliculaUsuario.Add(peliculaUsuario);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/usuario/favorita

        [HttpDelete("favorita")]
        public ActionResult EliminarFavorita(int idPelicula, int idUsuario)
        {
            var peliculaUsuario = _context.PeliculaUsuario
                .FirstOrDefault(pu => pu.id_pelicula == idPelicula && pu.id_usuario == idUsuario);

            if (peliculaUsuario == null)
            {
                return NotFound();
            }

            _context.PeliculaUsuario.Remove(peliculaUsuario);
            _context.SaveChanges();

            return Ok();
        }

        // GET: api/usuario/favorita

        [HttpGet("favorita")]
        public ActionResult<List<PeliculaUsuario>> ObtenerFavoritas(int idUsuario)
        {
            var peliculasFavoritas = _context.PeliculaUsuario
                .Where(pu => pu.id_usuario == idUsuario)
                .ToList();

            if (peliculasFavoritas.Count == 0)
            {
                return NotFound();
            }

            return peliculasFavoritas;
        }

    }

    public class LoginRequest
    {
        public string correo_electronico { get; set; }
        public string password { get; set; }
    }
}
