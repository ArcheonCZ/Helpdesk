using Helpdesk.Interfaces;
using Helpdesk.Models;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Repositories
{
	public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
	{
		public DocumentRepository(IDbContextFactory<HelpdeskDbContext> contextFactory) : base(contextFactory)
		{
		}

		public async Task<IList<Document>> GetAllByIssue(uint issueId)
		{
			using var context = _contextFactory.CreateDbContext();
			IList<Document> documentsFound = await context.Set<Document>()
				.Where(q =>q.IssueId == issueId)
				.ToListAsync();
			return documentsFound;
		}
	}
}
