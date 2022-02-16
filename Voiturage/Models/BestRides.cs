using System;
namespace Voiturage.Models
{
	public class BestRides
	{
		public Ville ? VilleDepart { get; set; } 
		public Ville ? VilleArrivee { get; set; } 
		public double prixMini { get; set; }
		public int nbTrajetPropose { get; set; }
		
	}
}

