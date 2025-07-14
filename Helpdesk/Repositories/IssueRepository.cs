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


		public new async Task<IList<Issue>> GetAll()
		{
			return await _helpdeskDbContext.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.ToListAsync();
		}
		public new async Task<Issue?> FindById(uint id)
		{
			return await _helpdeskDbContext.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.FirstOrDefaultAsync(i => i.Id == id); 
		}
		public async Task<IList<Issue>> GetIssuesByRequester(uint id)
		{
			IList<Issue> issuesFound = await _helpdeskDbContext.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.Where(i => i.RequesterId == id)
				.ToListAsync();
			return issuesFound;
		}
		public async Task<IList<Issue>> GetIssuesByAssignee(uint id)
		{
			IList<Issue> issuesFound = await _helpdeskDbContext.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.Where(i => i.AssigneeId == id)
				.ToListAsync();
			return issuesFound;
		}

		public async Task<IList<Issue>> GetUnresolvedIssues()
		{
			IList<Issue> issuesFound = await _helpdeskDbContext.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.Where(i => openStatuses
				.Contains(i.Status)).ToListAsync();
			return issuesFound;
		}
		public async Task<IList<Issue>> GetUnresolvedOverdueIssues(bool subIssues=false)
		{
			DateOnly today = DateOnly.FromDateTime(DateTime.Now);
			IList<Issue> issuesFound;
			if (!subIssues)
				issuesFound = await _helpdeskDbContext.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.Where(i => openStatuses
				.Contains(i.Status) && i.DueDate<today)
				.ToListAsync();
			else
				issuesFound = await _helpdeskDbContext.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.Where(i => openStatuses
				.Contains(i.Status) && i.SubIssues.Any(s => s.DueDate < today))
				.ToListAsync();
			return issuesFound;
		}
	}
}
