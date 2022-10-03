using APICoreTemplate.Core.Data.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace APICoreTemplate.Core.Data
{
    public interface IServerData : IDisposable
    {
        IUserRepository Users { get; }
        Task AssertConnected();
        void Commit();
    }
}
