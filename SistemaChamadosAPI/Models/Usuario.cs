using System;
using System.Collections.Generic;

namespace SistemaChamadosAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string UserCode { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        public int TipoUsuario { get; set; } // 1=User,2=Atendente,3=Supervisor,4=Admin
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Chamado> Chamados { get; set; } = new List<Chamado>();
    }
}
