using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.DataAccess
{
    public class TaskManagerDbContext : IdentityDbContext<ApplicationUser,ApplicationRole, int>
    {
        public DbSet<TaskProject> TaskProjects { get; set; }
        public DbSet<ClientLocation> ClientLocations { get; set; }

        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ClientLocation>().HasData(
                new ClientLocation() { ClientLocationID = 1, ClientLocationName = "Boston" },
                new ClientLocation() { ClientLocationID = 2, ClientLocationName = "New Delhi" },
                new ClientLocation() { ClientLocationID = 3, ClientLocationName = "New Jersy" },
                new ClientLocation() { ClientLocationID = 4, ClientLocationName = "New York" },
                new ClientLocation() { ClientLocationID = 5, ClientLocationName = "London" },
                new ClientLocation() { ClientLocationID = 6, ClientLocationName = "Tokyo" }
            );

            builder.Entity<TaskProject>().HasData(
                new TaskProject() { ProjectId = 1, ProjectName = "Hospital Management System", DateOfStart = DateOnly.Parse("2017-8-1"), Active = true, ClientLocationID = 2, Status = "In Force", TeamSize = 14 },
                new TaskProject() { ProjectId = 2, ProjectName = "Reporting Tool", DateOfStart = DateOnly.Parse("2018-3-16") , Active = true, ClientLocationID = 1, Status = "Support", TeamSize = 81 }
            );

            base.OnModelCreating(builder);
        }
    }
}
