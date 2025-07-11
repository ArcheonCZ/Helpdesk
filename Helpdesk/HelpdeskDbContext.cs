using Helpdesk.Models;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk
{
	public class HelpdeskDbContext : DbContext
	{
		public DbSet<Person>? Persons { get; set; }
		public DbSet<Issue>? Issues { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);


			modelBuilder.Entity<Issue>()
				.HasOne(b => b.Requester)
				.WithMany()
				.HasForeignKey(b => b.RequesterId)
				.OnDelete(DeleteBehavior.Restrict); // nebo NoAction

			modelBuilder.Entity<Issue>()
				.HasOne(b => b.Assignee)
				.WithMany()
				.HasForeignKey(b => b.AssigneeId)
				.OnDelete(DeleteBehavior.Restrict); // nebo NoAction
		}

		public HelpdeskDbContext(DbContextOptions<HelpdeskDbContext> options)
			: base(options)
		{ }
	}
}
