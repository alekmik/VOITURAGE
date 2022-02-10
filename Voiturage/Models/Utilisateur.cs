using System;
using System.Collections.Generic;

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
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public bool Admin { get; set; }
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
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
