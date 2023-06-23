using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiMovies.Data;
using apiMovies.Models;

namespace Api.Controllers
{
    [Route("api/pelicula")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PeliculaController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        // GET: api/pelicula
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pelicula>>> GetMovies()
        {
          if (_context.Pelicula == null)
          {
              return NotFound();
          }
            return await _context.Pelicula.ToListAsync();
        }

        // GET: api/pelicula/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pelicula>> GetMovie(int id)
        {
          if (_context.Pelicula == null)
          {
              return NotFound();
          }
            var pelicula = await _context.Pelicula.FindAsync(id);

            if (pelicula == null)
            {
                return NotFound();
            }

            return pelicula;
        }

        // PUT: api/pelicula/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Pelicula pelicula)
        {
            if (id != pelicula.id_pelicula)
            {
                return BadRequest();
            }

            _context.Entry(pelicula).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/pelicula
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pelicula>> PostMovie(Pelicula pelicula)
        {
          if (_context.Pelicula == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Pelicula'  is null.");
          }
            _context.Pelicula.Add(pelicula);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = pelicula.id_pelicula }, pelicula);
        }

        // DELETE: api/pelicula/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.Pelicula == null)
            {
                return NotFound();
            }
            var pelicula = await _context.Pelicula.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            _context.Pelicula.Remove(pelicula);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return (_context.Pelicula?.Any(e => e.id_pelicula == id)).GetValueOrDefault();
        }
    }
}
