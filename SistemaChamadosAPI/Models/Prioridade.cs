namespace SistemaChamadosAPI.Models
{
    public class Prioridade
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Nivel { get; set; } // 1..4
    }
}
