using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Models
{
	public abstract class BaseUkol
	{
		[Key]
		public uint Id { get; set; }
		[Required]
		public string Nazev { get; set; } = string.Empty;
		[Required]
		public string? Popis { get; set; } = string.Empty;
		[Required]
		public DateOnly TerminVyreseni { get; set; }
	}
}
