using APICoreTemplate.Core.Data.Models;
using APICoreTemplate.Core.Data.Models.History;
using System.Threading;
using System.Threading.Tasks;

namespace APICoreTemplate.Core.Data.Repository.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>, IHistoryRepositoryBase<UserHistory>
    {
        Task<int> CreateUserAsync(string userName, string userEmail, string firstName, string lastName, string roles, CancellationToken cancel);
        Task UpdateUserAsync(int id, string roles, CancellationToken cancel);
    }
}
