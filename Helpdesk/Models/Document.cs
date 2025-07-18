namespace Helpdesk.Models
{
	public class Document
	{
		public uint Id { get; set; }
		public string FileName { get; set; } = string.Empty;
		public string FileType { get; set; } = string.Empty;
		public byte[] FileContent { get; set; } = Array.Empty<byte>();
		public uint IssueId { get; set; }
		public virtual Issue Issue { get; set; } = null!;
	}
}
