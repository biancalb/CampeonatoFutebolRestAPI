using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crud_CampeonatoFutebol.Models;
using Crud_CampeonatoFutebol.Models.Services;
using Crud_CampeonatoFutebol.Models.Resources;

namespace Crud_TransferenciaFutebol.Controllers
{
    [RoutePrefix("Transferencia")]

    public class TransferenciaController : ApiController
    {
        private readonly TransferenciaSvc TransferenciaSvc = new TransferenciaSvc();
        private readonly JogadorSvc JogadorSvc = new JogadorSvc();

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage All()
        {
            var result = TransferenciaSvc.List();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("get/{id}", Name = "Route.Transferencia.Get")]
        public HttpResponseMessage Get(long id)
        {
            var result = TransferenciaSvc.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody] TransferenciaModel body)
        {
            DateTime dt;

            if (DateTime.TryParse(body.Data, out DateTime data)) dt = data;
            else throw new FormatException($"Data de Nascimento inválida");

            var Transferencia = new Transferencias
            {
                JogadorId = body.JogadorId,
                TimeOrigemId = body.TimeOrigemId,
                TImeDestinoId = body.TImeDestinoId,
                Valor = body.Valor,
                Data = dt,
            };
            var result = TransferenciaSvc.Add(Transferencia);

            var response = Request.CreateResponse(result > 0 ? HttpStatusCode.Created : HttpStatusCode.OK, result);
            if (result > 0)
            {
                var jogadorSaved = JogadorSvc.Find(Transferencia.JogadorId);
                jogadorSaved.TimeAtualID = Transferencia.TimeOrigemId;
                var resultJ = JogadorSvc.Update(jogadorSaved);

                response.Headers.Location = new Uri(Url.Link("Route.Transferencia.Get", new { id = Transferencia.TransferenciasId }));
            }
            return response;
        }

        [HttpPut]
        [Route("edit/{id}")]
        public HttpResponseMessage Edit([FromUri] int id, [FromBody] TransferenciaModel body)
        {
            var timeCampeonatoSaved = TransferenciaSvc.Find(id);
            if (timeCampeonatoSaved == null) throw new NullReferenceException($"Não foi possível encontrar {nameof(TimeCampeonato)} com id = {id}");
            
            DateTime dt;

            if (DateTime.TryParse(body.Data, out DateTime data)) dt = data;
            else throw new FormatException($"Data de Nascimento inválida");
            
            var timeCampeonato = new Transferencias
            {
                JogadorId = id,
                TimeOrigemId = body.TimeOrigemId,
                TImeDestinoId = body.TImeDestinoId,
                Valor = body.Valor,
                Data = dt,
            };
            var result = TransferenciaSvc.Update(timeCampeonato);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //[HttpDelete]
        //[Route("delete/{id}")]
        //public HttpResponseMessage Delete(long id)
        //{
        //    var result = TransferenciaSvc.Remove(id);
        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}
    }
}
