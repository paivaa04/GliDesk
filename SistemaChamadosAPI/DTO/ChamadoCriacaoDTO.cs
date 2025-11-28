namespace SistemaChamadosAPI.DTO
{
    public class ChamadoCriacaoDTO
    {
        public int UsuarioId { get; set; }
        public int SetorId { get; set; }
        public int? CategoriaId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int PrioridadeId { get; set; }
    }
}
