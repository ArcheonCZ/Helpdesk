using Helpdesk.Interfaces;
using Helpdesk.Models;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Repositories
{
	public class PersonRepository : BaseRepository<Person>, IPersonRepository
	{
		public PersonRepository(IDbContextFactory<HelpdeskDbContext> contextFactory) : base(contextFactory)//HelpdeskDbContext helpdeskDbContext) : base(helpdeskDbContext)
		{
		}
	}
}
