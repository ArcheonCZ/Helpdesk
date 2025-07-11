using Helpdesk.Enums;
using Helpdesk.Interfaces;
using Helpdesk.Models;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Repositories
{
	public class IssueRepository : BaseRepository<Issue>, IIssueRepository
	{
		private static readonly IssueStatus[] openStatuses = new[] {
				IssueStatus.New,
				IssueStatus.InProgress,
				IssueStatus.WaitingForResponse
			};
		public IssueRepository(HelpdeskDbContext helpdeskDbContext) : base(helpdeskDbContext)
		{
		}

		public async Task<IList<Issue>> GetIssuesByRequester(uint id)
		{
			IList<Issue> issuesFound = await _dbSet.Where(i => i.RequesterId == id).ToListAsync();
			return issuesFound;
		}
		public async Task<IList<Issue>> GetIssuesByAssignee(uint id)
		{
			IList<Issue> issuesFound = await _dbSet.Where(i => i.AssigneeId == id).ToListAsync();
			return issuesFound;
		}

		public async Task<IList<Issue>> GetUnresolvedIssues()
		{
			IList<Issue> issuesFound = await _dbSet.Where(i => openStatuses.Contains(i.Status)).ToListAsync();
			return issuesFound;
		}
		public async Task<IList<Issue>> GetUnresolvedOverdueIssues(bool subIssues=false)
		{
			DateOnly today = DateOnly.FromDateTime(DateTime.Now);
			IList<Issue> issuesFound;
			if (!subIssues)
				issuesFound = await _dbSet.Where(i => openStatuses.Contains(i.Status) && i.DueDate<today).ToListAsync();
			else
				issuesFound = await _dbSet.Where(i => openStatuses.Contains(i.Status) && i.SubIssues.Any(s => s.DueDate < today)).ToListAsync();
			return issuesFound;
		}
	}
}
