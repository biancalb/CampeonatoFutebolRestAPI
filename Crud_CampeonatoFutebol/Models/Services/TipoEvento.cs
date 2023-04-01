using System;
using System.Collections.Generic;
using System.Linq;

namespace Crud_CampeonatoFutebol.Models.Services
{
    public class TipoEventoSvc : CampeonatoFutebolEntities
    {
        public CampeonatoFutebolEntities db = new CampeonatoFutebolEntities();
        public TipoEvento Find(long id)
        {
            return db.TipoEvento.Find(id);
        }

        public List<TipoEvento> List()
        {
            return db.TipoEvento.ToList();
        }
    }
}
