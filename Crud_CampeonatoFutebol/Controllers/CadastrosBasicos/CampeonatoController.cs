using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crud_CampeonatoFutebol.Models;
using Crud_CampeonatoFutebol.Models.Services;
using Crud_CampeonatoFutebol.Models.Resources;

namespace Crud_CampeonatoFutebol.Controllers.CadastrosBasicos
{
    [RoutePrefix("Campeonato")]

    public class CampeonatoController : ApiController
    {
        private readonly CampeonatoSvc CampeonatoSvc = new CampeonatoSvc();

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage All()
        {
            var result = CampeonatoSvc.List();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("get/{id}", Name = "Route.campeonato.Get")]
        public HttpResponseMessage Get(long id)
        {
            var result = CampeonatoSvc.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody] CampeonatoModel body)
        {
            DateTime? dtInicio;
            DateTime? dtFim;

            if (DateTime.TryParse(body.DataInicio, out DateTime dataInicio)) dtInicio = dataInicio;
            else dtInicio = null; 
            
            if (DateTime.TryParse(body.DataFim, out DateTime dataFim)) dtFim = dataFim;
            else dtFim = null;

            var Campeonato = new Campeonato
            {
                Nome = body.Nome,
                Status = body.Status,
                DataInicio = dtInicio,
                DataFim = dtFim,
            };
            var result = CampeonatoSvc.Add(Campeonato);

            var response = Request.CreateResponse(result > 0 ? HttpStatusCode.Created : HttpStatusCode.OK, result);
            if (result > 0) response.Headers.Location = new Uri(Url.Link("Route.Campeonato.Get", new { id = Campeonato.CampeonatoId }));
            return response;
        }

        [HttpPut]
        [Route("edit/{id}")]
        public HttpResponseMessage Edit([FromUri] int id, [FromBody] CampeonatoModel body)
        {
            var campeonatoSaved = CampeonatoSvc.Find(id);
            if (campeonatoSaved == null)  throw new NullReferenceException($"Não foi possível encontrar {nameof(Campeonato)} com id = {id}");
            
            DateTime? dtInicio = null;
            DateTime? dtFim = null;

            if(body.DataInicio != null)
            {
                if (DateTime.TryParse(body.DataInicio, out DateTime dataInicio)) dtInicio = dataInicio;
                else throw new FormatException($"Data inválida");
            }

            if (body.DataFim != null)
            {
                if (DateTime.TryParse(body.DataFim, out DateTime dataFim)) dtFim = dataFim;
                else throw new FormatException($"Data inválida");
            }

            var campeonato = new Campeonato
            {
                CampeonatoId = id,
                Nome = body.Nome,
                Status = body.Status,
                DataInicio = dtInicio,
                DataFim = dtFim,
            };

            var result = CampeonatoSvc.Update(campeonato);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(long id)
        {
            var result = CampeonatoSvc.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
