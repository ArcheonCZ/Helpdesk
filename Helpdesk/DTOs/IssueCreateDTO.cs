using Helpdesk.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.DTOs
{
	public class IssueCreateDTO
	{
		[Required(ErrorMessage = "Title is required.")]
		public string Title { get; set; } = string.Empty;

		public string? Description { get; set; }

		[Required(ErrorMessage = "Due date is required.")]
		public DateOnly DueDate { get; set; }

		[Required]
		public DateOnly CreatedDate { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "Requester must be filled in!")]
		public uint RequesterId { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "AssigneeId must be filled in!")]
		public uint AssigneeId { get; set; }

		[Required(ErrorMessage = "Priority is required.")]
		public IssuePriority Priority { get; set; }

		[Required]
		public IssueStatus Status { get; set; }
	}
}
