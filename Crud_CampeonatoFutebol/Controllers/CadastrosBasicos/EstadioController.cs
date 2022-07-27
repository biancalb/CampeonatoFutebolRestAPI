using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crud_CampeonatoFutebol.Models;
using Crud_CampeonatoFutebol.Models.Services;
using Crud_CampeonatoFutebol.Models.Resources;

namespace Crud_CampeonatoFutebol.Controllers.CadastrosBasicos
{
    [RoutePrefix("estadio")]

    public class EstadioController : ApiController
    {
        private readonly EstadioSvc EstadioSvc = new EstadioSvc();

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage All()
        {
            var result = EstadioSvc.List();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("get/{id}", Name = "Route.estadio.Get")]
        public HttpResponseMessage Get(long id)
        {
            var result = EstadioSvc.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody] EstadioModel body)
        {
            var Estadio = new Estadio
            {
                Nome = body.Nome,
                Capacidade = Convert.ToDecimal(body.Capacidade),
                EstadoId = Convert.ToInt32(body.EstadoId),
            };
            var result = EstadioSvc.Add(Estadio);

            var response = Request.CreateResponse(result > 0 ? HttpStatusCode.Created : HttpStatusCode.OK, result);
            if (result > 0) response.Headers.Location = new Uri(Url.Link("Route.Estadio.Get", new { id = Estadio.EstadioId }));
            return response;
        }

        [HttpPut]
        [Route("edit/{id}")]
        public HttpResponseMessage Edit([FromUri] int id, [FromBody] EstadioModel body)
        {
            var estadioSaved = EstadioSvc.Find(body.EstadioId);
            if (estadioSaved == null) throw new NullReferenceException($"Não foi possível encontrar Estadio com id = {body.EstadioId}");

            var Estadio = new Estadio
            {
                EstadioId = id,
                Nome = body.Nome,
                Capacidade = Convert.ToDecimal(body.Capacidade),
                EstadoId = Convert.ToInt32(body.EstadoId),
            };
            var result = EstadioSvc.Update(Estadio);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(long id)
        {
            var result = EstadioSvc.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
