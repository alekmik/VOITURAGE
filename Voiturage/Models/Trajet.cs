using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Voiturage.Models
{
    public partial class Trajet
    {
        public Trajet()
        {
            Avis = new HashSet<Avis>();
            Passagers = new HashSet<Utilisateur>();
        }

        public int Id { get; set; }
        [Display(Name ="Heure de départ")]
        public DateTime HeureDepart { get; set; }
        [Display(Name = "Heure d'arrivée")]
        public DateTime HeureArrivee { get; set; }
        public double Prix { get; set; }
        [Display(Name = "Nombre de place")]
        [Range(1, 5,
        ErrorMessage = "Valeur {0} doit être compris entre {1} et {2}.")]
        public int Place { get; set; }
        [Display(Name = "Ville de départ")]
        public int IdVilleDepart { get; set; }
        [Display(Name = "Ville d'arrivée")]
        public int IdVilleArrivee { get; set; }
        public int IdChauffeur { get; set; }
        [Display(Name = "Ville d'arrivée")]
        public virtual Ville VilleArrivee { get; set; } = null!;
        public virtual Utilisateur Chauffeur { get; set; } = null!;
        [Display(Name = "Ville de départ")]
        public virtual Ville VilleDepart { get; set; } = null!;
        public virtual ICollection<Avis> Avis { get; set; }

        public virtual ICollection<Utilisateur> Passagers { get; set; }
    }
}
