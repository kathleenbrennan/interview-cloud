using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudStatus.Library.Models;

namespace CloudStatus.Library.Repositories
{
    public class QueryRepository : IQueryRepository
    {
        public async Task<List<ServerLoad>> RetrieveAveragesByMinute(DateTime from, DateTime to)
        {
            // in a production system we would have Entity Framework or some other ORM
            // that enables only the data matching the query to be retrieved
            // in order to reduce size of objects

            var filteredData = DataStore.GetData()
                .Where(d => d.TimeStamp >= from && d.TimeStamp < to);
            return GetDataGroupedByRoundedTime(filteredData, arg => RoundToNearestMinute(arg.TimeStamp));
        }

        public async Task<List<ServerLoad>> RetrieveAveragesByHour(DateTime from, DateTime to)
        {
            // in a production system we would have Entity Framework or some other ORM
            // that enables only the data matching the query to be retrieved
            // in order to reduce size of objects

            var filteredData = DataStore.GetData()
                .Where(d => d.TimeStamp >= from && d.TimeStamp < to);
            return GetDataGroupedByRoundedTime(filteredData, arg => RoundToNearestHour(arg.TimeStamp));
        }

        private static List<ServerLoad> GetDataGroupedByRoundedTime(
            IEnumerable<ServerLoadTransaction> data, 
            Func<ServerLoadTransaction, DateTime> roundingFunc)
        {
            return data
                .Select(d =>
                    new
                    {
                        d.CpuLoad,
                        d.RamLoad,
                        TimeStamp = roundingFunc(d)
                    }
                )
                .GroupBy(d => d.TimeStamp)
                .Select(g => new ServerLoad
                {
                    DateTime = g.Key,
                    AverageCpuLoad = g.Average(x => x.CpuLoad),
                    AverageRamLoad = g.Average(x => x.RamLoad)
                })
                .OrderByDescending(d => d.DateTime)
                .ToList();
        }

        private static DateTime RoundToNearestHour(DateTime timeStamp)
        {
            return new DateTime(
                timeStamp.Year,
                timeStamp.Month,
                timeStamp.Day,
                timeStamp.Hour,
                0, // minutes
                0 // seconds
            );
        }

        private static DateTime RoundToNearestMinute(DateTime timeStamp)
        {
            return new DateTime(
                timeStamp.Year,
                timeStamp.Month,
                timeStamp.Day,
                timeStamp.Hour,
                timeStamp.Minute,
                0 // seconds
            );
        }
    }
}