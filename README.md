# la-mia-pizzeria-crud-mvc

Creiamo prima un nostro controller chiamato PizzaController e utilizziamo lui d’ora in avanti. 
L’elenco delle pizze ora va passato come model dal controller, e la view deve utilizzarlo per mostrare l’html corretto.
Gestiamo anche la possibilità che non ci siano pizze nell’elenco: in quel caso dobbiamo mostrare un messaggio che indichi all’utente che non ci sono pizze presenti nella nostra applicazione.
Ogni pizza dell’elenco avrà un pulsante che se cliccato ci porterà a una pagina che mostrerà i dettagli di quella singola pizza. 
Dobbiamo quindi inviare l’id come parametro dell’URL, recuperarlo con la action, caricare i dati della pizza ricercata e passarli come model. La view a quel punto li mostrerà all’utente con la grafica che preferiamo.

Aggiungiamo tutto il codice necessario per mostrare il form per la creazione di una nuova pizza e 
per il salvataggio dei dati in tabella tramite Entity Framework.
Nella index creiamo ovviamente il bottone “Crea nuova pizza” che ci porta a questa nuova pagina creata.
Ricordiamoci che l’utente potrebbe sbagliare inserendo dei dati : gestiamo quindi la validazione!
Ad esempio verifichiamo che :
i dati della pizza siano tutti presenti
il campi di testo non superino una certa lunghezza
il prezzo abbia un valore valido (ha senso una pizza con prezzo minore o uguale a zero?)
BONUS
Prevediamo una validazione in più: vogliamo che la descrizione della pizza contenga almeno 5 parole.