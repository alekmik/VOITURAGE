using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Voiturage.Models
{
    public partial class Avis
    {
        [Key]
        public int Id { get; set; }
        public int Note { get; set; }
        public string Commentaire { get; set; } = null!;
        public int IdNotant { get; set; }
        public int IdNote { get; set; }
        public int IdTrajet { get; set; }

        public virtual Utilisateur? UtilisateurNote { get; set; } = null;
        public virtual Trajet? Trajet { get; set; } = null;
        public virtual Utilisateur? UtilisateurNotant { get; set; } = null;
    }
}
