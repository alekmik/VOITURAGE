using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

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

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Veuillez renseigner votre nom")]
        public string Nom { get; set; } = null!;

        [Required(ErrorMessage = "Veuillez renseigner votre prénom")]
        public string Prenom { get; set; } = null!;

        [Required(ErrorMessage = "Veuillez renseigner votre e-mail")]
        [RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$",ErrorMessage ="E-mail invalide")]
        public string Mail { get; set; } = null!;

        public bool Admin { get; set; } = false;

        [Required(ErrorMessage = "Veuillez renseigner votre mot de passe")]
        [StringLength(20,MinimumLength = 8)]
        public string Password { get; set; } = null!;

        [Required]
        public string Salt { get; set; } = null!;

        [Required(ErrorMessage = "Il vous faut un nom d'utilisateur")]
        [StringLength(20, MinimumLength = 4)]
        public string Username { get; set; } = null!;

        public string ? Photo { get; set; } = null;

        public int? IdVoiture { get; set; }

        public virtual Voiture? Voiture { get; set; } = default!;
        public virtual ICollection<Avis> Notes { get; set; }
        public virtual ICollection<Avis> NotesDonnees { get; set; }
        public virtual ICollection<Trajet> TrajetsPassager { get; set; }
        public virtual ICollection<Trajet> TrajetsChauffeur { get; set; }
    }
}
