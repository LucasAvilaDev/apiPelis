using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using apiPelis.DTOs;
using apiPelis.Models;
using apiPelis.Data;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest data)
    {
        var usuario = _context.Usuario.FirstOrDefault(u => u.correo_electronico == data.correo_electronico);

        if (usuario == null || usuario.password != data.password)
        {
            return Unauthorized("Credenciales inválidas");
        }

        var token = GenerarJwt(usuario);

        return Ok(new
        {
            token,
            usuario.id_usuario,
            usuario.nombre,
            usuario.tipo
        });
    }

    private string GenerarJwt(Usuario usuario)
    {
        var jwtSettings = _configuration.GetSection("Jwt");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.id_usuario.ToString()),
            new Claim(ClaimTypes.Email, usuario.correo_electronico),
            new Claim(ClaimTypes.Role, usuario.tipo)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    // POST: api/auth/registro
    [HttpPost("registro")]
    public async Task<ActionResult<Usuario>> Register([FromBody] Usuario usuario)
    {
        _context.Usuario.Add(usuario);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Register), new { id = usuario.id_usuario }, usuario);
    }
}
