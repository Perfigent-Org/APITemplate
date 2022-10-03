using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using APICoreTemplate.Core.Extensions;
using Dapper;

namespace APICoreTemplate.Core.Extensions
{
    internal static class IDbConnectionExtensions
    {
        public static async Task ExecuteAsync(this IDbConnection connection, string query, object parameters, CancellationToken cancellationToken)
        {
            await connection.ExecuteAsync(new CommandDefinition(
                query,
                parameters,
                cancellationToken: cancellationToken
            ));
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection connection, string query, object parameters, CancellationToken cancellationToken)
        {
            return await connection.QueryAsync<T>(new CommandDefinition(
                query,
                parameters,
                cancellationToken: cancellationToken
            ));
        }

        public static async Task<T> QuerySingleAsync<T>(this IDbConnection connection, string query, object parameters, CancellationToken cancellationToken)
        {
            return await connection.QuerySingleAsync<T>(new CommandDefinition(
                query,
                parameters,
                cancellationToken: cancellationToken
            ));
        }

        public static async Task<T> QuerySingleOrDefaultAsync<T>(this IDbConnection connection, string query, object parameters, CancellationToken cancellationToken)
        {
            return await connection.QuerySingleOrDefaultAsync<T>(new CommandDefinition(
                query,
                parameters,
                cancellationToken: cancellationToken
            ));
        }
    }
}
