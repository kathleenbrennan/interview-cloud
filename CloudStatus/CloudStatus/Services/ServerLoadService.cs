using CloudStatus.Library.Repositories;
using CloudStatus.Library.Models;
using CloudStatus.Library.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CloudStatus.Library.Services
{
    public class ServerLoadService : IServerLoadService
    {
        //CQRS separation of read and write repositories so each can be optimized
        private readonly ITransactionRepository transactionRepository;
        private readonly IQueryRepository queryRepository;
        private ServerLoadDataValidator serverLoadDataValidator;

        public ServerLoadService()
        {
            // normally you would initialize these from the constructor using an IoC container

            this.transactionRepository = new TransactionRepository();
            this.queryRepository = new QueryRepository();
        }

        public async Task Record(ServerLoadTransaction data)
        {
            this.serverLoadDataValidator = new ServerLoadDataValidator(data);
            if(!this.serverLoadDataValidator.IsValid())
            {
                throw new ValidationException("Unable to store data due to invalid properties");
            }
            await this.transactionRepository.RecordServerLoad(data);
        }

        public async Task<List<ServerLoad>> RetrieveAveragesByMinuteLastHour()
        {
            return await this.queryRepository.RetrieveAveragesByMinute(DateTime.Now.AddHours(-1), DateTime.Now);
        }

        public async Task<List<ServerLoad>> RetrieveAveragesByHourLastDay()
        {
            return await this.queryRepository.RetrieveAveragesByHour(DateTime.Now.AddDays(-1), DateTime.Now);
        }
    }
}
