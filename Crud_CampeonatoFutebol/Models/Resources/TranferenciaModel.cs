using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud_CampeonatoFutebol.Models.Resources
{
    public class TransferenciaModel
    {
        public int TransferenciasId { get; set; }
        public int JogadorId { get; set; }
        public int TimeOrigemId { get; set; }
        public int TImeDestinoId { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public string Data { get; set; }
    }
}