using CloudStatus.Library.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudStatus.Library.Repositories
{
    public class QueryRepository : IQueryRepository
    {
        public async Task<List<ServerLoad>> RetrieveAveragesByMinute(DateTime from, DateTime to)
        {
            return new List<ServerLoad>
            {
                new ServerLoad
                {
                    DateTime = DateTime.Now,
                    AverageCpuLoad = 3.17,
                    AverageRamLoad = 14.34
                }
            };
        }

        public async Task<List<ServerLoad>> RetrieveAveragesByHour(DateTime from, DateTime to)
        {
            return new List<ServerLoad>
            {
                new ServerLoad
                {
                    DateTime = DateTime.Now,
                    AverageCpuLoad = 3.17,
                    AverageRamLoad = 14.34
                }
            };
        }
    }
}
