using System;
using System.Collections.Generic;

namespace Voiturage.Models
{
    public partial class Ville
    {
        public Ville()
        {
            TrajetAuDepart = new HashSet<Trajet>();
            TrajetALarrivee = new HashSet<Trajet>();
        }

        public int Id { get; set; }
        public string Nom { get; set; } = null!;

        public virtual ICollection<Trajet> TrajetAuDepart { get; set; }
        public virtual ICollection<Trajet> TrajetALarrivee { get; set; }
    }
}
