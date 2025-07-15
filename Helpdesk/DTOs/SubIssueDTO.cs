using System.ComponentModel.DataAnnotations;

namespace Helpdesk.DTOs
{
	public class SubIssueDTO
	{
		public uint Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; } = string.Empty;
		public DateTime DueDate { get; set; }
		public uint IssueId { get; set; }
	}
}
