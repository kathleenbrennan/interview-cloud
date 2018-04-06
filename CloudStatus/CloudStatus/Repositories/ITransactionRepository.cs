using CloudStatus.Library.Models;
using System.Threading.Tasks;

namespace CloudStatus.Library.Repositories
{
    public interface ITransactionRepository
    {
        Task RecordServerLoad(ServerLoadTransaction data);
    }
}