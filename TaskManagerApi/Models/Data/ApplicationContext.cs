using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Models;

namespace TaskManagerApi.Models.Data;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ProjectAdmin> ProjectAdmins { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Desk> Desks { get; set; }
    public DbSet<Task> Tasks { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();

        if (Users.Any(u => u.Status == UserStatus.Admin) == false)
        {
            var admin = new User("Vitalii", "Semi", "admin", "User@123", UserStatus.Admin);
            Users.Add(admin);
            SaveChanges();
        }
    }

}