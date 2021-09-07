using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week7.Master.Core.Entities;

namespace Week7.Master.Core.InterfaceRepositories
{
    public interface IRepositoryLezioni: IRepository<Lezione>
    {
        public Lezione GetById(int id);
        List<Lezione> GetLezioniByCodiceCorso(string codiceCorso);
    }
}
