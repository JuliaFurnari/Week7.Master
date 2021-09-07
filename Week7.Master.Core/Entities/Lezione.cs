using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week7.Master.Core.Entities
{
    public class Lezione
    {
        public int LezioneID { get; set; }
        public DateTime DataOraInizio { get; set; }
        public int Durata { get; set; }
        public string Aula { get; set; }

        //FK verso Docente
        public int DocenteID { get; set; }
        public Docente Docente { get; set; }


        //Fk verso Corso
        public string CorsoCodice { get; set; }
        public Corso Corso { get; set; }


        public override string ToString()
        {
            return $"Lezione: {LezioneID}\tData: {DataOraInizio}\tAula: {Aula}\tDurata(in gg): {Durata}\nDocente: {Docente.ToString()}\nCorso: {Corso.ToString()}\n\n";
        }
    }
}
