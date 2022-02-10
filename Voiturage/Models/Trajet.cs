using System;
using System.Collections.Generic;

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
        public DateTime HeureDepart { get; set; }
        public DateTime HeureArrivee { get; set; }
        public double Prix { get; set; }
        public int Place { get; set; }
        public int IdVilleDepart { get; set; }
        public int IdVilleArrivee { get; set; }
        public int IdChauffeur { get; set; }

        public virtual Ville VilleArrivee { get; set; } = null!;
        public virtual Utilisateur Chauffeur { get; set; } = null!;
        public virtual Ville VilleDepart { get; set; } = null!;
        public virtual ICollection<Avis> Avis { get; set; }

        public virtual ICollection<Utilisateur> Passagers { get; set; }
    }
}
