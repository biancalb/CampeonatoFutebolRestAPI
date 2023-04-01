using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crud_CampeonatoFutebol.Models;
using Crud_CampeonatoFutebol.Models.Services;
using Crud_CampeonatoFutebol.Models.Resources;

namespace Crud_CampeonatoFutebol.Controllers
{
    [RoutePrefix("Partida")]

    public class PartidaController : ApiController
    {
        private readonly PartidaSvc PartidaSvc = new PartidaSvc();
        private readonly TimeCampeonatoSvc TimeCampeonatoSvc = new TimeCampeonatoSvc();

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage All()
        {
            var result = PartidaSvc.List();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("get/{id}", Name = "Route.Partida.Get")]
        public HttpResponseMessage Get(long id)
        {
            var result = PartidaSvc.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody] PartidaModel body)
        {
            DateTime dt;

            if (DateTime.TryParse(body.DataInicio, out DateTime data)) dt = data;
            else throw new FormatException($"Data de Nascimento inválida");

            var TimeCampeonato1 = TimeCampeonatoSvc.Find(body.TimeCampeonato1Id);
            var TimeCampeonato2 = TimeCampeonatoSvc.Find(body.TimeCampeonato2Id);

            if (TimeCampeonato1.CampeonatoId != TimeCampeonato2.CampeonatoId) throw new ApplicationException($"Os times devem estar no mesmo campeonato");

            var Partida = new Partida
            {
                TimeCampeonato1Id = body.TimeCampeonato1Id,
                TimeCampeonato2Id = body.TimeCampeonato2Id,
                DataInicio = dt,
                ResultadoPartidaId = body.ResultadoPartidaId,
                EstadioId = body.EstadioId,
            };
            var result = PartidaSvc.Add(Partida);

            var response = Request.CreateResponse(result > 0 ? HttpStatusCode.Created : HttpStatusCode.OK, result);
            if (result > 0) response.Headers.Location = new Uri(Url.Link("Route.Partida.Get", new { id = Partida.PartidaId }));
            return response;
        }

        [HttpPut]
        [Route("edit/{id}")]
        public HttpResponseMessage Edit([FromUri] int id, [FromBody] PartidaModel body)
        {
            var partidaSaved = PartidaSvc.Find(id);
            if (partidaSaved == null) throw new NullReferenceException($"Não foi possível encontrar {nameof(Partida)} com id = {id}");          
            DateTime dt;

            if (DateTime.TryParse(body.DataInicio, out DateTime data)) dt = data;
            else throw new FormatException($"Data de Nascimento inválida");
            var partida = new Partida
            {
                PartidaId = id,
                TimeCampeonato1Id = body.TimeCampeonato1Id,
                TimeCampeonato2Id = body.TimeCampeonato2Id,
                DataInicio = dt,
                ResultadoPartidaId = body.ResultadoPartidaId,
                EstadioId = body.EstadioId,
            };
            var result = PartidaSvc.Update(partida);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        //[HttpDelete]
        //[Route("delete/{id}")]
        //public HttpResponseMessage Delete(long id)
        //{
        //    var result = PartidaSvc.Remove(id);
        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}
    }
}
