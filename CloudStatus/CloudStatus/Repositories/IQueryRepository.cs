using CloudStatus.Library.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudStatus.Library.Repositories
{
    public interface IQueryRepository
    {
        Task<List<ServerLoad>> RetrieveAveragesByMinute(DateTime from, DateTime to);
        Task<List<ServerLoad>> RetrieveAveragesByHour(DateTime from, DateTime to);
    }
}