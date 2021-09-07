using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week7.Master.Core.Entities;
using Week7.Master.Core.InterfaceRepositories;

namespace Week7.Master.Core.BusinessLayer
{
    public class MainBusinessLayer : IBusinessLayer
    {
        //Dichiaro quali sono i repository che ho a disposizione.
        private readonly IRepositoryCorsi corsiRepo;
        private readonly IRepositoryDocenti docentiRepo;
        private readonly IRepositoryStudenti studentiRepo;
        private readonly IRepositoryLezioni lezioniRepo;

        public MainBusinessLayer(IRepositoryCorsi corsi, IRepositoryDocenti docenti, IRepositoryLezioni lezioni, IRepositoryStudenti studenti)
        {
            corsiRepo = corsi;
            docentiRepo = docenti;
            lezioniRepo = lezioni;
            studentiRepo = studenti;
        }

        

        #region Funzionalità Corsi
        public List<Corso> GetAllCorsi()
        {
            return corsiRepo.GetAll();
        }

        public string InserisciNuovoCorso(Corso newCorso)
        {
            //controllo input
            //non deve esistere un altro corso con lo stesso codice
            Corso corsoEsistente=corsiRepo.GetByCode(newCorso.CorsoCodice);
            if (corsoEsistente != null)
            {
                return "Errore: Codice corso già presente";
            }
            corsiRepo.Add(newCorso);
            return "Corso aggiunto correttamente";
        }

        public string ModificaCorso(string codiceCorsoDaModificare, string nuovoNome, string nuovaDescrizione)
        {
            //controllo i dati
            Corso corsoEsistente = corsiRepo.GetByCode(codiceCorsoDaModificare);
            if (corsoEsistente == null)
            {
                return "Errore: Codice errato.";
            }
            corsoEsistente.Nome = nuovoNome;
            corsoEsistente.Descrizione = nuovaDescrizione;
            corsiRepo.Update(corsoEsistente);
            return "Il corso è stato modificato con successo";
        }

        public string EliminaCorso(string codiceCorsoDaEliminare)
        {
            Corso corsoEsistente = corsiRepo.GetByCode(codiceCorsoDaEliminare);
            if (corsoEsistente == null)
            {
                return "Errore: Codice errato.";
            }

            //TODO:non deve essere possibile cancellare un corso che ha almeno una lezione associata
            //nè un corso che ha almeno uno studente iscritto.
            else
            {
                if (corsoEsistente.Lezioni.ToList().Count == 0 && corsoEsistente.Studenti.ToList().Count == 0)
                {
                    corsiRepo.Delete(corsoEsistente);
                    return "Corso eliminato correttamente";
                }
                return "Errore. Non è possibile cancellare il corso.";
            }

        }

        #endregion

        #region funzionalità Studenti
        public List<Studente> GetAllStudenti()
        {
            return studentiRepo.GetAll();
        }

        public string InserisciNuovoStudente(Studente nuovoStudente)
        {
            //controllo input
            Corso corsoEsistente = corsiRepo.GetByCode(nuovoStudente.CorsoCodice);
            if (corsoEsistente == null)
            {
                return "Codice corso errato";
            }
            studentiRepo.Add(nuovoStudente);
            return "studente inserito correttamente";
        }

        public object ModificaStudente(int id, string nuovaEmail)
        {
            Studente studenteEsistente = studentiRepo.GetById(id);
            if (studenteEsistente == null)
            {
                return "Errore: Codice errato.";
            }
            studenteEsistente.Email = nuovaEmail;
            studentiRepo.Update(studenteEsistente);
            return "Lo studente è stato modificato con successo";
        }

        public string EliminaStudente(int sceltaId)
        {
            Studente studenteEsistente = studentiRepo.GetById(sceltaId);
            if (studenteEsistente == null)
            {
                return "Errore: Codice errato.";
            }

            studentiRepo.Delete(studenteEsistente);
            return "Studente eliminato correttamente";
        }

        public List<Studente> GetStudentiByCorso(string code)
        {
            Corso corsoEsistente = corsiRepo.GetByCode(code);
            if (corsoEsistente == null)
            {
                Console.WriteLine( "Errore: Codice errato.");
                return null;
            }
            return studentiRepo.GetStudentiByCodiceCorso(code);
        }

        public List<Docente> GetAllDocenti()
        {
            return docentiRepo.GetAll();
        }

        public object InserisciNuovoDocente(Docente nuovoDocente)
        {
            Docente docenteEsistente = docentiRepo.GetAll().FirstOrDefault(d => d.Nome == nuovoDocente.Nome && d.Cognome==nuovoDocente.Cognome && d.Email==nuovoDocente.Email);
            if (docenteEsistente == null)
            {
                docentiRepo.Add(nuovoDocente);
                return "docente inserito correttamente";
            }
            else return "docente presente";
        }

        public object ModificaDocente(int sceltaId, string nuovaEmail, string nuovoTelefono)
        {
            Docente docenteEsistente = docentiRepo.GetById(sceltaId);
            if (docenteEsistente == null)
            {
                return "Errore: Codice errato.";
            }
            docenteEsistente.Email = nuovaEmail;
            docenteEsistente.Telefono = nuovoTelefono;
            docentiRepo.Update(docenteEsistente);
            return "Il docente è stato modificato con successo";
        }

        public string EliminaDocente(int sceltaId)
        {
            Docente docenteEsistente = docentiRepo.GetById(sceltaId);
            if (docenteEsistente == null)
            {
                return "Errore: Codice errato.";
            }
            else
            {
                if (docenteEsistente.Lezioni.ToList().Count == 0)
                {
                    docentiRepo.Delete(docenteEsistente);
                    return "Docente eliminato correttamente";
                }
                return "Errore. Non è possibile cancellare il docente.";
            }
            
        }

        public List<Lezione> GetAllLezioni()
        {
            return lezioniRepo.GetAll();
        }

        public object InserisciNuovaLezione(Lezione nuovaLezione)
        {
            //controllo input
            Corso corsoEsistente = corsiRepo.GetByCode(nuovaLezione.CorsoCodice);
            if (corsoEsistente == null)
            {
                return "Codice corso errato";
            }
            else
            {
                Docente docenteEsistente = docentiRepo.GetById(nuovaLezione.DocenteID);
                if (docenteEsistente == null)
                {
                    return "Id docente errato";
                }
                lezioniRepo.Add(nuovaLezione);
                return "Lezione inserita correttamente";
            }
        }

        public object ModificaLezione(int sceltaId, string nuovaAula)
        {
            Lezione lezioneEsistente = lezioniRepo.GetById(sceltaId);
            if (lezioneEsistente == null)
            {
                return "Errore: Codice errato.";
            }
            lezioneEsistente.Aula= nuovaAula;
            lezioniRepo.Update(lezioneEsistente);
            return "La lezione è stata modificata con successo";
        }

        public string EliminaLezione(int sceltaId)
        {
            Lezione lezioneEsistente = lezioniRepo.GetById(sceltaId);
            if (lezioneEsistente == null)
            {
                return "Errore: Codice errato.";
            }

            lezioniRepo.Delete(lezioneEsistente);
            return "Lezione eliminata correttamente";
        }

        public List<Lezione> GetLezioniByCorso(string codiceCorso)
        {
            Corso corsoEsistente = corsiRepo.GetByCode(codiceCorso);
            if (corsoEsistente == null)
            {
                Console.WriteLine("Errore: Codice errato.");
                return null;
            }
            return lezioniRepo.GetLezioniByCodiceCorso(codiceCorso);
        }
        #endregion
    }
}
