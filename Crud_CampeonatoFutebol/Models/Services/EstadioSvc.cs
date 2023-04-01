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
    public class EstadioSvc : CampeonatoFutebolEntities
    {
        public CampeonatoFutebolEntities db = new CampeonatoFutebolEntities();
        public Estadio Find(long id)
        {
            return db.Estadio.Find(id);
        }

        public List<Estadio> Find(string nome)
        {
            var query = db.Estadio as IQueryable<Estadio>;
            if (!string.IsNullOrEmpty(nome)) query = query.Where(ac => ac.Nome == nome);
            return query.ToList();
        }

        public List<Estadio> List()
        {
            return db.Estadio.ToList();
        }      

        public int Add(Estadio item)
        {
            item.DataAlteracao = DateTime.Now;
            db.Estadio.Add(item);
            return db.SaveChanges();
        }

        public int Update(Estadio item)
        {
            //Validações
            var Estadio = Find(item.Nome)?.FirstOrDefault();
            if (Estadio != null && Estadio.EstadioId != (int)item.EstadioId)
                throw new ApplicationException("Estadio já cadastrado");

            item.DataAlteracao = DateTime.Now;
            db.Entry(Estadio).State = EntityState.Modified;
            db.Entry(Estadio).CurrentValues.SetValues(item);
            return db.SaveChanges();
        }

        public int Remove(long id)
        {
            Estadio Estadio = db.Estadio.Find(id);
            db.Estadio.Remove(Estadio);
            return db.SaveChanges();
        }
    }
}
