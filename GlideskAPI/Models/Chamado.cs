using System;
using System.Collections.Generic;

namespace GlideskAPI.Models
{
    public class Chamado
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public int SetorId { get; set; }
        public Setor Setor { get; set; } = null!;

        public int? CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        public int PrioridadeId { get; set; }
        public Prioridade Prioridade { get; set; } = null!;

        public int StatusId { get; set; }
        public StatusChamado Status { get; set; } = null!;

        public DateTime DataAbertura { get; set; } = DateTime.UtcNow;
        public DateTime? DataFechamento { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<ChamadoHistorico> Historicos { get; set; } = new List<ChamadoHistorico>();
        public ICollection<Anexo> Anexos { get; set; } = new List<Anexo>();
    }
}
