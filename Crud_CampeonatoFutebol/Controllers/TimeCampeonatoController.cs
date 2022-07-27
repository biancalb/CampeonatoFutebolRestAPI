using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crud_CampeonatoFutebol.Models;
using Crud_CampeonatoFutebol.Models.Services;
using Crud_CampeonatoFutebol.Models.Resources;

namespace Crud_TimeCampeonatoFutebol.Controllers
{
    [RoutePrefix("TimeCampeonato")]

    public class TimeCampeonatoController : ApiController
    {
        private readonly TimeCampeonatoSvc TimeCampeonatoSvc = new TimeCampeonatoSvc();

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage All()
        {
            var result = TimeCampeonatoSvc.List();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("get/{id}", Name = "Route.TimeCampeonato.Get")]
        public HttpResponseMessage Get(long id)
        {
            var result = TimeCampeonatoSvc.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody] TimeCampeonatoModel body)
        {
            var TimeCampeonato = new TimeCampeonato
            {
                CampeonatoId = body.CampeonatoId,
                TimeId = body.TimeId,
            };
            var result = TimeCampeonatoSvc.Add(TimeCampeonato);

            var response = Request.CreateResponse(result > 0 ? HttpStatusCode.Created : HttpStatusCode.OK, result);
            if (result > 0) response.Headers.Location = new Uri(Url.Link("Route.TimeCampeonato.Get", new { id = TimeCampeonato.TimeCampeonatoId }));
            return response;
        }

        [HttpPut]
        [Route("edit/{id}")]
        public HttpResponseMessage Edit([FromUri] int id, [FromBody] TimeCampeonatoModel body)
        {
            var timeCampeonatoSaved = TimeCampeonatoSvc.Find(id);
            if (timeCampeonatoSaved == null) throw new NullReferenceException($"Não foi possível encontrar {nameof(TimeCampeonato)} com id = {id}");

            var timeCampeonato = new TimeCampeonato
            {
                TimeCampeonatoId = id,
                CampeonatoId = body.CampeonatoId,
                TimeId = body.TimeId,
            };
            var result = TimeCampeonatoSvc.Update(timeCampeonato);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //[HttpDelete]
        //[Route("delete/{id}")]
        //public HttpResponseMessage Delete(long id)
        //{
        //    var result = TimeCampeonatoSvc.Remove(id);
        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}
    }
}
