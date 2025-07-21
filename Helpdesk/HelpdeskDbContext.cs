using Helpdesk.Enums;
using Helpdesk.Models;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk
{
    public class HelpdeskDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<SubIssue> SubIssues { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Document> Documents { get; set; }

        public HelpdeskDbContext(DbContextOptions<HelpdeskDbContext> options)
        : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Issue>().ToTable("Issues");
            modelBuilder.Entity<SubIssue>().ToTable("SubIssues");

            modelBuilder.Entity<Issue>()
                .HasOne(b => b.Requester)
                .WithMany()
                .HasForeignKey(b => b.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Issue>()
                .HasOne(b => b.Assignee)
                .WithMany()
                .HasForeignKey(b => b.AssigneeId)
                .OnDelete(DeleteBehavior.Restrict);

            InitialTestingData(modelBuilder);
        }

        private static void InitialTestingData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    Email = "firma@example.com",
                    PersonType = PersonType.LegalEntity,
                    CompanyName = "Testovací Firma s.r.o.",
                    IdentificationNumber = 12345678,
                    IsApplicationAdmin = false
                },
                new Person
                {
                    Id = 2,
                    Email = "jan.novak@example.com",
                    PersonType = PersonType.PhysicalPerson,
                    FirstName = "Jan",
                    LastName = "Novák",
                    DateOfBirth = new DateOnly(1985, 5, 20),
                    IsApplicationAdmin = true
                },
                new Person
                {
                    Id = 3,
                    Email = "petr.svoboda@example.com",
                    PersonType = PersonType.PhysicalPerson,
                    FirstName = "Petr",
                    LastName = "Svoboda",
                    DateOfBirth = new DateOnly(1990, 8, 15),
                    IsApplicationAdmin = false

                }
            );
            modelBuilder.Entity<Issue>().HasData(
                new Issue
                {
                    Id = 1,
                    Title = "Problém s přihlášením",
                    Description = "Uživatel hlásí, že se nemůže přihlásit do systému.",
                    CreatedDate = new DateOnly(2025, 7, 1),
                    DueDate = new DateOnly(2025, 7, 7),
                    RequesterId = 2,
                    AssigneeId = 3,
                    Status = IssueStatus.New,
                    Priority = IssuePriority.High
                },
                new Issue
                {
                    Id = 2,
                    Title = "Chyba ve fakturaci",
                    Description = "Firma nahlásila špatně spočítanou fakturu.",
                    CreatedDate = new DateOnly(2025, 7, 2),
                    DueDate = new DateOnly(2025, 7, 20),
                    RequesterId = 1,
                    AssigneeId = 3,
                    Status = IssueStatus.InProgress,
                    Priority = IssuePriority.Medium
                },
                new Issue
                {
                    Id = 3,
                    Title = "Požadavek na nové zařízení",
                    Description = "Zaměstnanec požaduje nové pracovní zařízení.",
                    CreatedDate = new DateOnly(2025, 7, 1),
                    DueDate = new DateOnly(2025, 7, 20),
                    RequesterId = 3,
                    AssigneeId = 1,
                    Status = IssueStatus.Resolved,
                    Priority = IssuePriority.Low
                },
                 new Issue
                 {
                     Id = 4,
                     Title = "Vyřízený požadavek",
                     Description = "Požadavek, který byl již vyřešen.",
                     CreatedDate = new DateOnly(2025, 6, 15),
                     DueDate = new DateOnly(2025, 6, 30),
                     RequesterId = 2,
                     AssigneeId = 3,
                     Status = IssueStatus.Resolved,
                     Priority = IssuePriority.Medium
                 },
                new Issue
                {
                    Id = 5,
                    Title = "Nevyřízený požadavek",
                    Description = "Požadavek, který ještě čeká na vyřízení.",
                    CreatedDate = new DateOnly(2025, 6, 20),
                    DueDate = new DateOnly(2025, 7, 5),
                    RequesterId = 1,
                    AssigneeId = 3,
                    Status = IssueStatus.InProgress,
                    Priority = IssuePriority.High
                },
                  new Issue
                  {
                      Id = 6,
                      Title = "Nevyřízený pož. s vyřízenými SubIssues",
                      Description = "Požadavek, který ještě čeká na vyřízení.",
                      CreatedDate = new DateOnly(2025, 6, 20),
                      DueDate = new DateOnly(2025, 7, 5),
                      RequesterId = 1,
                      AssigneeId = 3,
                      Status = IssueStatus.InProgress,
                      Priority = IssuePriority.High
                  }
            );
            modelBuilder.Entity<SubIssue>().HasData(
                // K Issue 1 ("Problém s přihlášením")
                new SubIssue
                {
                    Id = 1,
                    Title = "Reset hesla",
                    Description = "Uživatel požádal o reset hesla.",
                    DueDate = new DateOnly(2025, 7, 3),
                    IssueId = 1,
                    IsDone = true
                },
                new SubIssue
                {
                    Id = 2,
                    Title = "Zkontrolovat logy",
                    Description = "Zkontrolovat chyby v autentizaci v logu.",
                    DueDate = new DateOnly(2025, 7, 4),
                    IssueId = 1,
                    IsDone = false
                },
                // K issue 4 (Resolved)
                new SubIssue
                {
                    Id = 3,
                    Title = "Vyčistit data",
                    Description = "Zkontrolovat a vyčistit stará data.",
                    DueDate = new DateOnly(2025, 6, 20),
                    IssueId = 4,
                    IsDone = true
                },
                new SubIssue
                {
                    Id = 4,
                    Title = "Zálohovat systém",
                    Description = "Pro jistotu provést zálohu.",
                    DueDate = new DateOnly(2025, 6, 25),
                    IssueId = 4,
                    IsDone = true
                },
                // K issue 5 (InProgress)
                new SubIssue
                {
                    Id = 5,
                    Title = "Připravit podklady",
                    Description = "Získat všechny informace od klienta.",
                    DueDate = new DateOnly(2025, 7, 1),
                    IssueId = 5,
                    IsDone = false
                },
                new SubIssue
                {
                    Id = 6,
                    Title = "Nakonfigurovat zařízení",
                    Description = "Nastavit přidělené zařízení dle požadavků.",
                    DueDate = new DateOnly(2025, 7, 3),
                    IssueId = 5,
                    IsDone = false
                },
                // K issue 6
                new SubIssue
                {
                    Id = 7,
                    Title = "Připravit podklady",
                    Description = "Získat všechny informace od klienta.",
                    DueDate = new DateOnly(2025, 7, 1),
                    IssueId = 6,
                    IsDone = true
                },
                new SubIssue
                {
                    Id = 8,
                    Title = "Nakonfigurovat zařízení",
                    Description = "Nastavit přidělené zařízení dle požadavků.",
                    DueDate = new DateOnly(2025, 7, 3),
                    IssueId = 6,
                    IsDone = true
                }
            );
        }

    }
}
