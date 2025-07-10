using System.ComponentModel.DataAnnotations;

namespace Helpdesk.DTOs
{
	public class BaseIssueDTO
	{
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; } = string.Empty;
		public DateOnly DueDate { get; set; }
	}
}
