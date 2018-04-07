using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudStatus.Library.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudStatus.Library.Models;

namespace CloudStatus.IntegrationTest
{
    [TestClass]
    public class ServerLoadServiceTests
    {
        // if we were using an IoC container, we could inject mocks into the services under test
        // to manipulate the behavior and test unhappy paths such as the data store not being available

        [TestMethod]
        public void ServerLoadService_WriteTransaction_Success()
        {
            var serverLoadService = new ServerLoadService();

            var transaction = new ServerLoadTransaction
            {
                TimeStamp = DateTime.Now,
                ServerName = "TestServer",
                CpuLoad = 13.3,
                RamLoad = 6.3501
            };

            var result = serverLoadService.Record(transaction);
            Assert.IsNotNull(result);
            Assert.AreEqual(TaskStatus.RanToCompletion, result.Status);
        }

        [TestMethod]
        public void ServerLoadService_RetrieveAveragesByHourLastDay_Success()
        {
            var serverLoadService = new ServerLoadService();

            Task<List<ServerLoad>> result = serverLoadService.RetrieveAveragesByHourLastDay();

            Assert.IsNotNull(result);
            Assert.AreEqual(TaskStatus.RanToCompletion, result.Status);
            Assert.IsNotNull(result.Result);
            Assert.IsFalse(result.Result.Count > 25, "Expected no more than 25 results.");

            Assert.IsFalse(result.Result.Any(r => r.DateTime == DateTime.MinValue));

            // check that none of the results are from more than a day and an hour ago
            DateTime dateToCompare = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day, DateTime.Now.Hour, 0, 0);
            Assert.IsTrue(result.Result.All(r => r.DateTime >= dateToCompare), $"Expected no results from prior to {dateToCompare}");

            Assert.IsTrue(result.Result.All(r => r.AverageRamLoad >= 0), "Expected no negative values for Average RAM Load");
            Assert.IsTrue(result.Result.All(r => r.AverageCpuLoad >= 0), "Expected no negative values for Average CPU Load");

            Assert.AreEqual(DateTime.Now.Day, result.Result.First().DateTime.Day, "Expected the most recent result to be for the current day.");

            Assert.AreEqual(DateTime.Now.Hour, result.Result.First().DateTime.Hour, "Expected the most recent result to be for the current hour.");

        }

        [TestMethod]
        public void ServerLoadService_RetrieveAveragesByMinuteLastHour_Success()
        {
            var serverLoadService = new ServerLoadService();

            Task<List<ServerLoad>> result = serverLoadService.RetrieveAveragesByMinuteLastHour();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsFalse(result.Result.Count > 61, "Expected no more than 61 results.");

            Assert.IsFalse(result.Result.Any(r => r.DateTime == DateTime.MinValue));

            // check that none of the results are from more than an hour and a minute ago
            DateTime dateToCompare = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.AddHours(-1).Hour, DateTime.Now.AddMinutes(-1).Minute, 0);
            Assert.IsTrue(result.Result.All(r => r.DateTime >= dateToCompare), $"Expected no results from prior to {dateToCompare}");

            Assert.IsTrue(result.Result.All(r => r.AverageRamLoad >= 0), "Expected no negative values for Average RAM Load");
            Assert.IsTrue(result.Result.All(r => r.AverageCpuLoad >= 0), "Expected no negative values for Average CPU Load");
            
            // check that the most recent result is for the current hour
            Assert.AreEqual(DateTime.Now.Hour, result.Result.First().DateTime.Hour, "Expected the most recent result to be for the current hour.");
            Assert.AreEqual(DateTime.Now.Minute, result.Result.First().DateTime.Minute, "Expected the most recent result to be for the current minute.");

        }

        [TestMethod]
        public void ServerLoadService_Workflow_Success()
        {
            var serverLoadService = new ServerLoadService();

            Task<List<ServerLoad>> firstRetrieveResult = serverLoadService.RetrieveAveragesByHourLastDay();
            Assert.IsNotNull(firstRetrieveResult);
            Assert.IsNotNull(firstRetrieveResult.Result);
            var mostRecentHourCpuLoad = firstRetrieveResult.Result.First().AverageCpuLoad;
            Console.WriteLine($"Average CPU Load Before Updating: {mostRecentHourCpuLoad}");
            var mostRecentHourRamLoad = firstRetrieveResult.Result.First().AverageRamLoad;
            Console.WriteLine($"Average RAM Load Before Updating: {mostRecentHourRamLoad}");

            // write a new transaction
            var transaction = new ServerLoadTransaction
            {
                TimeStamp = DateTime.Now,
                ServerName = "TestServer",
                CpuLoad = 13.3,
                RamLoad = 6.3501
            };

            var writeResult = serverLoadService.Record(transaction);
            Assert.IsNotNull(writeResult);

            // get the averages again
            Task<List<ServerLoad>> secondRetrieveResult = serverLoadService.RetrieveAveragesByHourLastDay();
            Assert.IsNotNull(secondRetrieveResult);
            Assert.IsNotNull(secondRetrieveResult.Result);
            var updatedMostRecentHourCpuLoad = secondRetrieveResult.Result.First().AverageCpuLoad;
            Console.WriteLine($"Average CPU Load After Updating: {updatedMostRecentHourCpuLoad}");
            var updatedMostRecentHourRamLoad = secondRetrieveResult.Result.First().AverageRamLoad;
            Console.WriteLine($"Average RAM Load After Updating: {updatedMostRecentHourRamLoad}");

            Assert.AreNotEqual(mostRecentHourCpuLoad, updatedMostRecentHourCpuLoad);
            Assert.AreNotEqual(mostRecentHourRamLoad, updatedMostRecentHourRamLoad);
        }
    }
}
