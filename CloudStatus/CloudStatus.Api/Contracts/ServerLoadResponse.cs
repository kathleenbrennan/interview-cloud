using System;

namespace CloudStatus.Api.Contracts
{
    public class ServerLoadResponse
    {
        public DateTime DateTime { get; set; }
        public double AverageCpuLoad { get; set; }
        public double AverageRamLoad { get; set; }
    }
}