using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SistemaChamadosAPI.Data;
using SistemaChamadosAPI.DTO;
using SistemaChamadosAPI.Models;

namespace SistemaChamadosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _ctx;
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public AuthController(AppDbContext ctx, IConfiguration config)
        {
            _ctx = ctx;
            _config = config;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UsuarioCadastroDTO dto)
        {
            if (_ctx.Usuarios.Any(u => u.Email == dto.Email)) return BadRequest("Email já cadastrado");
            if (_ctx.Usuarios.Any(u => u.UserCode == dto.UserCode)) return BadRequest("UserCode já existe");

            var user = new Usuario
            {
                Nome = dto.Nome,
                UserCode = dto.UserCode,
                CPF = dto.CPF,
                Email = dto.Email,
                Telefone = dto.Telefone,
                TipoUsuario = 1 // padrão
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Senha);

            _ctx.Usuarios.Add(user);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(Register), new { id = user.Id }, new { user.Id, user.Email, user.Nome });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLoginDTO dto)
        {
            var user = _ctx.Usuarios.SingleOrDefault(u => u.Email == dto.Email);
            if (user == null) return Unauthorized("Credenciais inválidas");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Senha);
            if (result == PasswordVerificationResult.Failed) return Unauthorized("Credenciais inválidas");

            var token = GenerateJwtToken(user);

            var resp = new AuthResponseDTO
            {
                Token = token,
                ExpiresIn = 3600,
                Email = user.Email,
                Nome = user.Nome,
                UsuarioId = user.Id
            };

            return Ok(resp);
        }

        private string GenerateJwtToken(Usuario user)
        {
            var jwt = _config.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwt.GetValue<string>("Secret"));
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim("usercode", user.UserCode ?? string.Empty),
                new Claim("tipo", user.TipoUsuario.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = jwt.GetValue<string>("Issuer"),
                Audience = jwt.GetValue<string>("Audience"),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
