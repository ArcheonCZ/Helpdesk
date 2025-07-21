using AutoMapper;
using Helpdesk.DTOs;
using Helpdesk.Interfaces;
using Helpdesk.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

namespace Helpdesk.Managers
{
	public class IssueManager : IIssueManager
	{
		protected readonly IIssueRepository _issueRepository;
		protected readonly ISubIssueRepository _subIssueRepository;
		protected readonly IPersonManager _personManager;
		protected readonly IDocumentRepository _documentRepository;
		protected readonly IMapper _mapper;

		public IssueManager(IIssueRepository issueRepository, 
			ISubIssueRepository subIssueRepository, 
			IPersonManager personManager,
			IDocumentRepository documentRepository,
			IMapper mapper)
		{
			_issueRepository = issueRepository;
			_mapper = mapper;
			_subIssueRepository = subIssueRepository;
			_documentRepository = documentRepository;
			_personManager = personManager;
		}

		public async Task<IList<IssueDTO>> GetAll()
		{
			return _mapper.Map<IList<IssueDTO>>(await _issueRepository.GetAll());
		}
		public async Task<IssueDTO> GetById(uint id)
		{
			Issue? issueFound = await _issueRepository.FindById(id);
			return _mapper.Map<IssueDTO>(issueFound);
		}
		public async Task<IList<IssueDTO>> GetAllIssuesByRequester(uint personId)
		{
			IList<Issue> issuesFound = await _issueRepository.GetIssuesByRequester(personId);
			return _mapper.Map<IList<IssueDTO>>(issuesFound);
		}
		public async Task<IList<IssueDTO>> GetAllIssuesByAssignee(uint personId)
		{
			IList<Issue> issuesFound = await _issueRepository.GetIssuesByAssignee(personId);
			return _mapper.Map<IList<IssueDTO>>(issuesFound);
		}
		public async Task<IList<IssueDTO>> GetAllUnresolved()
		{
			IList<Issue> issuesFound = await _issueRepository.GetUnresolvedIssues();
			return _mapper.Map<IList<IssueDTO>>(issuesFound);
		}
		public async Task<IList<IssueDTO>> GetAllOverdue()
		{
			IList<Issue> issuesFound = await _issueRepository.GetUnresolvedOverdueIssues();
			return _mapper.Map<IList<IssueDTO>>(issuesFound);
		}
		public async Task<IList<IssueDTO>> GetAllWithOverdueSubIssue()
		{
			IList<Issue> issuesFound = await _issueRepository.GetUnresolvedOverdueIssues(true);
			return _mapper.Map<IList<IssueDTO>>(issuesFound);
		}

		public async Task<IssueDTO> CreateNewIssue(IssueCreateDTO issueCreateDTO)
		{
			IssueDTO issueDTO = _mapper.Map<IssueDTO>(issueCreateDTO);
			issueDTO.Requester = await _personManager.GetById(issueDTO.RequesterId);
			issueDTO.Assignee = await _personManager.GetById(issueDTO.AssigneeId);

			if (issueDTO.Requester == null || issueDTO.Assignee == null)
				throw new Exception("Requester or Assignee does not exist.");
			Issue issue = _mapper.Map<Issue>(issueDTO);

			issue = await _issueRepository.Insert(issue);
			return _mapper.Map<IssueDTO>(issue);
		}

		public async Task<IssueDTO> UpdateIssue(IssueDTO issueDTO)
		{
        
            Issue issueToBeUpdated = _mapper.Map<Issue>(issueDTO);
			issueToBeUpdated = await _issueRepository.Update(issueToBeUpdated);
			Issue? testIssue = await _issueRepository.FindById(issueDTO.Id);
            return _mapper.Map<IssueDTO>(issueToBeUpdated);
		}

		public async Task<IList<SubIssueDTO>> GetAllSubissues(uint issueId)
		{
			IList<SubIssue> subIssuesFound = await _subIssueRepository.GetAllSubIssues(issueId);
			return _mapper.Map<IList<SubIssueDTO>>(subIssuesFound);
		}
		public async Task<SubIssueDTO> CreateSubIssue(SubIssueDTO subIssueDTO)
		{
			SubIssue subIssue = _mapper.Map<SubIssue>(subIssueDTO);
			subIssue = await _subIssueRepository.Insert(subIssue);
			return _mapper.Map<SubIssueDTO>(subIssue);
		}
		public async Task ToggleSubIssueDone(uint subIssueId)
		{
			await _subIssueRepository.ToggleSubIssueDone(subIssueId);
		}
		public async Task<bool> DeleteIssue(uint id)
		{
			bool isDeleted = await _issueRepository.Delete(id);
			return isDeleted;
		}

		public async Task<IList<DocumentDTO>?> GetAllDocumentsByIssue(uint issueId)
		{ 
			IList<Document> docsFound= await _documentRepository.GetAllByIssue(issueId);
			if (docsFound == null)
				return null;
			return _mapper.Map <IList<DocumentDTO>>(docsFound);
		}

		public async Task<DocumentDTO> UploadDocument (DocumentDTO documentDTO)
		{
			Document doc = _mapper.Map<Document>(documentDTO);
			doc = await _documentRepository.Insert(doc);
			return _mapper.Map<DocumentDTO>(doc);
		}
	}
}
