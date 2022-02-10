using System;
using System.Collections.Generic;

namespace Voiturage.Models
{
    public partial class Voiture
    {
        public Voiture()
        {
            Utilisateurs = new HashSet<Utilisateur>();
        }

        public int Id { get; set; }
        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;

        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}
