using Klenzer.Domain.Entities;
using Klenzer.Domain.Repositories;
using Klenzer.Persistence.Configuration;

namespace Klenzer.Persistence.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(KlenzerDbContext dbContext) : base(dbContext) { }

    }
}
