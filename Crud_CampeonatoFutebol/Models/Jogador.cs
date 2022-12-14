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
    
    public partial class Jogador
    {
        public Jogador()
        {
            this.Transferencias = new HashSet<Transferencias>();
            this.EventoPartida = new HashSet<EventoPartida>();
        }
    
        public int JogadorId { get; set; }
        public string Nome { get; set; }
        public System.DateTime DataNascimento { get; set; }
        public int TimeAtualID { get; set; }
        public Nullable<decimal> Salario { get; set; }
        public Nullable<int> MesesContrato { get; set; }
        public Nullable<int> PosicaoJogadorId { get; set; }
        public System.DateTime DataAlteracao { get; set; }
    
        public virtual PosicaoJogador PosicaoJogador { get; set; }
        public virtual Time Time { get; set; }
        public virtual ICollection<Transferencias> Transferencias { get; set; }
        public virtual ICollection<EventoPartida> EventoPartida { get; set; }
    }
}
