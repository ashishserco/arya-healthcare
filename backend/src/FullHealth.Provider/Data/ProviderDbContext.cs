using Microsoft.EntityFrameworkCore;
using FullHealth.Provider.Models;

namespace FullHealth.Provider.Data;

public class ProviderDbContext : DbContext
{
    public ProviderDbContext(DbContextOptions<ProviderDbContext> options) : base(options) { }

    public DbSet<Doctor> Doctors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Seed data can go here
    }
}
