using System;
using CloudStatus.Library.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudStatus.Library.Validators;

namespace CloudStatus.IntegrationTest
{
    [TestClass]
    public class ServerLoadDataValidatorTests
    {
        [TestMethod]
        public void ServerLoadData_Validator_Success()
        {
            var data = new ServerLoadTransaction
            {
                TimeStamp = DateTime.Now,
                ServerName = "MyServer",
                RamLoad = 11.009,
                CpuLoad = 9.034
            };

            var validator = new ServerLoadDataValidator(data);

            Assert.IsTrue(validator.IsValid());
        }
    }
}
