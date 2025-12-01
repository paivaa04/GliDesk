namespace GlideskAPI.DTO
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public int UsuarioId { get; set; }
    }
}
