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
    public class TimeSvc : CampeonatoFutebolEntities
    {
        public CampeonatoFutebolEntities db = new CampeonatoFutebolEntities();
        public Time Find(long id)
        {
            return db.Time.Find(id);
        }

        public List<Time> Find(string nome)
        {
            var query = db.Time as IQueryable<Time>;
            if (!string.IsNullOrEmpty(nome)) query = query.Where(ac => ac.Nome == nome);
            return query.ToList();
        }

        public List<Time> List()
        {
            return db.Time.ToList();
        }

        public int Add(Time item)
        {
            item.DataAlteracao = DateTime.Now;
            db.Time.Add(item);
            return db.SaveChanges();
        }

        public int Update(Time item)
        {
            //Validações
            var Time = Find(item.Nome)?.FirstOrDefault();
            if (Time != null && Time.TimeId != (int)item.TimeId)
                throw new ApplicationException("Time já cadastrado");

            item.DataAlteracao = DateTime.Now;
            db.Entry(Time).State = EntityState.Modified;
            db.Entry(Time).CurrentValues.SetValues(item);
            return db.SaveChanges();
        }

        public int Remove(long id)
        {
            Time Time = db.Time.Find(id);
            db.Time.Remove(Time);
            return db.SaveChanges();
        }
    }
}
