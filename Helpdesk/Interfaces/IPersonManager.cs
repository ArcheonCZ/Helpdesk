using Helpdesk.DTOs;

namespace Helpdesk.Interfaces
{
	public interface IPersonManager
	{
		Task<PersonDTO> GetById(uint id);
		Task<IList<PersonDTO>> GetAll();
		Task<PersonDTO> Insert(PersonDTO person);
		Task<PersonDTO> Update(PersonDTO person);
		Task<bool> Delete(uint id);
	}
}
