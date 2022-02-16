using System;
namespace Voiturage.Models
{
	public class SearchViewModel
	{

		public int IdVilleDepart { get; set; }
		public int IdVilleArrivee { get; set; }
		public DateTime DateDepart { get; set; }
		public int PlacesRequises { get; set; }
	}
}

