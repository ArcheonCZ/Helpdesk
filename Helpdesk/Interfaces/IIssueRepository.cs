using Helpdesk.Models;

namespace Helpdesk.Interfaces
{
	public interface IIssueRepository:IBaseRepository<Issue>
	{
		Task<IList<Issue>> GetIssuesByRequester(uint id);
		Task<IList<Issue>> GetIssuesByAssignee(uint id);
		Task<IList<Issue>> GetUnresolvedIssues();
		Task<IList<Issue>> GetUnresolvedOverdueIssues(bool subIssues = false);
		
	}
	
}
