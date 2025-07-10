using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Helpdesk.Models
{
	public class ChatMessage
	{
		[Key]
		public uint Id { get; set; }
		[Required]
		public uint SenderId { get; set; }
		[ForeignKey("SenderId")]
		public  Person Sender { get; set; } = null!;
		[Required]
		public string Message { get; set; } = string.Empty;
	}
}
