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
            /*Trajet = new HashSet<Trajet>();*/
        }

        [Key]
        public int Id { get; set; }
        [Display(Name ="Heure de départ : ")]
        [Required(ErrorMessage = "Veuillez saisir une heure de départ.")]
        public DateTime HeureDepart { get; set; }
        [Display(Name = "Heure d'arrivée : ")]
        [Required(ErrorMessage = "Veuillez saisir une heure d'arrivée.")]
        public DateTime HeureArrivee { get; set; }
        [Required(ErrorMessage = "Veuillez saisir le prix du trajet.")]
        public double Prix { get; set; }
        [Display(Name = "Nombre de places : ")]
        [Range(1, 5,
        ErrorMessage = "Valeur {0} doit être compris entre {1} et {2}.")]
        [Required(ErrorMessage = "Veuillez saisir le nombre de place disponible.")]
        public int Place { get; set; }
        public int PlaceMax { get; set; }
        [Display(Name = "Ville de départ : ")]
        [Required(ErrorMessage = "Veuillez sélectionner une ville de départ.")]
        public int IdVilleDepart { get; set; }
        [Display(Name = "Ville d'arrivée : ")]
        [Required(ErrorMessage = "Veuillez sélectionner une ville d'arrivée.")]
        public int IdVilleArrivee { get; set; }
        [Display(Name = "Chauffeur : ")]
        [Required(ErrorMessage = "Veuillez selectionner le chauffeur du trajet.")]
        public int IdChauffeur { get; set; }
        [Display(Name = "Ville d'arrivée : ")]

        public virtual Ville ? VilleArrivee { get; set; } = null!;

        public virtual Utilisateur ? Chauffeur { get; set; } = null!;
        [Display(Name = "Ville de départ : ")]
        public virtual Ville ? VilleDepart { get; set; } = null!;
        public virtual ICollection<Avis> Avis { get; set; }

        public virtual ICollection<Utilisateur> Passagers { get; set; }
    }
}
