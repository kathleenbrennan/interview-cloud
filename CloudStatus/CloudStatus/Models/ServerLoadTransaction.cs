using System;
using System.Collections.Generic;
using System.Text;

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
