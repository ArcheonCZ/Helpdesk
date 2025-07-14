using Helpdesk.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.Models
{
	public class Issue : BaseIssue
	{
		[Required]
		public uint RequesterId { get; set; } 
		[ForeignKey("RequesterId")]
		public virtual Person Requester { get; set; } = null!;
		[Required]
		public uint AssigneeId { get; set; } 
		[ForeignKey("AssigneeId")]
		public virtual Person Assignee { get; set; } = null!;
		[Required]
		public DateOnly CreatedDate { get; set; }
		public IssueStatus Status { get; set; }
		public IssuePriority Priority { get; set; }
		public List<ChatMessage> Chat { get; set; } = new List<ChatMessage>();
		public List<Document> Documents { get; set; } = new List<Document>();
		public virtual List<SubIssue> SubIssues { get; set; } = new List<SubIssue>();
	}
}
