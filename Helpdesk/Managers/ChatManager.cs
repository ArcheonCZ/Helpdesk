using AutoMapper;
using Helpdesk.DTOs;
using Helpdesk.Interfaces;
using Helpdesk.Models;

namespace Helpdesk.Managers
{
	public class ChatManager: IChatManager
	{
		private readonly IChatRepository _chatRepository;
		private readonly IMapper _mapper;

		public ChatManager(IChatRepository chatRepository, IMapper mapper)
		{
			_chatRepository = chatRepository;
			_mapper = mapper;
		}


		public async Task<IList<ChatMessageDTO>> GetMessagesByIssue(uint issueId)
		{
			IList<ChatMessage> messagesFound = await _chatRepository.GetChatMessagesByIssue(issueId);
			return _mapper.Map <IList<ChatMessageDTO>> (messagesFound);
		}

		public async Task<ChatMessageDTO> Insert(ChatMessageDTO messageDTO)
		{
			ChatMessage message = _mapper.Map<ChatMessage>(messageDTO);
			message = await _chatRepository.Insert (message);
			return _mapper.Map<ChatMessageDTO>(message);
		}


		public Task<ChatMessageDTO> GetById(uint id)
		{
			throw new NotImplementedException();
		}

		public async Task<ChatMessageDTO> Update(uint id, ChatMessageDTO messageDTO)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Delete(uint id)
		{
			throw new NotImplementedException();
		}
	
	}
}
