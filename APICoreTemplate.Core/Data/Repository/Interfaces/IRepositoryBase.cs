using APICoreTemplate.Core.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace APICoreTemplate.Core.Data.Repository.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel);
        Task<IEnumerable<T>> GetAllAsync(int[] ids, CancellationToken cancel);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>> property, object value, CancellationToken cancel);
        Task<IEnumerable<T>> GetAllByOffsetAsync(int pageNumber, int pageSize, CancellationToken cancel);
        Task<IEnumerable<T>> GetAllByOffsetAsync(string columnName, string value, int pageNumber, int pageSize, CancellationToken cancel);
        Task<IEnumerable<T>> SearchAsync(string columnName, string value, CancellationToken cancel);
        Task<T> GetAsync(int id, CancellationToken cancel);
        Task<T> GetAsync(Expression<Func<T, object>> property, object value, CancellationToken cancel);
        Task<int> GetTotalCountAsync(CancellationToken cancel);
        Task<int> GetTotalCountAsync(string columnName, string search, CancellationToken cancel);
        Task<IEnumerable<IdName>> GetNamesWithIdAsync(CancellationToken cancel);
        Task DeleteAsync(int id, CancellationToken cancel);
        Task DeleteAsync(Expression<Func<T, object>> property, object value, CancellationToken cancel);
    }
}
