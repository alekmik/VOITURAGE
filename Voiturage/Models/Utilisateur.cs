using System;
using System.Collections.Generic;

namespace Voiturage.Models
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            AviId2UtilisateurNavigations = new HashSet<Avi>();
            AviIdUtilisateurNavigations = new HashSet<Avi>();
            Trajets = new HashSet<Trajet>();
            IdTrajets = new HashSet<Trajet>();
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

        public virtual Voiture? IdVoitureNavigation { get; set; }
        public virtual ICollection<Avi> AviId2UtilisateurNavigations { get; set; }
        public virtual ICollection<Avi> AviIdUtilisateurNavigations { get; set; }
        public virtual ICollection<Trajet> Trajets { get; set; }

        public virtual ICollection<Trajet> IdTrajets { get; set; }
    }
}
