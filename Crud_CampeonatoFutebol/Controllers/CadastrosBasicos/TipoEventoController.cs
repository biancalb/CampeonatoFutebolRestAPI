using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crud_CampeonatoFutebol.Models.Services;

namespace Crud_CampeonatoFutebol.Controllers.CadastrosBasicos
{
    [RoutePrefix("tipoevento")]

    public class TipoEventoController : ApiController
    {
        private readonly TipoEventoSvc TipoEventoSvc = new TipoEventoSvc();

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage All()
        {
            var result = TipoEventoSvc.List();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("get/{id}", Name = "Route.tipoevento.Get")]
        public HttpResponseMessage Get(long id)
        {
            var result = TipoEventoSvc.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
