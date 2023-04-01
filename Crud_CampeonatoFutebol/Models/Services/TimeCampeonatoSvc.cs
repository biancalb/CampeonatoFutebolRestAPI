using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Crud_CampeonatoFutebol.Models;

namespace Crud_CampeonatoFutebol.Models.Services
{
    public class TimeCampeonatoSvc : CampeonatoFutebolEntities
    {
        public CampeonatoFutebolEntities db = new CampeonatoFutebolEntities();
        public TimeCampeonato Find(long id)
        {
            return db.TimeCampeonato.Find(id);
        }

        public List<TimeCampeonato> Find(string nome)
        {
            var query = db.TimeCampeonato as IQueryable<TimeCampeonato>;
            //if (!string.IsNullOrEmpty(nome)) query = query.Where(ac => ac.Nome == nome);
            return query.ToList();
        }

        public List<TimeCampeonato> List()
        {
            return db.TimeCampeonato.ToList();
        }

        public int Add(TimeCampeonato item)
        {
            item.DataAlteracao = DateTime.Now;
            db.TimeCampeonato.Add(item);
            return db.SaveChanges();
        }

        public int Update(TimeCampeonato item)
        {
            //Validações
            var TimeCampeonato = Find(item.TimeCampeonatoId);
            if (TimeCampeonato != null && TimeCampeonato.TimeCampeonatoId != (int)item.TimeCampeonatoId)
                throw new ApplicationException("TimeCampeonato já cadastrado");

            item.DataAlteracao = DateTime.Now;
            db.Entry(TimeCampeonato).CurrentValues.SetValues(item);
            return db.SaveChanges();
        }

        public int Remove(long id)
        {
            TimeCampeonato TimeCampeonato = db.TimeCampeonato.Find(id);
            db.TimeCampeonato.Remove(TimeCampeonato);
            return db.SaveChanges();
        }
    }
}
