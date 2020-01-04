using Gasstation.Implementation;
using System.Collections.Generic;

namespace Gasstation.Interfaces
{
    public interface IDataRepository
    {
        IReadOnlyList<Transaction> StoredTransactions { get; set; }
    }
}
