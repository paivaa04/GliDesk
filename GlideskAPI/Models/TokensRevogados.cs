using System;

namespace GlideskAPI.Models
{
    public class TokensRevogados
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        public string TokenId { get; set; } = string.Empty;

        public DateTime RevokedAt { get; set; } = DateTime.UtcNow;
    }
}
