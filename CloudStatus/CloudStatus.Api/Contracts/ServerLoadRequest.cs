namespace CloudStatus.Api.Contracts
{
    public class ServerLoadRequest
    {
        public string ServerName { get; set; }

        public double CpuLoad { get; set; }

        public double RamLoad { get; set; }
    }
}