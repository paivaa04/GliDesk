using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaChamadosAPI.Data;
using SistemaChamadosAPI.DTO;
using SistemaChamadosAPI.Models;

namespace SistemaChamadosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadosController : ControllerBase
    {
        private readonly AppDbContext _ctx;
        public ChamadosController(AppDbContext ctx) { _ctx = ctx; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _ctx.Chamados
                .Include(c => c.Prioridade)
                .Include(c => c.Status)
                .Include(c => c.Usuario)
                .Include(c => c.Setor)
                .ToListAsync();

            var resp = list.Select(c => new ChamadoResponseDTO
            {
                Id = c.Id,
                UsuarioId = c.UsuarioId,
                SetorId = c.SetorId,
                CategoriaId = c.CategoriaId,
                Titulo = c.Titulo,
                Descricao = c.Descricao,
                PrioridadeId = c.PrioridadeId,
                StatusId = c.StatusId,
                DataAbertura = c.DataAbertura,
                DataFechamento = c.DataFechamento
            });

            return Ok(resp);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _ctx.Chamados.FindAsync(id);
            if (c == null) return NotFound();
            var dto = new ChamadoResponseDTO
            {
                Id = c.Id,
                UsuarioId = c.UsuarioId,
                SetorId = c.SetorId,
                CategoriaId = c.CategoriaId,
                Titulo = c.Titulo,
                Descricao = c.Descricao,
                PrioridadeId = c.PrioridadeId,
                StatusId = c.StatusId,
                DataAbertura = c.DataAbertura,
                DataFechamento = c.DataFechamento
            };
            return Ok(dto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChamadoCriacaoDTO dto)
        {
            var usuario = await _ctx.Usuarios.FindAsync(dto.UsuarioId);
            if (usuario == null) return BadRequest("Usuário inválido");

            var chamado = new Chamado
            {
                UsuarioId = dto.UsuarioId,
                SetorId = dto.SetorId,
                CategoriaId = dto.CategoriaId,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                PrioridadeId = dto.PrioridadeId,
                StatusId = 1 // Em Aberto
            };

            _ctx.Chamados.Add(chamado);
            await _ctx.SaveChangesAsync();

            // adicionar histórico inicial
            var hist = new ChamadoHistorico
            {
                ChamadoId = chamado.Id,
                UsuarioId = dto.UsuarioId,
                OldStatusId = null,
                NewStatusId = chamado.StatusId,
                Comentario = "Abertura do chamado"
            };
            _ctx.ChamadoHistoricos.Add(hist);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = chamado.Id }, new { chamado.Id });
        }

        [Authorize]
        [HttpPut("{id:int}/status/{statusId:int}")]
        public async Task<IActionResult> UpdateStatus(int id, int statusId, [FromBody] string comentario)
        {
            var chamado = await _ctx.Chamados.FindAsync(id);
            if (chamado == null) return NotFound();

            var old = chamado.StatusId;
            chamado.StatusId = statusId;
            if (statusId == 4 || statusId == 5) chamado.DataFechamento = DateTime.UtcNow;
            chamado.UpdatedAt = DateTime.UtcNow;

            _ctx.Chamados.Update(chamado);

            var userId = int.TryParse(User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out var u) ? u : (int?)null;

            var hist = new ChamadoHistorico
            {
                ChamadoId = chamado.Id,
                UsuarioId = userId,
                OldStatusId = old,
                NewStatusId = statusId,
                Comentario = comentario
            };

            _ctx.ChamadoHistoricos.Add(hist);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var chamado = await _ctx.Chamados.FindAsync(id);
            if (chamado == null) return NotFound();
            chamado.IsDeleted = true;
            chamado.UpdatedAt = DateTime.UtcNow;
            _ctx.Chamados.Update(chamado);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
