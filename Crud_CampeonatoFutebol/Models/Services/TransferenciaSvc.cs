using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Crud_CampeonatoFutebol.Models;

namespace Crud_CampeonatoFutebol.Models.Services
{
    public class TransferenciaSvc : CampeonatoFutebolEntities
    {
        public CampeonatoFutebolEntities db = new CampeonatoFutebolEntities();
        public Transferencias Find(long id)
        {
            return db.Transferencias.Find(id);
        }

        public List<Transferencias> Find(string nome)
        {
            var query = db.Transferencias as IQueryable<Transferencias>;
            //if (!string.IsNullOrEmpty(nome)) query = query.Where(ac => ac.Nome == nome);
            return query.ToList();
        }

        public List<Transferencias> List()
        {
            return db.Transferencias.ToList();
        }       

        public int Add(Transferencias item)
        {
            item.DataAlteracao = DateTime.Now;
            db.Transferencias.Add(item);
            return db.SaveChanges();
        }

        public int Update(Transferencias item)
        {
            //Validações
            var Transferencia = Find(item.TransferenciasId);
            if (Transferencia != null && Transferencia.TransferenciasId != (int)item.TransferenciasId)
                throw new ApplicationException("Transferencia já cadastrada");

            item.DataAlteracao = DateTime.Now;
            db.Entry(Transferencia).CurrentValues.SetValues(item);
            return db.SaveChanges();
        }

        public int Remove(long id)
        {
            Transferencias Transferencia = db.Transferencias.Find(id);
            db.Transferencias.Remove(Transferencia);
            return db.SaveChanges();
        }
    }
}
