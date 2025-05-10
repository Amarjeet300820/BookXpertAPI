using Microsoft.EntityFrameworkCore;
using BookXpertAPI.Models;

namespace BookXpertAPI.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>().HasData(
                new State { Id = 1, Name = "Uttar Pradesh" },
                new State { Id = 2, Name = "Maharashtra" },
                new State { Id = 3, Name = "Karnataka" }
            );
        }

    }
}
