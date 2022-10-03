using System;

namespace APICoreTemplate.Core.Data
{
    public interface ITransactionScope : IDisposable
    {
        void Complete();
    }
}
