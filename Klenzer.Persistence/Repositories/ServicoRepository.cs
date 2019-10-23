using Klenzer.Domain.Entities;
using Klenzer.Domain.Repositories;
using Klenzer.Persistence.Configuration;

namespace Klenzer.Persistence.Repositories
{
    public class ServicoRepository : GenericRepository<Servico>, IServicoRepository
    {
        public ServicoRepository(KlenzerDbContext dbContext) : base(dbContext) { }
    }
}
