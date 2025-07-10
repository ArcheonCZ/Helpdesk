using AutoMapper;
using Helpdesk.DTOs;
using Helpdesk.Interfaces;

namespace Helpdesk.Managers
{
	public class IssueManager : IIssueManager
	{
		protected readonly IIssueRepository _issueRepository;
		protected readonly IMapper _mapper;

		public IssueManager(IIssueRepository issueRepository, IMapper mapper)
		{
			_issueRepository = issueRepository;
			_mapper = mapper;
		}

		public Task<IList<IssueDTO>> GetAll()
		{
			return _issueRepository.GetAll();
		}
		public Task<IList<IssueDTO>> GetAllByPersonId(uint personId)
		{
			throw new NotImplementedException();
		}
		public Task<IList<IssueDTO>> GetAllUnresolved()
		{
			throw new NotImplementedException();
		}
		public Task<IList<IssueDTO>> GetAllOverdue()
		{
			throw new NotImplementedException();
		}
		public Task<IList<IssueDTO>> GetAllWithOverdueSubIssue()
		{
			throw new NotImplementedException();
		}


	}
}
