using System.ComponentModel.DataAnnotations;
using ExpressiveAnnotations.Attributes;

namespace PerplexIdeeenbus.Models
{
    public enum IdeaType { suggestie, uitje }

    public class Idea
    {
        public long Id { get; }

        /*Mogelijke verbetering: DateTime.Now is niet bruikbaar op deze manier. Momenteel wordt the CreationTime aangepast bij elke GET van een Idea.
         De huidige implementatie sorteert de data in site.js op het Id van de Ideas, in plaats van de CreationTime. Dit zou tot hetzelfde resultaat moeten leiden als een correcte sort op de CreationTime, 
         aangezien Ids van verwijderde Ideas niet opnieuw gebruikt worden. Deze aanpak is echter wel beperkt door de maximale waarde van het Id. */
        public DateTime CreationTime { get; } = DateTime.Now; 

        [Required]
        public string? Onderwerp { get; set; }

        [Required]
        public string? Beschrijving { get; set; } //Mogelijke verbetering: Escape characters toevoegen in de setter (en verwijderen in de getter indien nodig)

        public int UserId { get; set; }

        public string? UserName { get; set; }

        private IdeaType _ideaType;

        [Required]
        public string Type { 
            get 
            { 
                return _ideaType.ToString();  
            }
            set
            {
                _ideaType = (IdeaType)Enum.Parse(typeof(IdeaType), value); //TODO: niet crashen op ongeldig type
            }
        }

        private DateTime _beginDatum;

        [RequiredIf("_ideaType == IdeaType.uitje")] //TODO: Dit lijkt niet afgedwongen te worden. Zoek uit waarom.
        public string BeginDatum {
            get 
            { 
                return _beginDatum.ToString("yyyy-MM-dd HH:mm");
            }
            set 
            {
                _beginDatum = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm", null); //TODO: Niet crashen op ongeldig format
            } 
        }

        private DateTime _eindDatum;

        [RequiredIf("_ideaType == IdeaType.uitje")]
        public string EindDatum {
            get
            {
                return _eindDatum.ToString("yyyy-MM-dd HH:mm");
            }
            set
            {
                _eindDatum = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm", null); //TODO: Niet crashen op ongeldig format
            }
        }

        public TimeSpan Duur { get { return _eindDatum.Subtract(_beginDatum); } } 

        //public DateTime BeginDatum { get; set; }
        //public DateTime EindDatum { get; set; }

        public List<string>? Categories { get; set; }
    }
}
