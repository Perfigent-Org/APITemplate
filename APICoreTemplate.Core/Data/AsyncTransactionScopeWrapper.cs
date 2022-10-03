using System.Transactions;

namespace APICoreTemplate.Core.Data
{
    internal class AsyncTransactionScopeWrapper : ITransactionScope
    {
        private readonly TransactionScope _transactionScope;

        public AsyncTransactionScopeWrapper()
        {
            _transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        public void Complete()
        {
            _transactionScope.Complete();
        }

        public void Dispose()
        {
            _transactionScope.Dispose();
        }
    }
}
