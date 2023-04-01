using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud_CampeonatoFutebol.Models.Resources
{
    public class CampeonatoModel
    {
        public int CampeonatoId { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
    }
}