using Klenzer.Domain.Entities;
using Klenzer.Domain.Repositories;
using Klenzer.Persistence.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Klenzer.Persistence.Repositories
{
    public class TipoServicoRepository : GenericRepository<TipoServico>, ITipoServicoRepository
    {
        public TipoServicoRepository(KlenzerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
