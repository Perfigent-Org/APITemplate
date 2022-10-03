using APICoreTemplate.Core.Helpers;
using APICoreTemplate.Core.Data.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using APICoreTemplate.Core.Data.Repository.Interfaces;
using APICoreTemplate.Core.Extensions;

namespace APICoreTemplate.Core.Data.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : IEntity
    {
        private readonly string _schema;
        private readonly string _entityTable;
        private readonly IDbConnection _connection;

        public RepositoryBase(IDbConnection sqlConnection)
        {
            _connection = sqlConnection;
            var table = ModelHelper.GetTableAttribute<T>();
            _schema = string.IsNullOrWhiteSpace(table.Schema) ? "" : $"{table.Schema}.";
            _entityTable = table.Name;
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel)
        {
            return await _connection.QueryAsync<T>($"SELECT * FROM {_schema}{_entityTable}", cancel);
        }

        public async Task<IEnumerable<T>> GetAllAsync(int[] ids, CancellationToken cancel)
        {
            return await _connection.QueryAsync<T>($"SELECT * FROM {_schema}{_entityTable} Where Id In @Ids", new
            {
                Ids = ids
            }, cancel);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>> property, object value, CancellationToken cancel)
        {
            var safeColumn = property.GetName();
            return await _connection.QueryAsync<T>($"SELECT * FROM {_schema}{_entityTable} WHERE {safeColumn} = @Value", new
            {
                Value = value
            }, cancel);
        }

        public async Task<IEnumerable<T>> GetAllByOffsetAsync(int pageNumber, int pageSize, CancellationToken cancel)
        {
            return await _connection.QueryAsync<T>(
                $"SELECT * FROM {_schema}{_entityTable} ORDER BY Id OFFSET(@PageNumber - 1) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY;", new
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }, cancel
            );
        }

        public async Task<IEnumerable<T>> GetAllByOffsetAsync(string columnName, string value, int pageNumber, int pageSize, CancellationToken cancel)
        {
            var safeColumn = ModelHelper.GetSafeColumnName<T>(columnName);
            return await _connection.QueryAsync<T>(
                $"SELECT * FROM {_schema}{_entityTable} Where {safeColumn} like @Search ORDER BY Id OFFSET(@PageNumber - 1) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY;", new
                {
                    Search = $"%{value}%",
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }, cancel
            );
        }

        public async Task<IEnumerable<T>> SearchAsync(string columnName, string value, CancellationToken cancel)
        {
            var safeColumn = ModelHelper.GetSafeColumnName<T>(columnName);
            return await _connection.QueryAsync<T>(
                $"SELECT * FROM {_schema}{_entityTable} Where {safeColumn} like @Search ORDER BY Id", new
                {
                    Search = $"%{value}%",
                }, cancel
            );
        }

        public async Task<T> GetAsync(int id, CancellationToken cancel)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<T>(
                $"SELECT * FROM {_schema}{_entityTable} WHERE Id = @Id", new
                {
                    Id = id
                }, cancel
            );

            return result;
        }

        public async Task<T> GetAsync(Expression<Func<T, object>> property, object value, CancellationToken cancel)
        {
            var safeColumn = property.GetName();
            return await _connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {_schema}{_entityTable} WHERE {safeColumn} = @Value", new
            {
                Value = value
            }, cancel);
        }

        public async Task<int> GetTotalCountAsync(CancellationToken cancel)
        {
            return await _connection.QuerySingleOrDefaultAsync<int>($"SELECT COUNT(Id) FROM {_schema}{_entityTable}", null, cancel);
        }

        public async Task<int> GetTotalCountAsync(string columnName, string search, CancellationToken cancel)
        {
            var safeColumn = ModelHelper.GetSafeColumnName<T>(columnName);
            return await _connection.QuerySingleOrDefaultAsync<int>($"SELECT COUNT(Id) FROM {_schema}{_entityTable} Where {safeColumn} like @Search", new
            {
                Search = $"%{search}%"
            }, cancel);
        }

        public async Task<IEnumerable<IdName>> GetNamesWithIdAsync(CancellationToken cancel)
        {
            return await _connection.QueryAsync<IdName>($"SELECT Id, Name FROM {_schema}{_entityTable}", null, cancel);
        }

        public async Task DeleteAsync(int id, CancellationToken cancel)
        {
            await _connection.ExecuteAsync(
                $"DELETE FROM {_schema}{_entityTable} WHERE Id = @Id",
                new
                {
                    Id = id
                },
                cancel
            );
        }

        public async Task DeleteAsync(Expression<Func<T, object>> property, object value, CancellationToken cancel)
        {
            var safeColumn = property.GetName();
            await _connection.ExecuteAsync(
                $"DELETE FROM {_schema}{_entityTable} WHERE {safeColumn} = @Value",
                new
                {
                    Value = value
                },
                cancel
            );
        }
    }
}
