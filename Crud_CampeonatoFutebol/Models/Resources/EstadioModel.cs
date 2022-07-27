using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud_CampeonatoFutebol.Models.Resources
{
    public class EstadioModel
    {
        public string EstadioId { get; set; }
        public string Nome { get; set; }
        public string Capacidade { get; set; }
        public string EstadoId { get; set; }
    }
}