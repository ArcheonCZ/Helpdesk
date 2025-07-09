using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.Models
{
	public class PodUkol : BaseUkol
	{
		[Required]
		public uint UkolId { get; set; }
		[ForeignKey("UkolId")]
		public virtual Ukol Ukol { get; set; }


	}
}
