using Klenzer.Domain.Entities;
using System.Threading.Tasks;

namespace Klenzer.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> Authenticate(User user);
    }
}
