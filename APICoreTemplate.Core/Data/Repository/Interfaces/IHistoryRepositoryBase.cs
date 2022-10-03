using APICoreTemplate.Core.Data.Models.History;

namespace APICoreTemplate.Core.Data.Repository.Interfaces
{
    public interface IHistoryRepositoryBase<T> where T : IHistoryEntity
    {
        IHistoryRepository<T> History { get; }
    }
}
