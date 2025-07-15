using Helpdesk.Models;

namespace Helpdesk.Interfaces
{
	public interface ISubIssueRepository:IBaseRepository<SubIssue>
	{
		//Task AddSubIssueAsync(uint issueId, string title, DateOnly dueDate, string description = "");
		Task<IList<SubIssue>> GetAllSubIssues(uint issueId);
		Task ToggleSubIssueDone(uint subIssueId);
	}
}
