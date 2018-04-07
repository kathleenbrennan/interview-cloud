using CloudStatus.Api.Contracts;

namespace CloudStatus.Api.Validators
{
    public class ServerLoadDataValidator
    {
        private readonly ServerLoadRequest data;

        public ServerLoadDataValidator(ServerLoadRequest data)
        {
            this.data = data;
        }

        // normally a validator would return a ValidationResult object containing
        // the true/false isValid value as well as 
        // zero to many messages describing what properties were invalid
        // and what was expected

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.data.ServerName)
                   && this.data.CpuLoad >= 0
                   && this.data.RamLoad >= 0;
        }
    }
}