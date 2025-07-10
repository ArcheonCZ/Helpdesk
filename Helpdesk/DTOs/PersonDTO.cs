using System.ComponentModel.DataAnnotations;

namespace Helpdesk.DTOs
{
	public class PersonDTO
	{
		public string Email { get; set; } = string.Empty;
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
