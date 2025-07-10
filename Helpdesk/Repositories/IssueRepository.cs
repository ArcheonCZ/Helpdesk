using Helpdesk.Models;

namespace Helpdesk.Repositories
{
	public class IssueRepository : BaseRepository<IssueDTO>
	{
		public IssueRepository(HelpdeskDbContext helpdeskDbContext) : base(helpdeskDbContext)
		{
		}
	}
}
