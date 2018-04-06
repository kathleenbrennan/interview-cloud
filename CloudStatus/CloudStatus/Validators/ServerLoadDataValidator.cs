using System;
using CloudStatus.Library.Models;

namespace CloudStatus.Library.Validators
{
    public class ServerLoadDataValidator
    {
        private readonly ServerLoadTransaction data;

        public ServerLoadDataValidator(ServerLoadTransaction data)
        {
            this.data = data;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.data.ServerName)
                && this.data.CpuLoad >= 0
                && this.data.RamLoad >= 0
                && this.data.TimeStamp >= DateTime.MinValue;
        }
    }
}
