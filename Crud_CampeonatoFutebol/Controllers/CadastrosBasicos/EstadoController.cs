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
    [RoutePrefix("estado")]

    public class EstadoController : ApiController
    {
        private readonly EstadoSvc EstadoSvc = new EstadoSvc();

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage All()
        {
            var result = EstadoSvc.List();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("get/{id}", Name = "Route.estado.Get")]
        public HttpResponseMessage Get(long id)
        {
            var result = EstadoSvc.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
