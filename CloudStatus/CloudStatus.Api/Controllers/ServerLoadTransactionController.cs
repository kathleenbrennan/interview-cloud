using CloudStatus.Api.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CloudStatus.Api.Controllers
{
    public class ServerLoadTransactionController : ApiController
    {
        // POST: api/ServerLoad
        public void Post([FromBody]ServerLoadRequest request)
        {
        }
    }
}
