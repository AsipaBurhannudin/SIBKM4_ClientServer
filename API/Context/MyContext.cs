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
        
        // One University has many Educations
        modelBuilder.Entity<University>()
                    .HasMany(u => u.Educations)
                    .WithOne(e => e.University)
                    .IsRequired(false)
                    .HasForeignKey(e => e.UniversityId)
                    .OnDelete(DeleteBehavior.SetNull);

        // One Education has one Profiling
        modelBuilder.Entity<Education>()
                    .HasOne(e => e.Profiling)
                    .WithOne(p => p.Education)
                    .IsRequired(false)
                    .HasForeignKey<Profiling>(p => p.EducationId)
                    .OnDelete(DeleteBehavior.SetNull);

        //One Account has one Employee
        modelBuilder.Entity<Employee>()
                    .HasOne(e => e.Account)
                    .WithOne(p => p.Employee)
                    .IsRequired(false)
                    .HasForeignKey<Account>(p => p.EmployeeNIK)
                    .OnDelete(DeleteBehavior.Restrict);

        //One Profiling has one Account
        modelBuilder.Entity<Account>()
                    .HasOne(p => p.Profiling)
                    .WithOne(e => e.Account)
                    .IsRequired(false)
                    .HasForeignKey<Profiling>(e => e.EmployeeNIK)
                    .OnDelete(DeleteBehavior.Restrict);
        
        
        modelBuilder.Entity<AccountRole>()
                .HasKey(ar => ar.Id);

        modelBuilder.Entity<AccountRole>()
            .HasOne(ar => ar.Account)
            .WithMany(a => a.AccountRoles)
            .IsRequired(false)
            .HasForeignKey(a => a.AccountNIK)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AccountRole>()
            .HasOne(ar => ar.Role)
            .WithMany(r => r.AccountRoles)
            .HasForeignKey(ar => ar.RoleId);
    }
}




