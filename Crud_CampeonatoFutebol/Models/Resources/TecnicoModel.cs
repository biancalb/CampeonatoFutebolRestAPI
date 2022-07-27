using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud_CampeonatoFutebol.Models.Resources
{
    public class TecnicoModel
    {
        public int TecnicoId { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public int TimeAtualID { get; set; }
    }
}