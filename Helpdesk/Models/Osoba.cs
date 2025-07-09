using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Models
{
	public class Osoba
	{
		[Key]
		public uint Id { get; set; } 
		[Required]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string TypOsoby { get; set; } = "FO";
		
		public bool JeSpravceAplikace { get; set; } = false;

		///////////////////////////////////////
		///FO
		///////////////////////////////////////
		public string? Jmeno { get; set;} 
		public string? Prijmeni { get; set; } 
		public DateOnly? DatumNarozeni { get; set; }
		public int? FirmaId { get; set; }

		///////////////////////////////////////
		///PO
		///////////////////////////////////////
		public string? Nazev {  get; set; }
		public int? Ico { get; set; }
	}
}
