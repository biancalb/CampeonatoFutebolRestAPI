using System;
using System.Collections.Generic;
using System.Linq;

namespace Crud_CampeonatoFutebol.Models.Services
{
    public class PosicaoJogadorSvc : CampeonatoFutebolEntities
    {
        public CampeonatoFutebolEntities db = new CampeonatoFutebolEntities();
        public PosicaoJogador Find(long id)
        {
            return db.PosicaoJogador.Find(id);
        }

        public List<PosicaoJogador> List()
        {
            return db.PosicaoJogador.ToList();
        }
    }
}
