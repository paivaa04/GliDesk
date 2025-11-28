namespace SistemaChamadosAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public int SetorId { get; set; }
        public Setor Setor { get; set; } = null!;
    }
}
