using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using UserContacts.Dal.Entities;
using UserContacts.Dal.EntityConfiguration;

namespace UserContacts.Dal;

public class MainContext : DbContext
{
    public DbSet<Users> Users { get; set; }
    public DbSet<UserRole> UserRole { get; set; }
    public DbSet<Contacts> Contacts { get; set; }
    public DbSet<RefreshTokens> RefreshTokens { get; set; }

    public MainContext(DbContextOptions<MainContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
