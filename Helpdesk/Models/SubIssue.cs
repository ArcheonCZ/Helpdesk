using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.Models
{
	public class SubIssue : BaseIssue
	{
		[Required]
		public uint IssueId { get; set; }
		[ForeignKey("IssueId")]
		public virtual Issue Issue { get; set; } = null!;


	}
}
