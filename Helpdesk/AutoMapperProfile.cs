using AutoMapper;
using Helpdesk.Models;

namespace Helpdesk
{
	public class AutoMapperProfile: Profile
	{
		public AutoMapperProfile() 
		{
			CreateMap<PersonDTO.PersonDTO>().Reverse();
			CreateMap<IssueDTO.IssueDTO>().Reverse();
		}
	}
}
