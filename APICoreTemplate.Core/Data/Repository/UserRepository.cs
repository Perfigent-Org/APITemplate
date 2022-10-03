using APICoreTemplate.Core.Data.Models;
using APICoreTemplate.Core.Data.Models.History;
using APICoreTemplate.Core.Data.Repository.Interfaces;
using APICoreTemplate.Core.Extensions;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace APICoreTemplate.Core.Data.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly IDbConnection _connection;
        public IHistoryRepository<UserHistory> History { get; }

        public UserRepository(IDbConnection sqlConnection)
            : base(sqlConnection)
        {
            _connection = sqlConnection;
            History = new HistoryRepository<UserHistory>(sqlConnection);
        }

        public async Task<int> CreateUserAsync(string userName, string userEmail, string firstName, string lastName, string roles, CancellationToken cancel)
        {
            var uid = await _connection.QuerySingleAsync<int>(@"
                INSERT INTO Users (UserName, Email, FirstName,LastName,Roles,LastLoginDateTime) VALUES(@username, @email,@firstname,@lastname,@roles,getutcdate()); SELECT SCOPE_IDENTITY();", new
            {
                username = userName,
                email = userEmail,
                firstname = firstName,
                lastname = lastName,
                roles = roles
            }, cancel
            );
            return uid;
        }

        public async Task UpdateUserAsync(int id, string roles, CancellationToken cancel)
        {
            await _connection.ExecuteAsync(@"
                UPDATE Users SET Roles = @roles, LastLoginDateTime = getutcdate() WHERE Id = @id", new
            {
                id = id,
                roles = roles
            }, cancel);
        }
    }
}
