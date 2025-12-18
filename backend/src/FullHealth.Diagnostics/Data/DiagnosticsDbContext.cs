using Microsoft.EntityFrameworkCore;
using FullHealth.Diagnostics.Models;

namespace FullHealth.Diagnostics.Data;

public class DiagnosticsDbContext : DbContext
{
    public DiagnosticsDbContext(DbContextOptions<DiagnosticsDbContext> options) : base(options) { }

    public DbSet<LabPackage> LabPackages { get; set; }
    public DbSet<LabTest> LabTests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
