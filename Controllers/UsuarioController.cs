using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiPelis.Data;
using apiPelis.Models;
using apiPelis.DTOs;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;


namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/usuario")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
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
        public async Task<ActionResult> AgregarFavorita(FavoritaDto favoritaDto)
        {
            // Validar los datos
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Verificar si la película ya está marcada como favorita por el usuario
                bool favoritaExistente = await _context.PeliculaUsuario
                    .AnyAsync(pu => pu.id_pelicula == favoritaDto.id_pelicula && pu.id_usuario == favoritaDto.id_usuario);

                if (favoritaExistente)
                {
                    return Conflict("La película ya está marcada como favorita por el usuario.");
                }

                // Crear una nueva instancia de PeliculaUsuario con los datos del DTO
                PeliculaUsuario peliculaUsuario = new PeliculaUsuario
                {
                    id_pelicula = favoritaDto.id_pelicula,
                    id_usuario = favoritaDto.id_usuario
                };

                // Agregar la película favorita a la base de datos
                _context.PeliculaUsuario.Add(peliculaUsuario);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return StatusCode(500, "Error al agregar la película favorita.");
            }
        }

        // DELETE: api/usuario/favorita
        [HttpDelete("favorita")]
        public async Task<ActionResult> EliminarFavorita(FavoritaDto favoritaDto)
        {
            // Validar los datos
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Buscar la película favorita en la base de datos
                PeliculaUsuario peliculaUsuario = await _context.PeliculaUsuario.FindAsync(favoritaDto.id_pelicula, favoritaDto.id_usuario);

                if (peliculaUsuario == null)
                {
                    return NotFound("La película favorita no existe.");
                }

                // Eliminar la película favorita de la base de datos
                _context.PeliculaUsuario.Remove(peliculaUsuario);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return StatusCode(500, "Error al eliminar la película favorita.");
            }
        }

        // GET: api/usuario/{id}/favorita
         [HttpGet("{id}/favorita")]
        public async Task<ActionResult<List<Pelicula>>> GetFavoritas(int id)
        {
            try
            {
                // Obtener las películas favoritas del usuario de la base de datos
                List<Pelicula> favoritas = await _context.PeliculaUsuario
                    .Where(pu => pu.id_usuario == id)
                    .Select(pu => pu.Pelicula)
                    .ToListAsync();

                return Ok(favoritas);
            }
            catch
            {
                return StatusCode(500, "Error al obtener las películas favoritas.");
            }
        }



    }


}
