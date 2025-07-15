using Helpdesk.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.DTOs
{
	public class ChatMessageDTO
	{
		public uint SenderId { get; set; }
		public string Message { get; set; } = string.Empty;
		public uint IssueId { get; set; }
		public PersonDTO Sender { get; set; } = null!;
	}
}
