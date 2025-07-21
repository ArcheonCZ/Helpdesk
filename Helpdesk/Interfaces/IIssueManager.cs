using Helpdesk.DTOs;
using Helpdesk.Models;
using Helpdesk.Repositories;

namespace Helpdesk.Interfaces
{
    public interface IIssueManager
    {
        Task<IList<IssueDTO>> GetAll();
        Task<IssueDTO> GetById(uint id);
        Task<IList<IssueDTO>> GetAllIssuesByRequester(uint personId);
        Task<IList<IssueDTO>> GetAllIssuesByAssignee(uint personId);
        Task<IList<IssueDTO>> GetAllUnresolved();
        Task<IList<IssueDTO>> GetAllOverdue();
        Task<IList<IssueDTO>> GetAllWithOverdueSubIssue();
        Task<IList<SubIssueDTO>> GetAllSubissues(uint issueId);
        Task<SubIssueDTO> CreateSubIssue(SubIssueDTO subIssueDTO);
        Task<IssueDTO> UpdateIssue(IssueDTO issueDTO);
        Task ToggleSubIssueDone(uint subIssueId);
        Task<IssueDTO> CreateNewIssue(IssueCreateDTO issueDTO);
        Task<bool> DeleteIssue(uint id);
        Task<IList<DocumentDTO>?> GetAllDocumentsByIssue(uint issueId);
        Task<DocumentDTO> UploadDocument(DocumentDTO documentDTO);

    }
}
