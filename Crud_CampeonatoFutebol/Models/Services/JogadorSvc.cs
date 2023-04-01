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
    public class JogadorSvc : CampeonatoFutebolEntities
    {
        public CampeonatoFutebolEntities db = new CampeonatoFutebolEntities();
        public Jogador Find(long id)
        {
            return db.Jogador.Find(id);
        }

        public List<Jogador> Find(string nome)
        {
            var query = db.Jogador as IQueryable<Jogador>;
            if (!string.IsNullOrEmpty(nome)) query = query.Where(ac => ac.Nome == nome);
            return query.ToList();
        }

        public List<Jogador> List()
        {
            return db.Jogador.ToList();
        }

        public int Add(Jogador item)
        {
            item.DataAlteracao = DateTime.Now;
            db.Jogador.Add(item);
            return db.SaveChanges();
        }

        public int Update(Jogador item)
        {
            //Validações
            var jogador = Find(item.JogadorId);
            if (jogador != null && jogador.JogadorId != (int)item.JogadorId)
                throw new ApplicationException($"{nameof(Jogador)} já cadastrado");

            item.DataAlteracao = DateTime.Now;
            db.Entry(jogador).State = EntityState.Modified;
            db.Entry(jogador).CurrentValues.SetValues(item);
            return db.SaveChanges();
        }

        public int Remove(long id)
        {
            Jogador jogador = db.Jogador.Find(id);
            db.Jogador.Remove(jogador);
            return db.SaveChanges();
        }
    }
}
