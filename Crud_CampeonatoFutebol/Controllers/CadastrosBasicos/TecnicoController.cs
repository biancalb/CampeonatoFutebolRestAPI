using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crud_CampeonatoFutebol.Models;
using Crud_CampeonatoFutebol.Models.Services;
using Crud_CampeonatoFutebol.Models.Resources;

namespace Crud_CampeonatoFutebol.Controllers.CadastrosBasicos
{
    [RoutePrefix("tecnico")]

    public class TecnicoController : ApiController
    {
        private readonly TecnicoSvc TecnicoSvc = new TecnicoSvc();

        [HttpGet]
        [Route("getAll")]
        public HttpResponseMessage All()
        {
            var result = TecnicoSvc.List();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("get/{id}", Name = "Route.tecnico.Get")]
        public HttpResponseMessage Get(long id)
        {
            var result = TecnicoSvc.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody] TecnicoModel body)
        {
            DateTime dtNascimento;

            if (DateTime.TryParse(body.DataNascimento, out DateTime dataNascimento)) dtNascimento = dataNascimento;
            else throw new FormatException($"Data de Nascimento inválida");

            var Tecnico = new Tecnico
            {
                Nome = body.Nome,
                DataNascimento = dtNascimento,
                TimeAtualId = body.TimeAtualID,
            };
            var result = TecnicoSvc.Add(Tecnico);

            var response = Request.CreateResponse(result > 0 ? HttpStatusCode.Created : HttpStatusCode.OK, result);
            if (result > 0) response.Headers.Location = new Uri(Url.Link("Route.Tecnico.Get", new { id = Tecnico.TecnicoId }));
            return response;
        }

        [HttpPut]
        [Route("edit/{id}")]
        public HttpResponseMessage Edit([FromUri] int id, [FromBody] TecnicoModel body)
        {
            DateTime dtNascimento;

            if (DateTime.TryParse(body.DataNascimento, out DateTime dataNascimento)) dtNascimento = dataNascimento;
            else throw new FormatException($"Data de Nascimento inválida"); ;

            var Tecnico = new Tecnico
            {
                TecnicoId = id,
                Nome = body.Nome,
                DataNascimento = dtNascimento,
                TimeAtualId = body.TimeAtualID,
            };
            var result = TecnicoSvc.Update(Tecnico);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(long id)
        {
            var result = TecnicoSvc.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
