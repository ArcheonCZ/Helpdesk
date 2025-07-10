using System.ComponentModel;

namespace Helpdesk.Enums
{
	public enum PersonType
	{
		[Description("Physical Person")]
		PhysicalPerson = 0,

		[Description("Legal Entity")]
		LegalEntity = 1

	}
}
