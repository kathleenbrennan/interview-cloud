using System;

namespace CloudStatus.Library.Models
{
    public class ServerLoadTransaction
    {
        public DateTime TimeStamp { get; set; }

        public string ServerName { get; set; }

        public double CpuLoad { get; set; }

        public double RamLoad { get; set; }
    }
}