namespace Helpdesk.Models
{
	public class Dokument
	{
		public uint Id { get; set; }

		public string JmenoSouboru { get; set; } = string.Empty;

		public string TypSouboru { get; set; } = string.Empty;

		public byte[] ObsahSouboru { get; set; } = [];
	}
}
