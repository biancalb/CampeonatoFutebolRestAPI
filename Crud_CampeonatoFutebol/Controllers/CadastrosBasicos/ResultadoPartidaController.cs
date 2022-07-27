using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crud_CampeonatoFutebol.Models;
using Crud_CampeonatoFutebol.Models.Services;
using Crud_CampeonatoFutebol.Models.Resources;

namespace Crud_CampeonatoFutebol.Controllers.CadastrosBasicos
{
    [RoutePrefix("ResultadoPartida")]

    public class ResultadoPartidaController : ApiController
    {
        private readonly ResultadoPartidaSvc ResultadoPartidaSvc = new ResultadoPartidaSvc();

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage All()
        {
            var result = ResultadoPartidaSvc.List();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("get/{id}", Name = "Route.ResultadoPartida.Get")]
        public HttpResponseMessage Get(long id)
        {
            var result = ResultadoPartidaSvc.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
