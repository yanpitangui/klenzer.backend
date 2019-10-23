using Microsoft.EntityFrameworkCore;
using Klenzer.Domain.Entities;
using System;

namespace Klenzer.Persistence.Configuration
{
    public class KlenzerDbContext : DbContext
    {
        public KlenzerDbContext(DbContextOptions<KlenzerDbContext> options)
           : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Servico> Servicos { get; set; }

        public DbSet<TipoServico> TipoServicos { get; set;
        }
        public DbSet<Agendamento> Agendamentos
        {
            get; set;
        }

        public DbSet<AgendamentoServico> AgendamentoServicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgendamentoServico>().HasKey(_as => new { _as.AgendamentoId, _as.ServicoId });
            modelBuilder.Entity<User>().HasData(
                new User { Id = new Guid("ebbed68e-6fc9-40b5-b9a3-e68ce97653b1"), CreationTime = DateTime.Now, Name = "Admin", Username = "admin", Password = "admin", Role = Role.Admin }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
