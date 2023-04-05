using System;
using System.Collections.Generic;
using System.Linq;

namespace Crud_CampeonatoFutebol.Models.Services
{
    public class EstadoSvc : CampeonatoFutebolEntities
    {
        public CampeonatoFutebolEntities db = new CampeonatoFutebolEntities();
        public Estado Find(long id)
        {
            return db.Estado.Find(id);
        }

        public List<Estado> List()
        {
            return db.Estado.ToList();
        }
    }
}
