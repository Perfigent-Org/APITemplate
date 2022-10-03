using System.Threading;
using System.Threading.Tasks;

namespace APICoreTemplate.Core.Data
{
    public interface IServerDataFactory
    {
        Task<IServerData> Create(CancellationToken cancel);
    }
}
