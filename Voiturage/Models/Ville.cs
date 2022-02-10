using System;
using System.Collections.Generic;

namespace Voiturage.Models
{
    public partial class Ville
    {
        public Ville()
        {
            TrajetId2VilleNavigations = new HashSet<Trajet>();
            TrajetIdVilleNavigations = new HashSet<Trajet>();
        }

        public int Id { get; set; }
        public string Nom { get; set; } = null!;

        public virtual ICollection<Trajet> TrajetId2VilleNavigations { get; set; }
        public virtual ICollection<Trajet> TrajetIdVilleNavigations { get; set; }
    }
}
