using System.Collections.Generic;

namespace SistemaChamadosAPI.Models
{
    public class Setor
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();
    }
}
