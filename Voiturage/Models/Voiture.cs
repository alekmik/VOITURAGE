using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Voiturage.Models
{
    public partial class Voiture
    {
        public Voiture()
        {
            Utilisateurs = new HashSet<Utilisateur>();
        }

        public int Id { get; set; }
        [Display(Name = "Marque : ")]
        public string Marque { get; set; } = null!;
        [Display(Name = "Modèle : ")]
        public string Modele { get; set; } = null!;

        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}
