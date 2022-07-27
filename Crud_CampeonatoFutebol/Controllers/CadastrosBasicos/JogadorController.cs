using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crud_CampeonatoFutebol.Models;
using Crud_CampeonatoFutebol.Models.Services;
using Crud_CampeonatoFutebol.Models.Resources;
using Crud_CampeonatoFutebol.Models.QueueHandler;

namespace Crud_CampeonatoFutebol.Controllers.CadastrosBasicos
{
    [RoutePrefix("jogador")]

    public class JogadorController : ApiController
    {
        private readonly JogadorSvc JogadorSvc = new JogadorSvc();

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage All()
        {
            var result = JogadorSvc.List();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("get/{id}", Name = "Route.jogador.Get")]
        public HttpResponseMessage Get(long id)
        {
            var result = JogadorSvc.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody] JogadorModel body)
        {
            DateTime dtNascimento;

            if (DateTime.TryParse(body.DataNascimento, out DateTime dataNascimento)) dtNascimento = dataNascimento;
            else throw new FormatException($"Data de Nascimento inválida"); 

            var jogador = new Jogador
            {
                Nome = body.Nome,
                DataNascimento = dtNascimento,
                TimeAtualID = body.TimeAtualID,
                Salario = body.Salario,
                MesesContrato = body.MesesContrato ,
                PosicaoJogadorId = body.PosicaoJogadorId,
            };
            var result = JogadorSvc.Add(jogador);

            var response = Request.CreateResponse(result > 0 ? HttpStatusCode.Created : HttpStatusCode.OK, result);
            if (result > 0) response.Headers.Location = new Uri(Url.Link("Route.jogador.Get", new { id = jogador.JogadorId }));
            return response;
        }

        [HttpPut]
        [Route("edit/{id}")]
        public HttpResponseMessage Edit([FromUri] int id, [FromBody] JogadorModel body)
        {
            var jogadorSaved = JogadorSvc.Find(id);
            if (jogadorSaved == null) throw new NullReferenceException($"Não foi possível encontrar {nameof(Jogador)} com id = {body.JogadorId}");

            DateTime dtNascimento;

            if (DateTime.TryParse(body.DataNascimento, out DateTime dataNascimento)) dtNascimento = dataNascimento;
            else throw new FormatException($"Data de Nascimento inválida");

            var jogador = new Jogador
            {
                JogadorId = id,
                Nome = body.Nome,
                DataNascimento = dtNascimento,
                TimeAtualID = body.TimeAtualID,
                Salario = body.Salario,
                MesesContrato = body.MesesContrato,
                PosicaoJogadorId = body.PosicaoJogadorId,
            };
            var result = JogadorSvc.Update(jogador);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(long id)
        {
            var result = JogadorSvc.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
