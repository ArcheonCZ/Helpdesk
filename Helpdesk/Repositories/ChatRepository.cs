using Helpdesk.Interfaces;
using Helpdesk.Models;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Repositories
{
	public class ChatRepository : BaseRepository<ChatMessage>, IChatRepository
	{
		public ChatRepository(IDbContextFactory<HelpdeskDbContext> contextFactory) : base(contextFactory)//HelpdeskDbContext helpdeskDbContext) : base(helpdeskDbContext)
		{
		}
		public async Task<IList<ChatMessage>> GetChatMessagesByIssue(uint issueId)
		{
			using var context = _contextFactory.CreateDbContext();
			IList<ChatMessage> messagesFound = await context.ChatMessages!
				.Include(cm =>cm.Sender)
				.Include(cm =>cm.Issue)
				.Where(cm => cm.IssueId == issueId)
				.ToListAsync();
			return messagesFound;
		}
	}
	

}
