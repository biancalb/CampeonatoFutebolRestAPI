using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud_CampeonatoFutebol.Models.Resources
{
    public class EventoPartidaModel
    {
        public int PartidaId { get; set; }
        public int TipoEventoId { get; set; }
        public string InstanteEvento { get; set; }
        public Nullable<int> JogadorId { get; set; }
        public System.DateTime DataAlteracao { get; set; }


    }
}