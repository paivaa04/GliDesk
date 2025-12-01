using System;

namespace GlideskAPI.Models
{
    public class ChamadoHistorico
    {
        public int Id { get; set; }

        public int ChamadoId { get; set; }
        public Chamado Chamado { get; set; } = null!;

        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int? OldStatusId { get; set; }
        public int? NewStatusId { get; set; }

        public string Comentario { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
