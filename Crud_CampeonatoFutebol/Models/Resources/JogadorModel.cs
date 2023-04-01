using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud_CampeonatoFutebol.Models.Resources
{
    public class JogadorModel
    {
        public int JogadorId { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public int TimeAtualID { get; set; }
        public decimal Salario { get; set; }
        public int MesesContrato { get; set; }
        public int PosicaoJogadorId { get; set; }
    }
}