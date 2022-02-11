using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Voiturage.Models
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            Notes = new HashSet<Avis>();
            NotesDonnees = new HashSet<Avis>();
            TrajetsChauffeur = new HashSet<Trajet>();
            TrajetsPassager = new HashSet<Trajet>();
        }

        public int Id { get; set; }
        [Display(Name = "Nom")]
        public string Nom { get; set; } = null!;
        [Display(Name = "Prénom")]
        public string Prenom { get; set; } = null!;
        [Display(Name = "e-mail")]
        public string Mail { get; set; } = null!;
        public bool Admin { get; set; }
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        [Display(Name = "Pseudo")]
        public string Username { get; set; } = null!;
        public string Photo { get; set; } = null!;
        public int? IdVoiture { get; set; }

        public virtual Voiture? Voiture { get; set; }
        public virtual ICollection<Avis> Notes { get; set; }
        public virtual ICollection<Avis> NotesDonnees { get; set; }
        public virtual ICollection<Trajet> TrajetsPassager { get; set; }
        public virtual ICollection<Trajet> TrajetsChauffeur { get; set; }
    }
}
