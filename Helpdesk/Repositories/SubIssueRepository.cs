using Helpdesk.Interfaces;
using Helpdesk.Models;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Repositories
{
	public class SubIssueRepository : BaseRepository<SubIssue>, ISubIssueRepository
	{
		public SubIssueRepository(HelpdeskDbContext helpdeskDbContext) : base(helpdeskDbContext)
		{
		}

				public async Task<IList<SubIssue>> GetAllSubIssues(uint issueId)
		{
			IList<SubIssue> issuesFound = await _helpdeskDbContext.SubIssues!.Where(s => s.IssueId == issueId).ToListAsync();
			return issuesFound;
		}

		public async Task ToggleSubIssueDone(uint subIssueId)
		{
			var sub = await _helpdeskDbContext.SubIssues.FindAsync(subIssueId);
			if (sub is not null)
			{
				sub.IsDone = !sub.IsDone;
				await _helpdeskDbContext.SaveChangesAsync();
			}
		}


		//public async Task AddSubIssueAsync(uint issueId, string title,  DateOnly dueDate, string description = "")
		//{
		//	var subIssue = new SubIssue
		//	{
		//		Id = default,
		//		Title = title,
		//		Description = description,
		//		DueDate = dueDate,
		//		IsDone = false,
		//		IssueId = issueId
		//	};

		//	_helpdeskDbContext.SubIssues.Add(subIssue);
		//	await _helpdeskDbContext.SaveChangesAsync();
		//}
	}
}
