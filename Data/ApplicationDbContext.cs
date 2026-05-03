using Microsoft.EntityFrameworkCore;
using DwarfColony.Models.Entities;
using DwarfColony.Models.Entities.Dwarfs;
using DwarfColony.Models.Entities.World;
using DwarfColony.Models.Entities.BaseResources;
using DwarfColony.Models.Entities.WorldResources;

namespace DwarfColony.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Dwarf> Dwarves { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<AreaResource> AreaResources { get; set; }
    public DbSet<ResourceType> ResourceTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Propojeni trpaslika s oblastmi (jeden trpaslik muze byt v jedne oblasti, oblast muze mit vice trpasliku)
        modelBuilder.
            Entity<Dwarf>().
            HasOne(d => d.CurrentArea).
            WithMany(a => a.CurrentDwarves).
            HasForeignKey(d => d.CurrentAreaId).
            OnDelete(DeleteBehavior.Restrict);

        modelBuilder.
            Entity<Dwarf>().
            HasOne(d => d.TargetArea).
            WithMany(a => a.IncomingDwarves).
            HasForeignKey(d => d.TargetAreaId).
            OnDelete(DeleteBehavior.Restrict);
        
        // Oblast muze mit vice zdroju
        modelBuilder.
            Entity<AreaResource>().
            HasOne(ar => ar.Area).
            WithMany(a => a.Resources).
            HasForeignKey(ar => ar.AreaId);

        // AreaResource ma jeden typ zdroje, jeden ResourceType muze byt pouzit ve vice AreaResource
        modelBuilder.
            Entity<AreaResource>().
            HasOne(ar => ar.ResourceType).
            WithMany(rt => rt.AreaResources).
            HasForeignKey(ar => ar.ResourceTypeId);
    }
}