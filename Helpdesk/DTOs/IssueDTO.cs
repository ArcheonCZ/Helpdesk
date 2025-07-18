using Helpdesk.Enums;
using Helpdesk.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.DTOs
{
	public class IssueDTO
	{
		public uint Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; } = string.Empty;
		public DateOnly DueDate { get; set; }
		public uint RequesterId { get; set; } 
		public PersonDTO? Requester { get; set; } 
		public uint AssigneeId { get; set; } 
		public PersonDTO? Assignee { get; set; } 
		public DateOnly CreatedDate { get; set; }
		public IssuePriority Priority { get; set; }
		public IssueStatus Status { get; set; }
		public List<ChatMessageDTO> Chat { get; set; } = new List<ChatMessageDTO>();
		public List<DocumentDTO> Documents { get; set; } = new List<DocumentDTO>();
		public List<BaseIssueDTO> Subtasks { get; set; } = new List<BaseIssueDTO>();
	}
}
