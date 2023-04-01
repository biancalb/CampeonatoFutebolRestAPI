using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Crud_CampeonatoFutebol.Models;

namespace Crud_CampeonatoFutebol.Models.Services
{
    public class EventoPartidaSvc : CampeonatoFutebolEntities
    {
        public CampeonatoFutebolEntities db = new CampeonatoFutebolEntities();
        public EventoPartida Find(long id)
        {
            return db.EventoPartida.Find(id);
        }

        public List<EventoPartida> Find(string nome)
        {
            var query = db.EventoPartida as IQueryable<EventoPartida>;
            //if (!string.IsNullOrEmpty(nome)) query = query.Where(ac => ac.Nome == nome);
            return query.ToList();
        }

        public List<EventoPartida> List()
        {
            return db.EventoPartida.ToList();
        } 

        public int Add(EventoPartida item)
        {
            item.DataAlteracao = DateTime.Now;
            db.EventoPartida.Add(item);
            return db.SaveChanges();
        }

        public int Update(EventoPartida item)
        {
            //Validações
            var EventoPartida = Find(item.EventoPartidaId);
            if (EventoPartida != null && EventoPartida.EventoPartidaId != (int)item.EventoPartidaId)
                throw new ApplicationException("EventoPartida já cadastrado");

            item.DataAlteracao = DateTime.Now;
            db.Entry(EventoPartida).CurrentValues.SetValues(item);
            return db.SaveChanges();
        }

        public int Remove(long id)
        {
            EventoPartida EventoPartida = db.EventoPartida.Find(id);
            db.EventoPartida.Remove(EventoPartida);
            return db.SaveChanges();
        }
    }
}
