using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace test.Controllers
{
    public class ConsultaCaraController : ApiController
    {
        // POST api/<controller>
        [Route("api/consultar")]
        [HttpPost]
        public async void Post()
        {
            byte[] file = await Request.Content.ReadAsByteArrayAsync();
            var x = await FaceApi.MakeAnalysis(file);
            var response = Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
        }
    }
}