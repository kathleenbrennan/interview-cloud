using CloudStatus.Library.Models;
using System;
using System.Threading.Tasks;

namespace CloudStatus.Library.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {

        public Task RecordServerLoad(ServerLoadTransaction data)
        {
            throw new NotImplementedException();
        }
    }
}
