using System;
using System.Collections.Generic;

namespace Voiturage.Models
{
    public partial class Trajet
    {
        public Trajet()
        {
            Avis = new HashSet<Avi>();
            Ids = new HashSet<Utilisateur>();
        }

        public int Id { get; set; }
        public DateTime HeureDepart { get; set; }
        public DateTime HeureArrivee { get; set; }
        public double Prix { get; set; }
        public int Place { get; set; }
        public int IdVille { get; set; }
        public int Id2Ville { get; set; }
        public int IdUtilisateur { get; set; }

        public virtual Ville Id2VilleNavigation { get; set; } = null!;
        public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;
        public virtual Ville IdVilleNavigation { get; set; } = null!;
        public virtual ICollection<Avi> Avis { get; set; }

        public virtual ICollection<Utilisateur> Ids { get; set; }
    }
}
