using CloudStatus.Api.Contracts;
using CloudStatus.Library.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;

namespace CloudStatus.Api.Controllers
{
    public class ServerLoadByDayController : ApiController
    {
        private ServerLoadService ServerLoadService;

        protected ServerLoadByDayController()
        {
            // normally this would be populated from the constructor using an IoC Container
            this.ServerLoadService = new ServerLoadService();
        }

        [HttpGet]
        public async Task<List<ServerLoadResponse>> Get()
        {
            var data = await this.ServerLoadService.RetrieveAveragesByMinuteLastHour();

            // normally we would use AutoMapper here instead of manually mapping

            return data.Select(d => 
                new ServerLoadResponse
                {
                    DateTime = d.DateTime,
                    AverageCpuLoad = d.AverageCpuLoad,
                    AverageRamLoad = d.AverageRamLoad
                })
                .ToList();
        }
    }
}
