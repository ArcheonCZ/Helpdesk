using AutoMapper;
using Helpdesk.Interfaces;
using Helpdesk.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Helpdesk.Managers
{
	public class PersonManager : IPersonManager
	{
		protected readonly IPersonRepository _personRepository;
		protected readonly IMapper _mapper;
		public PersonManager(IPersonRepository personrepository, IMapper mapper) 
		{ 
			_personRepository = personrepository;
			_mapper = mapper;			
		}


		public async Task<IList<PersonDTO>> GetAll()
		{
			IList<PersonDTO> persons = await _personRepository.GetAll();
			return _mapper.Map<IList<PersonDTO>>(persons);
		}
		public async Task<PersonDTO> GetById(uint id)
		{
			throw new NotImplementedException();
		}
		public async Task<PersonDTO> Insert(PersonDTO person)
		{
			throw new NotImplementedException();
		}

		public async Task<PersonDTO> Update(PersonDTO person)
		{
			throw new NotImplementedException();
		}
		public async Task<bool> Delete(uint id)
		{
			return await _personRepository.Delete(id);
		}
	}
}
