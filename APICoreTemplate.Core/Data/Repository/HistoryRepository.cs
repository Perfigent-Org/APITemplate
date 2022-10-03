using APICoreTemplate.Core.Data.Models.History;
using APICoreTemplate.Core.Data.Repository.Interfaces;
using APICoreTemplate.Core.Extensions;
using APICoreTemplate.Core.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace APICoreTemplate.Core.Data.Repository
{
    public class HistoryRepository<T> : IHistoryRepository<T> where T : IHistoryEntity
    {
        private readonly string _schema;
        private readonly string _entityTable;
        private readonly IDbConnection _connection;

        public HistoryRepository(IDbConnection sqlConnection)
        {
            _connection = sqlConnection;
            var table = ModelHelper.GetTableAttribute<T>();
            _schema = string.IsNullOrWhiteSpace(table.Schema) ? "" : $"{table.Schema}.";
            _entityTable = table.Name;
        }

        public async Task<IEnumerable<T>> GetAsync(int id, int pageNumber, int pageSize, CancellationToken cancel)
        {
            var result = await _connection.QueryAsync<T>(
                $@"SELECT * FROM {_schema}{_entityTable} 
                        Where Id = @id 
                        ORDER BY Id, SysEndTime OFFSET(@PageNumber - 1) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY;", new
                {
                    id = id,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }, cancel
            );

            return result;
        }

        public async Task<IEnumerable<T>> GetAsync(int id, string startTime, string endTime, CancellationToken cancel)
        {
            var result = await _connection.QueryAsync<T>(
                $@"SELECT *, SysStartTime, SysEndTime FROM {_entityTable} FOR SYSTEM_TIME FROM @StartTime TO @EndTime Where Id = @Id", new
                {
                    Id = id,
                    StartTime = startTime,
                    EndTime = endTime
                }, cancel
            );

            return result;
        }

        public async Task<IEnumerable<T>> GetAsync(int[] ids, string startTime, string endTime, CancellationToken cancel)
        {
            var result = await _connection.QueryAsync<T>($"SELECT *, SysStartTime, SysEndTime FROM {_entityTable} FOR SYSTEM_TIME FROM @StartTime TO @EndTime Where Id In @Ids", new
            {
                Ids = ids,
                StartTime = startTime,
                EndTime = endTime
            }, cancel);

            return result;
        }

        public async Task<int> GetTotalCountAsync(int id, CancellationToken cancel)
        {
            return await _connection.QuerySingleOrDefaultAsync<int>($"SELECT COUNT(Id) FROM {_schema}{_entityTable} Where Id = @id",
                new
                {
                    id = id,
                }, cancel);
        }
    }
}
