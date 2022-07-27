using System;
using System.Collections.Generic;
using System.Linq;

namespace Crud_CampeonatoFutebol.Models.Services
{
    public class ResultadoPartidaSvc : CampeonatoFutebolEntities
    {
        public CampeonatoFutebolEntities db = new CampeonatoFutebolEntities();
        public ResultadoPartida Find(long id)
        {
            return db.ResultadoPartida.Find(id);
        }

        public List<ResultadoPartida> List()
        {
            return db.ResultadoPartida.ToList();
        }       
    }
}
