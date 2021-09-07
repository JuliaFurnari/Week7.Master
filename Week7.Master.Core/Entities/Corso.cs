using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week7.Master.Core.Entities
{
    public class Corso
    {
        public string CorsoCodice { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }

        public List<Studente> Studenti { get; set; } = new List<Studente>();

        public List<Lezione> Lezioni { get; set; } = new List<Lezione>();



        public override string ToString()
        {
            return $"{CorsoCodice}\t{Nome}\t{Descrizione}";
        }
    }
}
