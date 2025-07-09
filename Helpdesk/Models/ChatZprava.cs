using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Models
{
	public class ChatZprava
	{
		[Key]
		public uint Id { get; set; }
		[Required]
		public required Osoba Odesilatel { get; set; }
		[Required]
		public string Zprava { get; set; } = string.Empty;
	}
}
