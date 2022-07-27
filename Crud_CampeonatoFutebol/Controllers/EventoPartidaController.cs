using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crud_CampeonatoFutebol.Models;
using Crud_CampeonatoFutebol.Models.Services;
using Crud_CampeonatoFutebol.Models.Resources;
using RabbitMQ.Client;
using Crud_CampeonatoFutebol.Models.QueueHandler;

namespace Crud_CampeonatoFutebol.Controllers
{
    [RoutePrefix("EventoPartida")]

    public class EventoPartidaController : ApiController
    {
        private readonly EventoPartidaSvc EventoPartidaSvc = new EventoPartidaSvc();

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage All()
        {
            var result = EventoPartidaSvc.List();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("get/{id}", Name = "Route.EventoPartida.Get")]
        public HttpResponseMessage Get(long id)
        {
            var result = EventoPartidaSvc.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody] EventoPartidaModel body)
        {
            var EventoPartida = new EventoPartida
            {
                PartidaId = body.PartidaId,
                TipoEventoId = body.TipoEventoId,
                InstanteEvento = DateTime.Now,
                JogadorId = body.JogadorId,
            };
            var result = EventoPartidaSvc.Add(EventoPartida);

            var response = Request.CreateResponse(result > 0 ? HttpStatusCode.Created : HttpStatusCode.OK, result);
            if (result > 0) Sendmsg($"{EventoPartida.InstanteEvento};{EventoPartida.PartidaId};{EventoPartida.TipoEventoId};{EventoPartida.JogadorId}", nameof(TipoEvento));
            if (result > 0) response.Headers.Location = new Uri(Url.Link("Route.EventoPartida.Get", new { id = EventoPartida.EventoPartidaId }));
            return response;
        }

        [HttpPut]
        [Route("edit/{id}")]
        public HttpResponseMessage Edit([FromUri] int id, [FromBody] EventoPartidaModel body)
        {
            var EventoPartidaSaved = EventoPartidaSvc.Find(id);
            if (EventoPartidaSaved == null) throw new NullReferenceException($"Não foi possível encontrar {nameof(EventoPartida)} com id = {id}");

            var eventoPartida = new EventoPartida
            {
                PartidaId = body.PartidaId,
                TipoEventoId = body.TipoEventoId,
                InstanteEvento = DateTime.Now,
                JogadorId = body.JogadorId,
            };
            var result = EventoPartidaSvc.Update(eventoPartida);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        //[HttpDelete]
        //[Route("delete/{id}")]
        //public HttpResponseMessage Delete(long id)
        //{
        //    var result = EventoPartidaSvc.Remove(id);
        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}
        public void Sendmsg(string message, string friend)
        {
            RabbitMQOperations obj = new RabbitMQOperations();
            IConnection con = obj.GetConnection();
            obj.Send(con, message, friend);
        }
    }
}
