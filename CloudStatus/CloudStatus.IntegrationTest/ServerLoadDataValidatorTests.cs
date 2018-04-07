using System;
using CloudStatus.Api.Contracts;
using CloudStatus.Api.Validators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudStatus.IntegrationTest
{
    [TestClass]
    public class ServerLoadDataValidatorTests
    {
        [TestMethod]
        public void ServerLoadData_Validator_Success()
        {
            var data = new ServerLoadRequest
            {
                ServerName = "MyServer",
                RamLoad = 11.009,
                CpuLoad = 9.034
            };

            var validator = new ServerLoadDataValidator(data);

            Assert.IsTrue(validator.IsValid());
        }
        [TestMethod]
        public void ServerLoadData_Validator_RamLoadZero_Success()
        {
            var data = new ServerLoadRequest
            {
                ServerName = "MyServer",
                RamLoad = 0.0,
                CpuLoad = 9.034
            };

            var validator = new ServerLoadDataValidator(data);

            Assert.IsTrue(validator.IsValid());
        }

        [TestMethod]
        public void ServerLoadData_Validator_CpuLoadZero_Success()
        {
            var data = new ServerLoadRequest
            {
                ServerName = "MyServer",
                RamLoad = 11.009,
                CpuLoad = 0.0
            };

            var validator = new ServerLoadDataValidator(data);

            Assert.IsTrue(validator.IsValid());
        }
        
        [TestMethod]
        public void ServerLoadData_Validator_ServerNameEmpty_Fail()
        {
            var data = new ServerLoadRequest
            {
                ServerName = string.Empty,
                RamLoad = 11.009,
                CpuLoad = 9.034
            };

            var validator = new ServerLoadDataValidator(data);

            Assert.IsFalse(validator.IsValid());
        }

        [TestMethod]
        public void ServerLoadData_Validator_RamLoadNegative_Fail()
        {
            var data = new ServerLoadRequest
            {
                ServerName = "MyServer",
                RamLoad = -10.30,
                CpuLoad = 9.034
            };

            var validator = new ServerLoadDataValidator(data);

            Assert.IsFalse(validator.IsValid());
        }

        [TestMethod]
        public void ServerLoadData_Validator_CpuLoadNegative_Fail()
        {
            var data = new ServerLoadRequest
            {
                ServerName = "MyServer",
                RamLoad = 11.009,
                CpuLoad = -13.23
            };

            var validator = new ServerLoadDataValidator(data);

            Assert.IsFalse(validator.IsValid());
        }
    }
}
