using System;

namespace CloudStatus.Library.Models
{
    public class ServerLoad
    {
        public DateTime DateTime { get; internal set; }
        public double AverageCpuLoad { get; internal set; }
        public double AverageRamLoad { get; internal set; }
    }
}
