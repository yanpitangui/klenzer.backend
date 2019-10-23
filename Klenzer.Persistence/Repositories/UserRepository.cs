using Klenzer.Domain.Entities;
using Klenzer.Domain.Repositories;
using Klenzer.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Klenzer.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(KlenzerDbContext dbContext) : base(dbContext) { }

        public async Task<User> Authenticate(User user)
        {
            return await GetAll()
                .Where(x => x.Username == user.Username && x.Password == user.Password)
                .SingleOrDefaultAsync();
        }
    }
}
