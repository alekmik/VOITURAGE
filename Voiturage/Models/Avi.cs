using System;
using System.Collections.Generic;

namespace Voiturage.Models
{
    public partial class Avi
    {
        public int Id { get; set; }
        public int Note { get; set; }
        public string Commentaire { get; set; } = null!;
        public int IdUtilisateur { get; set; }
        public int Id2Utilisateur { get; set; }
        public int IdTrajet { get; set; }

        public virtual Utilisateur Id2UtilisateurNavigation { get; set; } = null!;
        public virtual Trajet IdTrajetNavigation { get; set; } = null!;
        public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;
    }
}
