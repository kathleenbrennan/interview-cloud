using CloudStatus.Library.Models;
using System;
using System.Threading.Tasks;

namespace CloudStatus.Library.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {

        public async Task RecordServerLoad(ServerLoadTransaction data)
        {
            DataStore.WriteTransaction(data);
        }
    }
}
