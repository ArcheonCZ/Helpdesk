using Helpdesk.Interfaces;
using Helpdesk.Models;

namespace Helpdesk.Repositories
{
	public class PersonRepository : BaseRepository<Person>, IPersonRepository
	{
		public PersonRepository(HelpdeskDbContext helpdeskDbContext) : base(helpdeskDbContext)
		{
		}
	}
}
