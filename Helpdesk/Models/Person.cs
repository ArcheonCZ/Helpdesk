using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Models
{
	public class Person
	{
		[Key]
		public uint Id { get; set; } 
		[Required]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string PersonType { get; set; } = "PP";

		public bool IsApplicationAdmin { get; set; } = false;

		///////////////////////////////////////
		/// Physical Person
		///////////////////////////////////////
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public DateOnly? DateOfBirth { get; set; }
		public int? CompanyId { get; set; }

		///////////////////////////////////////
		/// Legal Entity 
		///////////////////////////////////////
		public string? CompanyName { get; set; }
		public int? IdentificationNumber { get; set; }  // ICO 
	}
}
