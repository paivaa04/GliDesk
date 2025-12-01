using Microsoft.EntityFrameworkCore;
using GlideskAPI.Models;

namespace GlideskAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Prioridade> Prioridades { get; set; }
        public DbSet<StatusChamado> StatusChamados { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ChamadoHistorico> ChamadoHistoricos { get; set; }
        public DbSet<Anexo> Anexos { get; set; }
        public DbSet<TokensRevogados> TokensRevogados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<StatusChamado>().ToTable("StatusChamado");   // :contentReference[oaicite:0]{index=0}
            modelBuilder.Entity<Setor>().ToTable("Setores");
            modelBuilder.Entity<Prioridade>().ToTable("Prioridades");         // :contentReference[oaicite:1]{index=1}
            modelBuilder.Entity<Categoria>().ToTable("Categorias");           // :contentReference[oaicite:2]{index=2}
            modelBuilder.Entity<ChamadoHistorico>().ToTable("ChamadoHistorico");


            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.UserCode)
                .IsUnique();

            modelBuilder.Entity<Categoria>()
                .HasIndex(c => new { c.SetorId, c.Nome })
                .IsUnique();

            modelBuilder.Entity<Chamado>()
                .HasQueryFilter(c => !c.IsDeleted);

          
            modelBuilder.Entity<Prioridade>().HasData(
                new Prioridade { Id = 1, Nome = "Baixa", Nivel = 1 },
                new Prioridade { Id = 2, Nome = "Média", Nivel = 2 },
                new Prioridade { Id = 3, Nome = "Alta", Nivel = 3 },
                new Prioridade { Id = 4, Nome = "Crítica", Nivel = 4 }
            );

            modelBuilder.Entity<StatusChamado>().HasData(
                new StatusChamado { Id = 1, Nome = "Em Aberto", Ordem = 1 },
                new StatusChamado { Id = 2, Nome = "Em Atendimento", Ordem = 2 },
                new StatusChamado { Id = 3, Nome = "Aguardando Cliente", Ordem = 3 },
                new StatusChamado { Id = 4, Nome = "Resolvido", Ordem = 4 },
                new StatusChamado { Id = 5, Nome = "Fechado", Ordem = 5 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
