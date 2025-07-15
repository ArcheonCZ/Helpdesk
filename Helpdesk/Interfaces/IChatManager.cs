using Helpdesk.DTOs;

namespace Helpdesk.Interfaces
{
	public interface IChatManager
	{
		Task<ChatMessageDTO> GetById(uint id);
		Task<IList<ChatMessageDTO>> GetMessagesByIssue(uint issueId);
		Task<ChatMessageDTO> Insert(ChatMessageDTO messageDTO);
		Task<ChatMessageDTO> Update(uint id, ChatMessageDTO messageDTO);
		Task<bool> Delete(uint id);
		

	}
}
