using System;

namespace SistemaChamadosAPI.Models
{
    public class Anexo
    {
        public int Id { get; set; }

        public int ChamadoId { get; set; }
        public Chamado Chamado { get; set; } = null!;

        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;

        public long? SizeBytes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
