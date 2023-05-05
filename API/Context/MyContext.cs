using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) { }

    // Introduce the model to the database that eventually become an entity
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Profiling> Profilings { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Role> Roles { get; set; }

    //Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //One University has many Educations
        modelBuilder.Entity<University>()
                    .HasMany(u => u.Educations)
                    .WithOne(e => e.Universities)
                    .HasForeignKey(e => e.UniversityId);

        //One Education has one Profiling
        modelBuilder.Entity<Education>()
                    .HasOne(e => e.Profilings)
                    .WithOne(p => p.Educations)
                    .HasForeignKey<Profiling>(p => p.EducationId);

        //One Profiling has One Employee
        modelBuilder.Entity<Employee>()
                    .HasOne(e => e.Profilings)
                    .WithOne(p => p.Employees)
                    .HasForeignKey<Profiling>(p => p.EmployeeNIK);

        //One Account has One Employee
        modelBuilder.Entity<Employee>()
                    .HasOne(em => em.Accounts)
                    .WithOne(ac => ac.Employees)
                    .HasForeignKey<Account>(ac => ac.EmployeeNIK);

        //One Account has many Account Roles
        modelBuilder.Entity<Account>()
                    .HasMany(ac => ac.AccountRoles)
                    .WithOne(a => a.Accounts)
                    .HasForeignKey(a => a.AccountNIK);

        //One Role has many Account Roles
        modelBuilder.Entity<Role>()
                    .HasMany(r => r.AccountRoles)
                    .WithOne(a => a.Roles)
                    .HasForeignKey(a => a.RoleId);
    }
}




