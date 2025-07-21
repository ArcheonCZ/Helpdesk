using Helpdesk.Enums;
using Helpdesk.Interfaces;
using Helpdesk.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Repositories
{
	public class IssueRepository : BaseRepository<Issue>, IIssueRepository
	{
		
		private static readonly IssueStatus[] openStatuses = new[] {
				IssueStatus.New,
				IssueStatus.InProgress,
				IssueStatus.WaitingForResponse
			};
		public IssueRepository(IDbContextFactory<HelpdeskDbContext> contextFactory):base(contextFactory)
		{
		}


		public new async Task<IList<Issue>> GetAll()
		{
			using var context = _contextFactory.CreateDbContext();
			return await context.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.Include(i => i.SubIssues)
				.ToListAsync();
		}
		public new async Task<Issue?> FindById(uint id)
		{
			using var context = _contextFactory.CreateDbContext();
			return await context.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.Include(i => i.SubIssues)
				.FirstOrDefaultAsync(i => i.Id == id);
		}
		public async Task<IList<Issue>> GetIssuesByRequester(uint id)
		{
			using var context = _contextFactory.CreateDbContext();
			IList<Issue> issuesFound = await context.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.Include(i => i.SubIssues)
				.Where(i => i.RequesterId == id)
				.ToListAsync();
			return issuesFound;
		}
		public async Task<IList<Issue>> GetIssuesByAssignee(uint id)
		{
			using var context = _contextFactory.CreateDbContext();
			IList<Issue> issuesFound = await context.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.Include(i => i.SubIssues)
				.Where(i => i.AssigneeId == id)
				.ToListAsync();
			return issuesFound;
		}

		public async Task<IList<Issue>> GetUnresolvedIssues()
		{
			using var context = _contextFactory.CreateDbContext();
			IList<Issue> issuesFound = await context.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.Include(i => i.SubIssues)
				.Where(i => openStatuses
				.Contains(i.Status)).ToListAsync();
			return issuesFound;
		}
		public async Task<IList<Issue>> GetUnresolvedOverdueIssues(bool subIssues = false)
		{
			using var context = _contextFactory.CreateDbContext();
			DateOnly today = DateOnly.FromDateTime(DateTime.Now);
			IList<Issue> issuesFound;
			if (!subIssues)
				issuesFound = await context.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.Include(i => i.SubIssues)
				.Where(i => openStatuses
				.Contains(i.Status) && i.DueDate < today)
				.ToListAsync();
			else
				issuesFound = await context.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.Include(i => i.SubIssues)
				.Where(i => openStatuses.Contains(i.Status) && i.SubIssues.Any(s => s.DueDate < today && !s.IsDone))
				.ToListAsync();
			return issuesFound;
		}

	
	}
}
