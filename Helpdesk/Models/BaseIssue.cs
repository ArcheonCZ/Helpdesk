using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.Models
{
	[NotMapped]
	public abstract class BaseIssue
	{
		[Key]
		public uint Id { get; set; }
		[Required]
		public string Title { get; set; } = string.Empty;
		[Required]
		public string? Description { get; set; } = string.Empty;
		[Required]
		public DateOnly DueDate { get; set; }
	}
}
