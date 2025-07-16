using Helpdesk.Interfaces;
using Helpdesk.Models;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Repositories
{
	public class SubIssueRepository : BaseRepository<SubIssue>, ISubIssueRepository
	{
		public SubIssueRepository(IDbContextFactory<HelpdeskDbContext> contextFactory) : base(contextFactory)//HelpdeskDbContext helpdeskDbContext) : base(helpdeskDbContext)
		{
		}

		public async Task<IList<SubIssue>> GetAllSubIssues(uint issueId)
		{
			using var context = _contextFactory.CreateDbContext();
			IList<SubIssue> issuesFound = await context.SubIssues!.Where(s => s.IssueId == issueId).ToListAsync();
			return issuesFound;
		}

		public async Task ToggleSubIssueDone(uint subIssueId)
		{
			using var context = _contextFactory.CreateDbContext();
			var sub = await context.SubIssues.FindAsync(subIssueId);
			if (sub is not null)
			{
				sub.IsDone = !sub.IsDone;
				await context.SaveChangesAsync();
			}
		}

	}
}
