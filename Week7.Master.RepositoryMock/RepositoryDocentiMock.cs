using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week7.Master.Core.Entities;
using Week7.Master.Core.InterfaceRepositories;

namespace Week7.Master.RepositoryMock
{
    public class RepositoryDocentiMock : IRepositoryDocenti
    {
        public static List<Docente> Docenti = new List<Docente>()
        {
            new Docente{ID=1, Nome="Mario", Cognome="Rossi", Email="mario@mail.it", Telefono="3331234567"},
            new Docente{ID=2, Nome="Giuseppe", Cognome="Bianchi", Email="giuseppe@mail.it", Telefono="3331434567"}
        };

        public Docente Add(Docente item)
        {
            if (Docenti.Count == 0)
            {
                item.ID = 1;
            }
            else
            {
                item.ID = Docenti.Max(d => d.ID) + 1;
            }

            Docenti.Add(item);
            return item;
        }

        public bool Delete(Docente item)
        {
            Docenti.Remove(item);
            return true;
        }

        public List<Docente> GetAll()
        {
            return Docenti;
        }

        public Docente GetById(int id)
        {
            return Docenti.Find(d => d.ID == id);
        }

        public Docente Update(Docente item)
        {
            var old = Docenti.FirstOrDefault(d => d.ID == item.ID);
            old.Email = item.Email;
            return item;
        }
    }
}
