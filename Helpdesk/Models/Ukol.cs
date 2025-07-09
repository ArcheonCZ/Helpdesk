using Helpdesk.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.Models
{
	public class Ukol:BaseUkol
	{
		//[Key]
		//public uint Id { get; set; } 
		//[Required]
		//public string Nazev { get; set; } = string.Empty;
		//[Required]
		//public string Popis { get; set; } = string.Empty;
		[Required]
		public string ZadavatelId { get; set; } = string.Empty;
		[ForeignKey("ZadavatelId"), Required]
		public virtual required Osoba Zadavatel {  get; set; }
		[Required]
		public string ResitelId { get; set; } = string.Empty;
		[ForeignKey("ResitelId"), Required]
		public virtual required Osoba Resitel { get; set; }
		[Required]
		public DateOnly DatumVytvoreni { get; set; }
		[Required]
		public DateOnly TerminVyreseni { get; set; }
		public StavUkolu Stav {  get; set; }
		public PrioritaUkolu Priorita { get; set; }
		public List<ChatZprava> Chat { get; set; } = new List<ChatZprava>();
		public List<Dokument> Dokumenty { get; set; } = new List<Dokument>();
		public List<BaseUkol> Podukoly { get; set; } = new List<BaseUkol>();
	}
}
