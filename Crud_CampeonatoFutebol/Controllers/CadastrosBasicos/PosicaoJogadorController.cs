using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crud_CampeonatoFutebol.Models.Services;

namespace Crud_CampeonatoFutebol.Controllers.CadastrosBasicos
{
    [RoutePrefix("posicaoJogador")]

    public class PosicaoJogadorController : ApiController
    {
        private readonly PosicaoJogadorSvc PosicaoJogadorSvc = new PosicaoJogadorSvc();

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage All()
        {
            var result = PosicaoJogadorSvc.List();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("get/{id}", Name = "Route.posicaoJogador.Get")]
        public HttpResponseMessage Get(long id)
        {
            var result = PosicaoJogadorSvc.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
