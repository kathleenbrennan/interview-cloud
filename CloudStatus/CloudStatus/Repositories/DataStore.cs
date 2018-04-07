using CloudStatus.Library.Models;
using System;
using System.Collections.Generic;

namespace CloudStatus.Library.Repositories
{
    public static class DataStore
    {
        private static List<ServerLoadTransaction> data = InitializeData();

        public static void WriteTransaction(ServerLoadTransaction transaction)
        {
            data.Add(transaction);

            // in a production system this CQRS pattern would be supported by
            // a message queue or service bus that would independently
            // synchronize the read-only data store from a set of transactions.
            // Here, for simplicity, we are just writing the data from the transaction
            // directly to the in-memory data store.
        }

        public static List<ServerLoadTransaction> GetData()
        {
            return data;
        }

        private static List<ServerLoadTransaction> InitializeData()
        {
            // we want to have data populated for the last couple of days
            // for testing purposes

            var data = new List<ServerLoadTransaction>();

            var rnd = new Random(DateTime.Now.Millisecond);

            //add some older data that should always be filtered out
            data.Add(GenerateDummyData(DateTime.Now.AddDays(-3), rnd));

            // initialize some data for yesterday's date
            for (var i = 0; i <= 24; i++)
            {
                data.Add(GenerateDummyData(DateTime.Now.AddDays(-1).AddHours(-i), rnd));
            }

            // initialize some data for today
            for (var i = 0; i <= 24; i++)
            {
                data.Add(GenerateDummyData(DateTime.Now.AddHours(-i), rnd));
            }

            // initialize some more granular data for the last couple of hours

            for (var i = 0; i <= 100; i++)
            {
                var timeStamp = DateTime.Now.AddSeconds(0-rnd.Next(0, 3600));
                data.Add(GenerateDummyData(timeStamp, rnd));
            }
            return data;
        }

        private static ServerLoadTransaction GenerateDummyData(DateTime timeStamp, Random rnd)
        {
            return new ServerLoadTransaction
            {
                ServerName = Guid.NewGuid().ToString(),
                TimeStamp = timeStamp,
                CpuLoad = rnd.NextDouble() * 100,
                RamLoad = rnd.NextDouble() * 100
            };
        }

        
    }
}
