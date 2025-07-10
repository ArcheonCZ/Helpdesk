using Helpdesk.Models;

namespace Helpdesk.Repositories
{
	public class PersonRepository : BaseRepository<PersonDTO>
	{
		public PersonRepository(HelpdeskDbContext helpdeskDbContext) : base(helpdeskDbContext)
		{
		}
	}
}
