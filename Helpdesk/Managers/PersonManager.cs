using AutoMapper;
using Helpdesk.Interfaces;
using Helpdesk.DTOs;
using Helpdesk.Models;

namespace Helpdesk.Managers
{
	public class PersonManager : IPersonManager
	{
		protected readonly IPersonRepository _personRepository;
		protected readonly IMapper _mapper;
		public PersonManager(IPersonRepository personRepository, IMapper mapper) 
		{ 
			_personRepository = personRepository;
			_mapper = mapper;			
		}


		public async Task<IList<PersonDTO>> GetAll()
		{
			IList<Person> persons = await _personRepository.GetAll();
			return _mapper.Map<IList<PersonDTO>>(persons);
		}
		public async Task<PersonDTO> GetById(uint id)
		{
			Person? personFound = await _personRepository.FindById(id);
			return _mapper.Map<PersonDTO>(personFound);
		}
		public async Task<PersonDTO> Insert(PersonDTO personDTO)
		{
			Person person = _mapper.Map<Person>(personDTO);
			person = await _personRepository.Insert(person);
			return _mapper.Map<PersonDTO>(person);
		}

		public async Task<PersonDTO> Update(uint id,PersonDTO personDTO)
		{
			Person person = _mapper.Map<Person>(personDTO);
			person.Id= id;
			person = await _personRepository.Update(person);
			return _mapper.Map<PersonDTO>(person);
		}
		public async Task<bool> Delete(uint id)
		{
			return await _personRepository.Delete(id);
		}
	}
}
