using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week7.Master.Core.Entities;
using Week7.Master.Core.InterfaceRepositories;

namespace Week7.Master.RepositoryMock
{
   public class RepositoryLezioniMock : IRepositoryLezioni
    {
        public static List<Lezione> Lezioni = new List<Lezione>();


        public Lezione Add(Lezione item)
        {
            if (Lezioni.Count == 0)
            {
                item.LezioneID = 1;
            }
            else
            {
                item.LezioneID = Lezioni.Max(s => s.LezioneID) + 1;
            }
            var corso = RepositoryCorsiMock.Corsi.FirstOrDefault(c => c.CorsoCodice == item.CorsoCodice);
            item.Corso = corso;
            corso.Lezioni.Add(item);

            var docente = RepositoryDocentiMock.Docenti.FirstOrDefault(d => d.ID == item.DocenteID);
            item.Docente = docente;
            docente.Lezioni.Add(item);

            Lezioni.Add(item);
            return item;

        }

        public bool Delete(Lezione item)
        {
            Lezioni.Remove(item);
            return true;
        }

        public List<Lezione> GetAll()
        {
            return Lezioni;
        }

        public Lezione GetById(int id)
        {
            return Lezioni.Find(l => l.LezioneID == id);
        }

        public List<Lezione> GetLezioniByCodiceCorso(string codiceCorso)
        {
            return Lezioni.Where(l => l.Corso.CorsoCodice == codiceCorso).ToList();
        }

        public Lezione Update(Lezione item)
        {
            var old = Lezioni.FirstOrDefault(l => l.LezioneID == item.LezioneID);
            old.Aula = item.Aula;
            return item;
        }
    }
}
