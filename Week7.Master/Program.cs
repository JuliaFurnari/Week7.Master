using System;
using Week7.Master.Core.BusinessLayer;
using Week7.Master.Core.Entities;
using Week7.Master.RepositoryMock;

namespace Week7.Master
{
    class Program
    {
        private static readonly IBusinessLayer bl = new MainBusinessLayer(new RepositoryCorsiMock(), new RepositoryDocentiMock(), new RepositoryLezioniMock(), new RepositoryStudentiMock());
        static void Main(string[] args)
        {
            bool continua = true;
            while (continua)
            {
                int scelta = SchermoMenu();
                continua = AnalizzaScelta(scelta);
            }
        }

        private static int SchermoMenu()
        {
            Console.WriteLine("******************Menu****************");
            //Funzionalità su Corsi
            Console.WriteLine("\nFunzionalità CORSI");
            Console.WriteLine("1. Visualizza Corsi");
            Console.WriteLine("2. Inserisci nuovo Corso");
            Console.WriteLine("3. Modifica Corso");
            Console.WriteLine("4. Elimina Corso");
            //Funzionalità su Docenti
            Console.WriteLine("\nFunzionalità Docenti");
            Console.WriteLine("5. Visualizza Docenti");
            Console.WriteLine("6. Inserisci nuovo Docente");
            Console.WriteLine("7. Modifica Docente");
            Console.WriteLine("8. Elimina Docente");
            //Funzionalità su Lezioni
            Console.WriteLine("\nFunzionalità Lezioni");
            Console.WriteLine("9. Visualizza elenco delle lezioni completo");
            Console.WriteLine("10. Inserimento nuova lezione");
            Console.WriteLine("11. Modifica lezione");//per semplicità solo modifica Aula
            Console.WriteLine("12. Elimina lezione");
            Console.WriteLine("13. Visualizza le Lezioni di un Corso ricercando per Codice del Corso");
            Console.WriteLine("14. Visualizza le Lezioni di un Corso ricercando per Nome del Corso");
            //Funzionalità su Studenti
            Console.WriteLine("\nFunzionalità Studenti");
            Console.WriteLine("15. Visualizza l'elenco completo degli studenti");
            Console.WriteLine("16. Inserimento nuovo Studente");
            Console.WriteLine("17. Modifica Studente");//per semplicità solo email
            Console.WriteLine("18. Elimina Studente");
            Console.WriteLine("19. Visualizza l'elenco degli studenti iscritti ad un corso");

            //Exit
            Console.WriteLine("\n0. Exit");
            Console.WriteLine("********************************************");


            int scelta;
            Console.Write("Inserisci scelta: ");
            while (!int.TryParse(Console.ReadLine(), out scelta) || scelta < 0 || scelta > 19)
            {
                Console.Write("\nScelta errata. Inserisci scelta corretta: ");
            }
            return scelta;

        }
        private static bool AnalizzaScelta(int scelta)
        {
            switch (scelta)
            {
                case 1:
                    VisualizzaCorsi();
                    break;
                case 2:
                    InserisciNuovoCorso();
                    break;
                case 3:
                    ModificaCorso();//solo nome e descrizione
                    break;
                case 4:
                    EliminaCorso(); 
                    break;
                case 5:
                    VisualizzaDocenti();
                    break;
                case 6:
                    InserisciNuovoDocente();
                    break;
                case 7:
                    ModificaDocente();
                    break;
                case 8:
                    EliminaDocente();
                    break;
                case 9:
                    VisualizzaElencoCompletoLezioni();
                    break;
                case 10:
                    InserisciNuovaLezione();
                    break;
                case 11:
                    ModificaLezione();//per semplicità solo modifica Aula
                    break;
                case 12:
                    EliminaLezione();
                    break;
                case 13:
                    VisualizzaLezioniByCorso();
                    break;
                case 14:
                    VisualizzaLezioniByNomeCorso();
                    break;
                case 15:
                    VisualizzaElencoCompletoStudenti();
                    break;
                case 16:
                    InserisciNuovoStudente();
                    break;
                case 17:
                    ModificaStudente();//Solo email
                    break;
                case 18:
                    EliminaStudente();
                    break;
                case 19:
                    VisualizzaStudentiByCorso();
                    break;
                case 0:
                    return false;
            }
            return true;
        }

        
        private static void VisualizzaLezioniByNomeCorso()
        {
            throw new NotImplementedException(); //Simile alla funzione VisualizzaLezioniByCorso
        }

