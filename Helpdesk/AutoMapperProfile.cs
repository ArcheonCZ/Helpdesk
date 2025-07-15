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
			CreateMap<ChatMessage,ChatMessageDTO>().ReverseMap();

			CreateMap<DateTime, DateOnly>().ConvertUsing(dt => DateOnly.FromDateTime(dt));
			CreateMap<DateOnly, DateTime>().ConvertUsing(da => da.ToDateTime(TimeOnly.MinValue));
		}
	}
}
