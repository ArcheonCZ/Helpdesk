using Helpdesk.DTOs;
using Helpdesk.Models;
using Helpdesk.Repositories;

namespace Helpdesk.Interfaces
{
	public interface IIssueManager
	{
		 Task<IList<IssueDTO>> GetAll();
		Task<IList<IssueDTO>> GetAllIssuesByRequester(uint personId);
		Task<IList<IssueDTO>> GetAllIssuesByAssignee(uint personId);
		 Task<IList<IssueDTO>> GetAllUnresolved();
		 Task<IList<IssueDTO>> GetAllOverdue();
		 Task<IList<IssueDTO>> GetAllWithOverdueSubIssue();

	}
}