        private static void VisualizzaLezioniByCorso()
        {
            Console.WriteLine("Ecco l'elenco dei corsi disponibili:");
            VisualizzaCorsi();
            Console.WriteLine("Digita l'ID del corso di cui vuoi visualizzare le lezioni:");
            string codiceCorso = Console.ReadLine();
            var lezioniByCorso = bl.GetLezioniByCorso(codiceCorso);
            if (lezioniByCorso.Count == 0)
            {
                Console.WriteLine("Nessuna lezione presente in questo corso.");
            }
            else
            {
                Console.WriteLine($"Ecco l'elenco delle lezioni del corso con codice{codiceCorso}");
                foreach (var item in lezioniByCorso)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void EliminaLezione()
        {
            Console.WriteLine("Ecco l'elenco completo delle lezioni:");
            VisualizzaElencoCompletoLezioni();
            Console.WriteLine("Quale lezione vuoi eliminare? Inserisci il codice Id");
            int sceltaId;
            while (!int.TryParse(Console.ReadLine(), out sceltaId) || sceltaId < 1 || sceltaId > bl.GetAllLezioni().Count)
            {
                Console.WriteLine("Errore.");
            }
            string esito = bl.EliminaLezione(sceltaId);
            Console.WriteLine(esito);
        }

        private static void ModificaLezione()
        {
            Console.WriteLine("Ecco l'elenco completo delle lezioni.");
            VisualizzaElencoCompletoLezioni();
            Console.WriteLine("Quale lezione vuoi modificare? Digita l'ID della lezione.");
            int sceltaId;
            while (!int.TryParse(Console.ReadLine(), out sceltaId) || sceltaId < 1 || sceltaId > bl.GetAllLezioni().Count)
            {
                Console.WriteLine("Errore.");
            }
            Console.WriteLine("Inserisci la nuova aula della lezione.");
            string nuovaAula = Console.ReadLine();

            var esito = bl.ModificaLezione(sceltaId, nuovaAula);
            Console.WriteLine(esito);
        }

        private static void InserisciNuovaLezione()
        {
            //Chiedo le info per creare la nuova lezione
            Console.WriteLine("Inserisci la data e l'ora dell'inizio della lezione. (gg-mm-aaaa hh-mm-ss)");
            DateTime dataEOra;
            while(!DateTime.TryParse(Console.ReadLine(), out dataEOra)|| dataEOra < DateTime.Today)
            {
                Console.WriteLine("Errore");
            }
            Console.WriteLine("Inserisci durata (giorni).");
            int durata;
            while (!int.TryParse(Console.ReadLine(), out durata) || durata < 0)
            {
                Console.WriteLine("Errore");
            }
            Console.WriteLine("Inserisci l'aula.");
            string aula = Console.ReadLine();
            VisualizzaCorsi();
            Console.WriteLine("Inserisci codice corso a cui appartiene la lezione.");
            string codiceCorso = Console.ReadLine();
            VisualizzaDocenti();
            Console.WriteLine("Inserisci l'id del docente del corso.");
            int idDocente;
            while (!int.TryParse(Console.ReadLine(), out idDocente) || idDocente < 1 || idDocente>bl.GetAllDocenti().Count)
            {
                Console.WriteLine("Errore");
            }

            //lo creo
            Lezione nuovaLezione = new Lezione();
            nuovaLezione.DataOraInizio = dataEOra;
            nuovaLezione.Durata = durata;
            nuovaLezione.Aula = aula;
            nuovaLezione.DocenteID = idDocente;
            nuovaLezione.CorsoCodice = codiceCorso;
           

            //lo passo al bl
            var esito = bl.InserisciNuovaLezione(nuovaLezione);
            Console.WriteLine(esito);

        }

        private static void VisualizzaElencoCompletoLezioni()
        {
            var lezioni = bl.GetAllLezioni();
            if (lezioni.Count == 0)
            {
                Console.WriteLine("Nessuna lezione presente");
            }
            else
            {
                foreach (var item in lezioni)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void EliminaDocente()
        {
            Console.WriteLine("Ecco l'elenco completo dei docenti:");
            VisualizzaDocenti();
            Console.WriteLine("Quale docente vuoi eliminare? Inserisci il codice Id");
            int sceltaId;
            while (!int.TryParse(Console.ReadLine(), out sceltaId) || sceltaId < 1 || sceltaId > bl.GetAllDocenti().Count)
            {
                Console.WriteLine("Errore.");
            }
            string esito = bl.EliminaDocente(sceltaId);
            Console.WriteLine(esito);
        }

        private static void ModificaDocente()
        {
            Console.WriteLine("Ecco l'elenco completo dei docenti.");
            VisualizzaDocenti();
            Console.WriteLine("Quale docente vuoi modificare? Digita l'ID del docente.");
            int sceltaId;
            while (!int.TryParse(Console.ReadLine(), out sceltaId) || sceltaId < 1 || sceltaId > bl.GetAllDocenti().Count)
            {
                Console.WriteLine("Errore.");
            }
            Console.WriteLine("Inserisci la nuova email del docente.");
            string nuovaEmail = Console.ReadLine();
            Console.WriteLine("Inserisci il nuovo numero di telefono del docente.");
            string nuovoTelefono = Console.ReadLine();

            var esito = bl.ModificaDocente(sceltaId, nuovaEmail, nuovoTelefono);
            Console.WriteLine(esito);
        }

        private static void InserisciNuovoDocente()
        {
            //Chiedo le info per creare il nuovo docente
            Console.WriteLine("Inserisci nome");
            string nome = Console.ReadLine();
            Console.WriteLine("Inserisci cognome");
            string cognome = Console.ReadLine();
            Console.WriteLine("Inserisci email");
            string email = Console.ReadLine();
            Console.WriteLine("Inserisci il numero di telefono");
            string telefono = Console.ReadLine();

            //lo creo
            Docente nuovoDocente = new Docente();
            nuovoDocente.Nome = nome;
            nuovoDocente.Cognome = cognome;
            nuovoDocente.Email = email;
            nuovoDocente.Telefono = telefono;
           
            //lo passo al bl
            var esito = bl.InserisciNuovoDocente(nuovoDocente);
            Console.WriteLine(esito);

        }

        private static void VisualizzaDocenti()
        {
            var docenti = bl.GetAllDocenti();
            if (docenti.Count == 0)
            {
                Console.WriteLine("Nessun docente presente");
            }
            else
            {
                foreach (var item in docenti)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void VisualizzaStudentiByCorso()
        {
            Console.WriteLine("Ecco l'elenco dei corsi disponibili:");
            VisualizzaCorsi();
            Console.WriteLine("Digita l'ID del corso di cui vuoi visualizzare gli studenti:");
            string codiceCorso = Console.ReadLine();
            var studentiByCorso = bl.GetStudentiByCorso(codiceCorso);
            if (studentiByCorso.Count == 0)
            {
                Console.WriteLine("Nessuno studente presente in questo corso.");
            }
            else
            {
                Console.WriteLine($"Ecco l'elenco degli studenti iscritti al corso con codice{codiceCorso}");
                foreach (var item in studentiByCorso)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void EliminaStudente()
        {
            Console.WriteLine("Ecco l'elenco completo degli studenti:");
            VisualizzaElencoCompletoStudenti();
            Console.WriteLine("Quale studente vuoi eliminare? Inserisci il codice Id");
            int sceltaId;
            while (!int.TryParse(Console.ReadLine(), out sceltaId) || sceltaId < 1 || sceltaId > bl.GetAllStudenti().Count)
            {
                Console.WriteLine("Errore.");
            }
            string esito = bl.EliminaStudente(sceltaId);
            Console.WriteLine(esito);
        }

        private static void ModificaStudente()
        {
            Console.WriteLine("Ecco l'elenco completo degli studenti.");
            VisualizzaElencoCompletoStudenti();
            Console.WriteLine("Quale studente vuoi modificare? Digita l'ID dello studente.");
            int sceltaId;
            while(!int.TryParse(Console.ReadLine(), out sceltaId) || sceltaId<1 || sceltaId>bl.GetAllStudenti().Count)
            {
                Console.WriteLine("Errore.");
            }
            Console.WriteLine("Inserisci la nuova e-mail dello studente.");
            string nuovaEmail = Console.ReadLine();
          
            var esito = bl.ModificaStudente(sceltaId, nuovaEmail);
            Console.WriteLine(esito);
        }

        private static void InserisciNuovoStudente()
        {
            //Chiedo le info per creare il nuovo studente
            Console.WriteLine("Inserisci nome");
            string nome = Console.ReadLine();
            Console.WriteLine("Inserisci cognome");
            string cognome = Console.ReadLine();
            Console.WriteLine("Inserisci email");
            string email = Console.ReadLine();
            Console.WriteLine("Inserisci dat di nascita (formato gg-mm-aaaa)");
            DateTime dataNascita = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Inserisci titolo studio");
            string titoloStudio = Console.ReadLine();
            VisualizzaCorsi();
            Console.WriteLine("Inserisci codice corso a cui è iscritto");
            string codiceCorso = Console.ReadLine();

            //lo creo
            Studente nuovoStudente = new Studente();
            nuovoStudente.Nome = nome;
            nuovoStudente.Cognome = cognome;
            nuovoStudente.DataNascita = dataNascita;
            nuovoStudente.Email = email;
            nuovoStudente.TitoloStudio = titoloStudio;
            nuovoStudente.CorsoCodice = codiceCorso;

            //lo passo al bl
            var esito=bl.InserisciNuovoStudente(nuovoStudente);
            Console.WriteLine(esito);



        }

        private static void VisualizzaElencoCompletoStudenti()
        {
            var studenti = bl.GetAllStudenti();
            if (studenti.Count == 0)
            {
                Console.WriteLine("Nessuno Studente presente");
            }
            else
            {
                foreach (var item in studenti)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void EliminaCorso()
        {
            Console.WriteLine("Ecco l'elenco dei corsi disponibili:");
            VisualizzaCorsi();
            Console.WriteLine("Quale corso vuoi eliminare? Inserisci il codice");
            string codice = Console.ReadLine();
            string esito = bl.EliminaCorso(codice);
            Console.WriteLine(esito);

        }

        private static void ModificaCorso()
        {
            Console.WriteLine("Ecco l'elenco de i corsi disponibili");
            VisualizzaCorsi();
            Console.WriteLine("Quale corso vuoi modificare? Inserisci il codice");
            string codice = Console.ReadLine();
            Console.WriteLine("Inserisci il nuovo nome del corso");
            string nuovoNome = Console.ReadLine();
            Console.WriteLine("Inserisci la nua descrizione del corso");
            string nuovaDescrizione = Console.ReadLine();

            var esito= bl.ModificaCorso(codice, nuovoNome, nuovaDescrizione);
            Console.WriteLine(esito);
        }

        private static void InserisciNuovoCorso()
        {
            //Chiedo all'utente i dati per creare il nuovo corso
            Console.WriteLine("Inserisci il codice del nuovo corso");
            string codice = Console.ReadLine();
            Console.WriteLine("Inserisci il nome del nuovo corso");
            string nome = Console.ReadLine();
            Console.WriteLine("Inserisci la descrizione del nuovo corso");
            string descrizione = Console.ReadLine();

            //lo creo
            Corso nuovoCorso = new Corso();
            nuovoCorso.Nome = nome;
            nuovoCorso.CorsoCodice = codice;
            nuovoCorso.Descrizione = descrizione;

            //lo passo al business layer per controllare i dati ed aggiungerlo poi nel "DB"
            string esito= bl.InserisciNuovoCorso(nuovoCorso);
            //Stampo il messaggio
            Console.WriteLine(esito);
        }

        private static void VisualizzaCorsi()
        {
            var corsi=bl.GetAllCorsi();
            if (corsi.Count == 0)
            {
                Console.WriteLine("Lista vuota. Non ci sono corsi!");
            }
            else
            {
                Console.WriteLine("I Corsi disponibili sono:");
                foreach (var item in corsi)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }
    }
}
