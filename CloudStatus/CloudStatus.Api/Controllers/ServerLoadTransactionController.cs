using CloudStatus.Api.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CloudStatus.Api.Validators;
using CloudStatus.Library.Models;
using CloudStatus.Library.Services;

namespace CloudStatus.Api.Controllers
{
    public class ServerLoadTransactionController : ApiController
    {
        private readonly ServerLoadService ServerLoadService;
        private ServerLoadDataValidator serverLoadDataValidator;

        protected ServerLoadTransactionController()
        {            
            // normally this would be populated from the constructor using an IoC Container
            this.ServerLoadService = new ServerLoadService();
        }

        // POST: api/ServerLoad
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]ServerLoadRequest request)
        {
            this.serverLoadDataValidator = new ServerLoadDataValidator(request);
            if (!this.serverLoadDataValidator.IsValid())
            {
                return BadRequest("Unable to store data due to invalid properties");
            }

            // normally would use AutoMapper here but doing it manually to keep it simple
            ServerLoadTransaction transaction = new ServerLoadTransaction
            {
                TimeStamp = DateTime.Now,
                ServerName = request.ServerName,
                CpuLoad = request.CpuLoad,
                RamLoad = request.RamLoad
            };

            try
            {
                await this.ServerLoadService.Record(transaction);
                return Ok(); // hack: this should really be Created but we don't have a route in this implementation
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
