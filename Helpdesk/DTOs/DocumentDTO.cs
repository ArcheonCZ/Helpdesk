﻿namespace Helpdesk.DTOs
{
	public class DocumentDTO
	{
		public string FileName { get; set; } = string.Empty;
		public string FileType { get; set; } = string.Empty;
		public byte[] FileContent { get; set; } = Array.Empty<byte>();
	}
}
