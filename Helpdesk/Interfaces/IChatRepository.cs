using Helpdesk.Models;

namespace Helpdesk.Interfaces
{
	public interface IChatRepository:IBaseRepository<ChatMessage>
	{
		Task<IList<ChatMessage>> GetChatMessagesByIssue(uint issueId);
	}
}
