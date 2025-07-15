using AutoMapper;
using Helpdesk.Models;
using Helpdesk.DTOs;

namespace Helpdesk
{
	public class AutoMapperProfile: Profile
	{
		public AutoMapperProfile() 
		{
			CreateMap<Person,PersonDTO>().ReverseMap();
			CreateMap<Issue,IssueDTO>().ReverseMap();
			CreateMap<SubIssue,SubIssueDTO>().ReverseMap();
		}
	}
}
