using Helpdesk.Models;

namespace Helpdesk.Interfaces
{
	public interface IDocumentRepository: IBaseRepository<Document>
	{
		Task<IList<Document>> GetAllByIssue(uint issueId);
	}
}
