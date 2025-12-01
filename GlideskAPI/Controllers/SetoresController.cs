using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GlideskAPI.Data;
using GlideskAPI.DTO;
using GlideskAPI.Models;

namespace GlideskAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetoresController : ControllerBase
    {
        private readonly AppDbContext _ctx;
        public SetoresController(AppDbContext ctx) { _ctx = ctx; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var setores = await _ctx.Setores
                    .AsNoTracking()
                    .ToListAsync();

                return Ok(setores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Erro interno ao consultar os setores.",
                    detail = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var setor = await _ctx.Setores
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (setor == null)
                    return NotFound(new { message = "Setor não encontrado." });

                return Ok(setor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Erro interno ao buscar o setor.",
                    detail = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SetorCriacaoDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Dados inválidos." });

                if (string.IsNullOrWhiteSpace(dto.Nome))
                    return BadRequest(new { message = "O nome do setor é obrigatório." });

                var exists = await _ctx.Setores.AnyAsync(s => s.Nome == dto.Nome);
                if (exists)
                    return Conflict(new { message = "Já existe um setor com esse nome." });

                var setor = new Setor
                {
                    Nome = dto.Nome.Trim(),
                    Descricao = dto.Descricao?.Trim()
                };

                _ctx.Setores.Add(setor);
                await _ctx.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = setor.Id }, setor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Erro interno ao criar o setor.",
                    detail = ex.Message
                });
            }
        }
    }
}
