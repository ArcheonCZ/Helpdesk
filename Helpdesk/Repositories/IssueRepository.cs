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
		public IssueRepository(HelpdeskDbContext helpdeskDbContext) : base(helpdeskDbContext)
		{
		}


		public new async Task<IList<Issue>> GetAll()
		{
			return await GetAllIssuesQuery().ToListAsync();
		}
		public new async Task<Issue?> FindById(uint id)
		{
			return await GetAllIssuesQuery().FirstOrDefaultAsync(i => i.Id == id);
		}
		public async Task<IList<Issue>> GetIssuesByRequester(uint id)
		{
			IList<Issue> issuesFound = await GetAllIssuesQuery()
				.Where(i => i.RequesterId == id)
				.ToListAsync();
			return issuesFound;
		}
		public async Task<IList<Issue>> GetIssuesByAssignee(uint id)
		{
			IList<Issue> issuesFound = await GetAllIssuesQuery()
				.Where(i => i.AssigneeId == id)
				.ToListAsync();
			return issuesFound;
		}

		public async Task<IList<Issue>> GetUnresolvedIssues()
		{
			IList<Issue> issuesFound = await GetAllIssuesQuery()
				.Where(i => openStatuses
				.Contains(i.Status)).ToListAsync();
			return issuesFound;
		}
		public async Task<IList<Issue>> GetUnresolvedOverdueIssues(bool subIssues = false)
		{
			DateOnly today = DateOnly.FromDateTime(DateTime.Now);
			IList<Issue> issuesFound;
			if (!subIssues)
				issuesFound = await GetAllIssuesQuery()
				.Where(i => openStatuses
				.Contains(i.Status) && i.DueDate < today)
				.ToListAsync();
			else
				issuesFound = await GetAllIssuesQuery()
				.Where(i => openStatuses
				.Contains(i.Status) && i.SubIssues.Any(s => s.DueDate < today))
				.ToListAsync();
			return issuesFound;
		}

		private IQueryable<Issue> GetAllIssuesQuery()
		{
			return _helpdeskDbContext.Issues!
				.Include(i => i.Requester)
				.Include(i => i.Assignee)
				.Include(i => i.SubIssues);
		}


	}
}
