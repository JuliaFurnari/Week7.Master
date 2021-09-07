using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week7.Master.Core.Entities;
using Week7.Master.Core.InterfaceRepositories;

namespace Week7.Master.RepositoryMock
{
    public class RepositoryCorsiMock : IRepositoryCorsi
    {
        //Dati finti
        public static List<Corso> Corsi = new List<Corso>()
        {
            new Corso{CorsoCodice="C-01", Nome="Pre-Academy I", Descrizione="Corso base c# livello1"},
            new Corso{CorsoCodice="C-02", Nome="Academy I", Descrizione="Corso base c# livello2"}
        };


        public Corso Add(Corso item)
        {
            Corsi.Add(item);
            return item;
        }

        public bool Delete(Corso item)
        {
            Corsi.Remove(item);
            return true;
        }

        public List<Corso> GetAll()
        {
            return Corsi;
        }

        public Corso GetByCode(string code)
        {
            return Corsi.Find(c => c.CorsoCodice == code);
            //return Corsi.FirstOrDefault(c => c.CorsoCodice == code);
        }

        public Corso Update(Corso item)
        {
            var old = Corsi.FirstOrDefault(c => c.CorsoCodice == item.CorsoCodice);
            old.Nome = item.Nome;
            old.Descrizione = item.Descrizione;
            return item;
        }
    }
}
