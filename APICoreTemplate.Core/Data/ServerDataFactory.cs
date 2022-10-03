using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace APICoreTemplate.Core.Data
{
    public class ServerDataFactory : IServerDataFactory
    {
        private readonly string _connectionString;

        public ServerDataFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IServerData> Create(CancellationToken cancel)
        {
            var scope = new AsyncTransactionScopeWrapper();

            var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync(cancel);

            return new ServerData(connection, scope);
        }
    }
}
