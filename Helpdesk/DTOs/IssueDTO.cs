using Helpdesk.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.DTOs
{
	public class IssueDTO
	{
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; } = string.Empty;
		public DateOnly DueDate { get; set; }
		public string RequesterId { get; set; } = string.Empty;
		public string AssigneeId { get; set; } = string.Empty;
		public DateOnly CreatedDate { get; set; }
		public IssuePriority Priority { get; set; }
		public List<ChatMessageDTO> Chat { get; set; } = new List<ChatMessageDTO>();
		public List<DocumentDTO> Documents { get; set; } = new List<DocumentDTO>();
		public List<BaseIssueDTO> Subtasks { get; set; } = new List<BaseIssueDTO>();
	}
}
