using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crud_CampeonatoFutebol.Models;
using Crud_CampeonatoFutebol.Models.Services;
using Crud_CampeonatoFutebol.Models.Resources;

namespace Crud_CampeonatoFutebol.Controllers.CadastrosBasicos
{
    [RoutePrefix("time")]

    public class TimeController : ApiController
    {
        private readonly TimeSvc TimeSvc = new TimeSvc();

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage All()
        {
            var result = TimeSvc.List();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("get/{id}", Name = "Route.time.Get")]
        public HttpResponseMessage Get(long id)
        {
            var result = TimeSvc.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody] TimeModel body)
        {
            var Time = new Time
            {
                Nome = body.Nome,
                EstadoId = body.EstadoId,
            };
            var result = TimeSvc.Add(Time);

            var response = Request.CreateResponse(result > 0 ? HttpStatusCode.Created : HttpStatusCode.OK, result);
            if (result > 0) response.Headers.Location = new Uri(Url.Link("Route.Time.Get", new { id = Time.TimeId }));
            return response;
        }

        [HttpPut]
        [Route("edit/{id}")]
        public HttpResponseMessage Edit([FromUri] int id, [FromBody] TimeModel body)
        {
            var timeSaved = TimeSvc.Find(id);
            if (timeSaved == null) throw new NullReferenceException($"Não foi possível encontrar {nameof(Time)} com id = {id}");

            var time = new Time
            {
                Nome = body.Nome,
                EstadoId = body.EstadoId,
            };
            var result = TimeSvc.Update(time);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(long id)
        {
            var result = TimeSvc.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
