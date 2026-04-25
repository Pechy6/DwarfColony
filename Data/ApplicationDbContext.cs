using Microsoft.EntityFrameworkCore;
using DwarfColony.Models.Entities;

namespace DwarfColony.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Dwarf> Dwarves { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<AreaResource> AreaResources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.
            Entity<Dwarf>().
            HasOne(d => d.CurrentArea).
            WithMany(a => a.Dwarves).
            HasForeignKey(d => d.CurrentAreaId);

        modelBuilder.
            Entity<AreaResource>().
            HasOne(ar => ar.Area).
            WithMany(a => a.Resources).
            HasForeignKey(ar => ar.AreaId);
    }
}