using Microsoft.EntityFrameworkCore;
using Core.Models;

namespace DataStore.EF
{
    public class BugsContext : DbContext
    {
        public BugsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tickets)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

            //seeding
            modelBuilder.Entity<Project>().HasData(
                new Project { Id = 1, Name = "Project 1" },
                new Project { Id = 2, Name = "Project 2" }
                );

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { Id = 1, Title = "Bug #1", ProjectId = 1 },
                new Ticket { Id = 2, Title = "Bug #2", ProjectId = 1 },
                new Ticket { Id = 3, Title = "Bug #3", ProjectId = 2 }
                );
        }
    }
}
