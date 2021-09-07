using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week7.Master.Core.InterfaceRepositories
{
    public interface IRepository<T>
    {
        //operazioni CRUD
        public List<T> GetAll();
        public T Add(T item);
        public T Update(T item);
        public bool Delete(T item);

    }
}
