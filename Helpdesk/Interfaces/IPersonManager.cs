using Helpdesk.DTOs;

namespace Helpdesk.Interfaces
{
	public interface IPersonManager
	{
		Task<PersonDTO> GetById(uint id);
		Task<IList<PersonDTO>> GetAll();
		Task<PersonDTO> Insert(PersonDTO person);
		Task<PersonDTO> Update(uint id, PersonDTO person);
		Task<bool> Delete(uint id);
	}
}
