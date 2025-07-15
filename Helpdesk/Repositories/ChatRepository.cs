using Helpdesk.Interfaces;
using Helpdesk.Models;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Repositories
{
	public class ChatRepository : BaseRepository<ChatMessage>, IChatRepository
	{
		public ChatRepository(HelpdeskDbContext helpdeskDbContext) : base(helpdeskDbContext)
		{
		}
		public async Task<IList<ChatMessage>> GetChatMessagesByIssue(uint issueId)
		{
			IList<ChatMessage> messagesFound = await _helpdeskDbContext.ChatMessages!
				.Include(cm =>cm.Sender)
				.Include(cm =>cm.Issue)
				.Where(cm => cm.IssueId == issueId)
				.ToListAsync();
			return messagesFound;
		}
	}
	

}
