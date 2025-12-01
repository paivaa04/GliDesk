using System;

namespace GlideskAPI.DTO
{
    public class ChamadoResponseDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int SetorId { get; set; }
        public int? CategoriaId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int PrioridadeId { get; set; }
        public int StatusId { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
    }
}
