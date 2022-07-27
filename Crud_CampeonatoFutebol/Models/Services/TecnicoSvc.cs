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
    public class TecnicoSvc : CampeonatoFutebolEntities
    {
        public CampeonatoFutebolEntities db = new CampeonatoFutebolEntities();
        public Tecnico Find(long id)
        {
             return db.Tecnico.Find(id);
        }

        public List<Tecnico> Find(string nome)
        {
            var query = db.Tecnico as IQueryable<Tecnico>;
            if (!string.IsNullOrEmpty(nome)) query = query.Where(ac => ac.Nome == nome);
            return query.ToList();          
        }

        public List<Tecnico> List()
        {
            return db.Tecnico.ToList();
        }       

        public int Add(Tecnico item)
        {
            item.DataAlteracao = DateTime.Now;
            db.Tecnico.Add(item);
            return db.SaveChanges();
        }

        public int Update(Tecnico item)
        {
            //Validações
            var tecnico = Find(item.Nome)?.FirstOrDefault();
            if (tecnico != null && tecnico.TecnicoId != (int)item.TecnicoId)
                throw new ApplicationException($"{nameof(Tecnico)} já cadastrado");

            item.DataAlteracao = DateTime.Now;
            db.Entry(tecnico).State = EntityState.Modified;
            db.Entry(tecnico).CurrentValues.SetValues(item);
            return db.SaveChanges();
        }

        public int Remove(long id)
        {
            Tecnico Tecnico = db.Tecnico.Find(id);
            db.Tecnico.Remove(Tecnico);
            return db.SaveChanges();
        }
    }
}
