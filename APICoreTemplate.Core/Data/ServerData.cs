using APICoreTemplate.Core.Data.Repository;
using APICoreTemplate.Core.Data.Repository.Interfaces;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace APICoreTemplate.Core.Data
{
    public class ServerData : IServerData
    {
        private readonly SqlConnection _connection;
        private readonly ITransactionScope _scope;

        public IUserRepository Users { get; }

        public ServerData(SqlConnection sqlConnection, ITransactionScope scope)
        {
            _connection = sqlConnection;
            _scope = scope;

            Users = new UserRepository(sqlConnection);
        }

        public async Task AssertConnected()
        {
            await _connection.ExecuteAsync(new CommandDefinition(
                "SELECT 1",
                cancellationToken: default
            ));
        }

        public void Commit()
        {
            _scope.Complete();
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
            _scope.Dispose();
        }
    }
}
