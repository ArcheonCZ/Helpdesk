using Helpdesk.DTOs;

namespace Helpdesk.Interfaces
{
	public interface IIssueManager
	{
		public Task<IList<IssueDTO>> GetAll();
		public Task<IList<IssueDTO>> GetAllByPersonId(uint personId);
		public Task<IList<IssueDTO>> GetAllUnresolved();
		public Task<IList<IssueDTO>> GetAllOverdue();
		public Task<IList<IssueDTO>> GetAllWithOverdueSubIssue();

	}
}
