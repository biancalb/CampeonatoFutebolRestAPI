using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Crud_CampeonatoFutebol.Models;

namespace Crud_CampeonatoFutebol.Models.Services
{
    public class PartidaSvc : CampeonatoFutebolEntities
    {
        public CampeonatoFutebolEntities db = new CampeonatoFutebolEntities();
        public Partida Find(long id)
        {
            return db.Partida.Find(id);
        }

        public List<Partida> Find(string nome)
        {
            var query = db.Partida as IQueryable<Partida>;
            //if (!string.IsNullOrEmpty(nome)) query = query.Where(ac => ac.Nome == nome);
            return query.ToList();
        }

        public List<Partida> List()
        {
            return db.Partida.ToList();
        }

        public int Add(Partida item)
        {
            item.DataAlteracao = DateTime.Now;
            db.Partida.Add(item);
            return db.SaveChanges();
        }

        public int Update(Partida item)
        {
            //Validações
            var Partida = Find(item.PartidaId);
            if (Partida != null && Partida.PartidaId != (int)item.PartidaId)
                throw new ApplicationException("Partida já cadastrado");

            item.DataAlteracao = DateTime.Now;
            db.Entry(Partida).CurrentValues.SetValues(item);
            return db.SaveChanges();
        }

        public int Remove(long id)
        {
            Partida Partida = db.Partida.Find(id);
            db.Partida.Remove(Partida);
            return db.SaveChanges();
        }
    }
}
