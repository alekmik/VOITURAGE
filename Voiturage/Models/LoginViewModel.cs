using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Voiturage.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage ="Le nom d'utilisateur est requis")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Le mot de passe est requis")]
		public string Password { get; set; }

	}
}

