//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Crud_CampeonatoFutebol.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventoPartida
    {
        public int EventoPartidaId { get; set; }
        public int PartidaId { get; set; }
        public int TipoEventoId { get; set; }
        public System.DateTime InstanteEvento { get; set; }
        public Nullable<int> JogadorId { get; set; }
        public System.DateTime DataAlteracao { get; set; }
    
        public virtual Jogador Jogador { get; set; }
        public virtual TipoEvento TipoEvento { get; set; }
        public virtual Partida Partida { get; set; }
    }
}
