using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Crud_CampeonatoFutebol.Models;

namespace Crud_CampeonatoFutebol.Models.Services
{
    public class CampeonatoSvc : CampeonatoFutebolEntities
    {
        public CampeonatoFutebolEntities db = new CampeonatoFutebolEntities();
        public Campeonato Find(long id)
        {
            return db.Campeonato.Find(id);
        }

        public List<Campeonato> Find(string nome)
        {
            var query = db.Campeonato as IQueryable<Campeonato>;
            if (!string.IsNullOrEmpty(nome)) query = query.Where(ac => ac.Nome == nome);
            return query.ToList();
        }

        public List<Campeonato> List()
        {
            return db.Campeonato.ToList();
        }       

        public int Add(Campeonato item)
        {
            item.DataAlteracao = DateTime.Now;
            db.Campeonato.Add(item);
            return db.SaveChanges();
        }

        public int Update(Campeonato item)
        {
            //Validações
            var campeonato = Find(item.CampeonatoId);
            if (campeonato != null && campeonato.CampeonatoId != (int)item.CampeonatoId)
                throw new ApplicationException($"{nameof(Campeonato)} já cadastrado");

            item.DataAlteracao = DateTime.Now;
            db.Entry(campeonato).State = EntityState.Modified;
            db.Entry(campeonato).CurrentValues.SetValues(item);
            return db.SaveChanges();
        }

        public int Remove(long id)
        {
            Campeonato Campeonato = db.Campeonato.Find(id);
            db.Campeonato.Remove(Campeonato);
            return db.SaveChanges();
        }
    }
}
