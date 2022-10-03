using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APICoreTemplate.Core.Data.Repository.Interfaces
{
    public interface IHistoryRepository<T>
    {
        Task<IEnumerable<T>> GetAsync(int id, int pageNumber, int pageSize, CancellationToken cancel);
        Task<IEnumerable<T>> GetAsync(int id, string startTime, string endTime, CancellationToken cancel);
        Task<IEnumerable<T>> GetAsync(int[] ids, string startTime, string endTime, CancellationToken cancel);
        Task<int> GetTotalCountAsync(int id, CancellationToken cancel);
    }
}
