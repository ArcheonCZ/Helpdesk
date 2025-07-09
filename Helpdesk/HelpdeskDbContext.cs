using Helpdesk.Models;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk
{
	public class HelpdeskDbContext : DbContext
	{
		public DbSet<Osoba>? Osoby { get; set; }
		public DbSet<Ukol>? Ukoly { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);


			////model builder nastavení sloupců a vazeb
		}

		public HelpdeskDbContext(DbContextOptions<HelpdeskDbContext> options)
			: base(options)
		{ }
	}
}
