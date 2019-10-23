using Klenzer.Domain.Entities;
using Klenzer.Domain.Repositories;
using Klenzer.Persistence.Configuration;

namespace Klenzer.Persistence.Repositories
{
    public class AgendamentoRepository : GenericRepository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(KlenzerDbContext dbContext) : base(dbContext) { }
    }
}
