using System;

namespace Crud_CampeonatoFutebol.Models.Resources
{
    public class PartidaModel
    {
        public int PartidaId { get; set; }
        public int TimeCampeonato1Id { get; set; }
        public int TimeCampeonato2Id { get; set; }
        public string DataInicio { get; set; }
        public Nullable<int> ResultadoPartidaId { get; set; }
        public int EstadioId { get; set; }

    }
}