using System.Collections.Generic;
using CloudStatus.Library.Models;
using System.Threading.Tasks;

namespace CloudStatus.Library.Services
{
    public interface IServerLoadService
    {
        Task Record(ServerLoadTransaction data);
        Task<List<ServerLoad>> RetrieveAveragesByHourLastDay();
        Task<List<ServerLoad>> RetrieveAveragesByMinuteLastHour();
    }
}