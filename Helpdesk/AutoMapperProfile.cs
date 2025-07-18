using AutoMapper;
using Helpdesk.Models;
using Helpdesk.DTOs;
using Helpdesk.Enums;

namespace Helpdesk
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Person, PersonDTO>().ReverseMap();
			CreateMap<Issue, IssueDTO>().ReverseMap()
			    .ForMember(dest => dest.Requester, opt => opt.Ignore())
				.ForMember(dest => dest.Assignee, opt => opt.Ignore());
			CreateMap<SubIssue, SubIssueDTO>().ReverseMap();
			CreateMap<ChatMessage, ChatMessageDTO>().ReverseMap();
			CreateMap<Document, DocumentDTO>().ReverseMap();

			CreateMap<DateTime, DateOnly>().ConvertUsing(dt => DateOnly.FromDateTime(dt));
			CreateMap<DateOnly, DateTime>().ConvertUsing(da => da.ToDateTime(TimeOnly.MinValue));

			CreateMap<IssueCreateDTO, IssueDTO>()
				//.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateOnly.FromDateTime(DateTime.Today)))
				//.ForMember(dest => dest.Status, opt => opt.MapFrom(_ => IssueStatus.New))
				.ForMember(dest => dest.Requester, opt => opt.Ignore())
				.ForMember(dest => dest.Assignee, opt => opt.Ignore());
		}
	}
}
